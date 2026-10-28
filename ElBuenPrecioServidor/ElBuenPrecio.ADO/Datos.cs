using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Microsoft.Data.Sqlite;
using ElBuenPrecio.Entidades;

namespace ElBuenPrecio.ADO
{
    public class Datos
    {
        private const string NombreCadena = "cadenaDeConexion";

        // Tablas que se pueden contar con Contar(). El nombre de tabla nunca viene de entrada externa.
        private static readonly string[] TablasPermitidas = { "Cajero", "Categoria", "Producto", "Venta", "DetalleDeLaVenta" };

        // Esquema SQLite traducido de ElBuenPrecio.sql: mismo orden de columnas, mismas PK y FK.
        private const string Esquema = @"
CREATE TABLE IF NOT EXISTS Cajero (
    Identificacion  INTEGER NOT NULL,
    Nombre          TEXT    NOT NULL,
    PrimerApellido  TEXT    NOT NULL,
    SegundoApellido TEXT    NOT NULL,
    CajaAsignada    INTEGER NOT NULL,
    Activo          INTEGER NOT NULL CHECK (Activo BETWEEN 0 AND 255),
    CONSTRAINT PK_Cajero PRIMARY KEY (Identificacion)
);
CREATE TABLE IF NOT EXISTS Categoria (
    Codigo      INTEGER NOT NULL,
    Descripcion TEXT    NOT NULL,
    CONSTRAINT PK_Categoria PRIMARY KEY (Codigo)
);
CREATE TABLE IF NOT EXISTS Producto (
    Codigo          INTEGER NOT NULL,
    Descripcion     TEXT    NOT NULL,
    Precio          NUMERIC NOT NULL,
    Cantidad        INTEGER NOT NULL,
    CodigoCategoria INTEGER NOT NULL,
    CONSTRAINT PK_Producto PRIMARY KEY (Codigo),
    CONSTRAINT FK_Producto_Categoria FOREIGN KEY (CodigoCategoria) REFERENCES Categoria (Codigo)
);
CREATE TABLE IF NOT EXISTS Venta (
    Codigo       INTEGER NOT NULL,
    CodigoCajero INTEGER NOT NULL,
    Fecha        TEXT    NOT NULL,
    Importe      NUMERIC NOT NULL,
    CONSTRAINT PK_Venta PRIMARY KEY (Codigo),
    CONSTRAINT FK_Venta_Cajero FOREIGN KEY (CodigoCajero) REFERENCES Cajero (Identificacion)
);
CREATE TABLE IF NOT EXISTS DetalleDeLaVenta (
    CodigoVenta    INTEGER NOT NULL,
    CodigoProducto INTEGER NOT NULL,
    PrecioUnitario NUMERIC NOT NULL,
    Cantidad       INTEGER NOT NULL,
    CONSTRAINT FK_Detalle_Producto FOREIGN KEY (CodigoProducto) REFERENCES Producto (Codigo),
    CONSTRAINT FK_Detalle_Venta    FOREIGN KEY (CodigoVenta)    REFERENCES Venta (Codigo)
);";

        // Resuelve un Data Source relativo contra el directorio indicado. Las rutas absolutas,
        // ":memory:" y las URI "file:" se dejan igual. Se conservan las demás claves.
        public static string ResolverCadenaDeConexion(string cadena, string directorioBase)
        {
            var builder = new SqliteConnectionStringBuilder(cadena);
            string origen = builder.DataSource;
            if (!string.IsNullOrWhiteSpace(origen)
                && origen != ":memory:"
                && !origen.StartsWith("file:", StringComparison.OrdinalIgnoreCase)
                && !Path.IsPathRooted(origen))
            {
                builder.DataSource = Path.Combine(directorioBase, origen);
            }
            return builder.ToString();
        }

        // Abre una conexión con la cadena resuelta contra AppDomain.CurrentDomain.BaseDirectory
        // y activa la integridad referencial (PRAGMA por conexión, Requisito 8.10).
        private static SqliteConnection AbrirConexion()
        {
            string cadena = ConfigurationManager.ConnectionStrings[NombreCadena].ConnectionString;
            var conexion = new SqliteConnection(
                ResolverCadenaDeConexion(cadena, AppDomain.CurrentDomain.BaseDirectory));
            conexion.Open();
            using (var pragma = conexion.CreateCommand())
            {
                pragma.CommandText = "PRAGMA foreign_keys = ON;";
                pragma.ExecuteNonQuery();
            }
            return conexion;
        }

        // Ejecuta una sentencia sin resultados, opcionalmente dentro de una transacción.
        private static void Ejecutar(SqliteConnection conexion, SqliteTransaction transaccion, string sql)
        {
            using (var comando = conexion.CreateCommand())
            {
                comando.Transaction = transaccion;
                comando.CommandText = sql;
                comando.ExecuteNonQuery();
            }
        }

        // Cuenta los registros de una tabla. Solo admite nombres de la lista fija TablasPermitidas.
        private static long Contar(SqliteConnection conexion, SqliteTransaction transaccion, string tabla)
        {
            if (Array.IndexOf(TablasPermitidas, tabla) < 0)
                throw new ArgumentException("Tabla no permitida: " + tabla, nameof(tabla));

            using (var comando = conexion.CreateCommand())
            {
                comando.Transaction = transaccion;
                comando.CommandText = string.Concat("SELECT COUNT(*) FROM ", tabla, ";");
                return Convert.ToInt64(comando.ExecuteScalar());
            }
        }

        // Crea el archivo de base de datos si no existe, el esquema si falta y los datos semilla
        // en las tablas que estén vacías. Las tablas con registros no se modifican.
        public void InicializarBaseDeDatos()
        {
            SQLitePCL.Batteries_V2.Init();
            using (var conexion = AbrirConexion())
            {
                Ejecutar(conexion, null, "PRAGMA journal_mode = WAL;");
                Ejecutar(conexion, null, Esquema);
                using (var tx = conexion.BeginTransaction())
                {
                    if (Contar(conexion, tx, "Cajero") == 0) InsertarCajerosSemilla(conexion, tx);
                    if (Contar(conexion, tx, "Categoria") == 0) InsertarCategoriasSemilla(conexion, tx);
                    if (Contar(conexion, tx, "Producto") == 0) InsertarProductosSemilla(conexion, tx);
                    tx.Commit();
                }
            }
        }

        // Cajero.Estado: 1 = activo, 0 = pendiente de activación.
        private static void InsertarCajerosSemilla(SqliteConnection conexion, SqliteTransaction tx)
        {
            InsertarCajero(conexion, tx, 1, "Ana", "Mora", "Solís", 1, 1);
            InsertarCajero(conexion, tx, 2, "Luis", "Vargas", "Rojas", 2, 0);
        }

        private static void InsertarCajero(SqliteConnection conexion, SqliteTransaction tx,
            int id, string nombre, string apellido1, string apellido2, int caja, byte activo)
        {
            using (var comando = conexion.CreateCommand())
            {
                comando.Transaction = tx;
                comando.CommandText = "INSERT INTO Cajero (Identificacion, Nombre, PrimerApellido, SegundoApellido, CajaAsignada, Activo) " +
                    "VALUES (@id, @nombre, @apellido1, @apellido2, @caja, @activo);";
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@apellido1", apellido1);
                comando.Parameters.AddWithValue("@apellido2", apellido2);
                comando.Parameters.AddWithValue("@caja", caja);
                comando.Parameters.AddWithValue("@activo", activo);
                comando.ExecuteNonQuery();
            }
        }

        private static void InsertarCategoriasSemilla(SqliteConnection conexion, SqliteTransaction tx)
        {
            InsertarCategoria(conexion, tx, 1, "Abarrotes");
            InsertarCategoria(conexion, tx, 2, "Lácteos");
            InsertarCategoria(conexion, tx, 3, "Limpieza");
        }

        private static void InsertarCategoria(SqliteConnection conexion, SqliteTransaction tx, int codigo, string descripcion)
        {
            using (var comando = conexion.CreateCommand())
            {
                comando.Transaction = tx;
                comando.CommandText = "INSERT INTO Categoria (Codigo, Descripcion) VALUES (@codigo, @descripcion);";
                comando.Parameters.AddWithValue("@codigo", codigo);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                comando.ExecuteNonQuery();
            }
        }

        private static void InsertarProductosSemilla(SqliteConnection conexion, SqliteTransaction tx)
        {
            InsertarProducto(conexion, tx, 1, "Arroz 1 kg", 1250.00m, 50, 1);
            InsertarProducto(conexion, tx, 2, "Frijoles 900 g", 1475.50m, 40, 1);
            InsertarProducto(conexion, tx, 3, "Leche 1 L", 890.00m, 60, 2);
            InsertarProducto(conexion, tx, 4, "Detergente 1 kg", 2350.00m, 25, 3);
        }

        // Si la categoría indicada no existe (Categoria ya tenía otros datos), se usa la de menor código.
        // Si Categoria está vacía no se inserta nada, así nunca se viola FK_Producto_Categoria.
        private static void InsertarProducto(SqliteConnection conexion, SqliteTransaction tx,
            int codigo, string descripcion, decimal precio, int cantidad, int categoria)
        {
            using (var comando = conexion.CreateCommand())
            {
                comando.Transaction = tx;
                comando.CommandText = "INSERT INTO Producto (Codigo, Descripcion, Precio, Cantidad, CodigoCategoria) " +
                    "SELECT @c, @d, @p, @s, COALESCE((SELECT Codigo FROM Categoria WHERE Codigo = @cat), (SELECT MIN(Codigo) FROM Categoria)) " +
                    "WHERE EXISTS (SELECT 1 FROM Categoria);";
                comando.Parameters.AddWithValue("@c", codigo);
                comando.Parameters.AddWithValue("@d", descripcion);
                comando.Parameters.AddWithValue("@p", precio);
                comando.Parameters.AddWithValue("@s", cantidad);
                comando.Parameters.AddWithValue("@cat", categoria);
                comando.ExecuteNonQuery();
            }
        }

        //Esta función se usa para probar si la base de datos existe y si el App.Config está bien configurado
        public void Conectar()
        {
            using (AbrirConexion())
            {
            }
        }

        public List<Cajero> ObtenerCajeros()
        {
            List<Cajero> listaCajeros = new List<Cajero>();

            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT Identificacion, Nombre, PrimerApellido, SegundoApellido, CajaAsignada, Activo FROM Cajero";
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaCajeros.Add(new Cajero
                            (reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                            reader.GetInt32(4), Convert.ToByte(reader.GetValue(5))));
                    }
                }
            }
            return listaCajeros;
        }//Fin obtener cajeros

        public Cajero ObtenerUnCajero(int ID)
        {
            Cajero cajero = null;

            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT Identificacion, Nombre, PrimerApellido, SegundoApellido, CajaAsignada, Activo FROM Cajero WHERE Identificacion = @user";
                comando.Parameters.AddWithValue("@user", ID);
                using (var reader = comando.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            cajero = new Cajero(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                                reader.GetInt32(4), Convert.ToByte(reader.GetValue(5)));
                        }
                    }
                    else
                    {
                        // Centinela: iD == -1 significa que el cajero no existe.
                        cajero = new Cajero(-1, "", "", "", 0, 0);
                    }
                }
            }
            return cajero;
        }

        public List<Categoria> ObtenerCategorias()
        {
            List<Categoria> listaCategorias = new List<Categoria>();

            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT Codigo, Descripcion FROM Categoria";
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaCategorias.Add(new Categoria(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            }
            return listaCategorias;
        }//Fin obtener categorias

        public Categoria ObtenerUnaCategoria(int ID)
        {
            Categoria categoria = null;

            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT Codigo, Descripcion FROM Categoria WHERE Codigo = @id";
                comando.Parameters.AddWithValue("@id", ID);
                using (var reader = comando.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }
                    while (reader.Read())
                    {
                        categoria = new Categoria(reader.GetInt32(0), reader.GetString(1));
                    }
                }
            }
            return categoria;
        }

        public List<Producto> ObtenerTodosLosProductos()
        {
            List<Producto> listaProductos = new List<Producto>();

            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT Codigo, Descripcion, Precio, Cantidad, CodigoCategoria FROM Producto";
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaProductos.Add(new Producto
                            (reader.GetInt32(0), reader.GetString(1), Convert.ToDecimal(reader.GetValue(2)), reader.GetInt32(3),
                            ObtenerUnaCategoria(reader.GetInt32(4))));
                    }
                }
            }
            return listaProductos;
        }//Fin obtener productos

        public List<Producto> ObtenerProductosPorNombre(string nombre)
        {
            List<Producto> listaProductos = new List<Producto>();

            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                // El texto del usuario va como parámetro, nunca concatenado en el SQL.
                comando.CommandText = "SELECT Codigo, Descripcion, Precio, Cantidad, CodigoCategoria FROM Producto WHERE Descripcion LIKE @patron";
                comando.Parameters.AddWithValue("@patron", string.Concat("%", nombre, "%"));
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaProductos.Add(new Producto
                            (reader.GetInt32(0), reader.GetString(1), Convert.ToDecimal(reader.GetValue(2)), reader.GetInt32(3),
                            ObtenerUnaCategoria(reader.GetInt32(4))));
                    }
                }
            }
            return listaProductos;
        }//Fin obtener productos

        public int ObtenerStockDeUnProducto(int codigo)
        {
            int stock = 0;

            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT Cantidad FROM Producto WHERE Codigo = @codigo";
                comando.Parameters.AddWithValue("@codigo", codigo);
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stock = reader.GetInt32(0);
                    }
                }
            }
            return stock;
        }//Fin obtener stock

        public List<Producto> ObtenerProductosPorCategoria(int codigoCategoria)
        {
            List<Producto> listaProductos = new List<Producto>();

            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT Codigo, Descripcion, Precio, Cantidad, CodigoCategoria FROM Producto WHERE CodigoCategoria = @codigo";
                comando.Parameters.AddWithValue("@codigo", codigoCategoria);
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaProductos.Add(new Producto
                            (reader.GetInt32(0), reader.GetString(1), Convert.ToDecimal(reader.GetValue(2)), reader.GetInt32(3),
                            ObtenerUnaCategoria(reader.GetInt32(4))));
                    }
                }
            }
            return listaProductos;
        }//Fin obtener productos

        public void IngresarCajero(Cajero cajero)
        {
            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "INSERT INTO Cajero (Identificacion, Nombre, PrimerApellido, SegundoApellido, CajaAsignada, Activo) " +
                    "VALUES (@ID, @Nombre, @Apellido1, @Apellido2, @Caja, @activo)";
                comando.Parameters.AddWithValue("@ID", cajero.iD);
                comando.Parameters.AddWithValue("@Nombre", cajero.Nombre);
                comando.Parameters.AddWithValue("@Apellido1", cajero.Apellido1);
                comando.Parameters.AddWithValue("@Apellido2", cajero.Apellido2);
                comando.Parameters.AddWithValue("@Caja", cajero.Caja);
                comando.Parameters.AddWithValue("@activo", cajero.Estado);
                comando.ExecuteNonQuery();
            }
        }//Fin ingresar cajero

        public void ActualizarCajero(Cajero cajero)
        {
            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "UPDATE Cajero SET Identificacion = @ID, Nombre = @Nombre, PrimerApellido = @Apellido1, SegundoApellido = @Apellido2, " +
                    "CajaAsignada = @Caja, Activo = @activo WHERE Identificacion = @ID";
                comando.Parameters.AddWithValue("@ID", cajero.iD);
                comando.Parameters.AddWithValue("@Nombre", cajero.Nombre);
                comando.Parameters.AddWithValue("@Apellido1", cajero.Apellido1);
                comando.Parameters.AddWithValue("@Apellido2", cajero.Apellido2);
                comando.Parameters.AddWithValue("@Caja", cajero.Caja);
                comando.Parameters.AddWithValue("@activo", cajero.Estado);
                comando.ExecuteNonQuery();
            }
        }

        public void IngresarCategoria(Categoria categoria)
        {
            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "INSERT INTO Categoria (Codigo, Descripcion) VALUES (@ID, @Descripcion)";
                comando.Parameters.AddWithValue("@ID", categoria.IdCategoria);
                comando.Parameters.AddWithValue("@Descripcion", categoria.DescripcionCategoria);
                comando.ExecuteNonQuery();
            }
        }//Fin ingresar categoría

        public void IngresarProducto(Producto producto)
        {
            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "INSERT INTO Producto (Codigo, Descripcion, Precio, Cantidad, CodigoCategoria) " +
                    "VALUES (@ID, @Descripcion, @Precio, @Stock, @Categoria)";
                comando.Parameters.AddWithValue("@ID", producto.IdProducto);
                comando.Parameters.AddWithValue("@Descripcion", producto.DescripcionProducto);
                comando.Parameters.AddWithValue("@Precio", producto.Precio);
                comando.Parameters.AddWithValue("@Stock", producto.Stock);
                comando.Parameters.AddWithValue("@Categoria", producto.Categoria.IdCategoria);
                comando.ExecuteNonQuery();
            }
        }//Fin ingresar producto

        public void ActualizarProductos(Producto producto)
        {
            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "UPDATE Producto SET Codigo = @ID, Descripcion = @descripcion, Precio = @precio, Cantidad = @stock, " +
                    "CodigoCategoria = @categoria WHERE Codigo = @ID";
                comando.Parameters.AddWithValue("@ID", producto.IdProducto);
                comando.Parameters.AddWithValue("@descripcion", producto.DescripcionProducto);
                comando.Parameters.AddWithValue("@precio", producto.Precio);
                comando.Parameters.AddWithValue("@stock", producto.Stock);
                comando.Parameters.AddWithValue("@categoria", producto.Categoria.IdCategoria);
                comando.ExecuteNonQuery();
            }
        }

        // Una sola conexión y una transacción (BEGIN IMMEDIATE): los escritores se serializan,
        // así que dos ventas simultáneas no obtienen el mismo MAX(Codigo) + 1.
        // Orden igual al original: cabecera de la venta, todos los detalles y luego el stock.
        public void RegistrarUnaVenta(TodaLaVenta venta)
        {
            using (var conexion = AbrirConexion())
            using (var tx = conexion.BeginTransaction())
            {
                int codigo;
                using (var comando = conexion.CreateCommand())
                {
                    comando.Transaction = tx;
                    comando.CommandText = "SELECT MAX(Codigo) FROM Venta";
                    object ultimo = comando.ExecuteScalar();
                    if (ultimo == null || ultimo == DBNull.Value)
                        codigo = 0;
                    else
                        codigo = Convert.ToInt32(ultimo) + 1;
                }

                using (var comando = conexion.CreateCommand())
                {
                    comando.Transaction = tx;
                    comando.CommandText = "INSERT INTO Venta (Codigo, CodigoCajero, Fecha, Importe) " +
                        "VALUES (@numfac, @cajero, date('now', 'localtime'), @importe)";
                    comando.Parameters.AddWithValue("@numfac", codigo);
                    comando.Parameters.AddWithValue("@cajero", venta.Venta.CodigoCajero);
                    comando.Parameters.AddWithValue("@importe", venta.Venta.Importe);
                    comando.ExecuteNonQuery();
                }

                foreach (DetalleDeVenta detalle in venta.Detalle)
                {
                    using (var comando = conexion.CreateCommand())
                    {
                        comando.Transaction = tx;
                        comando.CommandText = "INSERT INTO DetalleDeLaVenta (CodigoVenta, CodigoProducto, PrecioUnitario, Cantidad) " +
                            "VALUES (@factura, @codigoProducto, @precioUnitario, @cantidad)";
                        comando.Parameters.AddWithValue("@factura", codigo);
                        comando.Parameters.AddWithValue("@codigoProducto", detalle.CodigoProducto);
                        comando.Parameters.AddWithValue("@precioUnitario", detalle.PrecioUnitario);
                        comando.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                        comando.ExecuteNonQuery();
                    }
                }

                foreach (DetalleDeVenta detalle in venta.Detalle)
                {
                    // Stock actual del producto (0 si no existe, igual que antes).
                    int cantidad = 0;
                    using (var comando = conexion.CreateCommand())
                    {
                        comando.Transaction = tx;
                        comando.CommandText = "SELECT Cantidad FROM Producto WHERE Codigo = @codigo";
                        comando.Parameters.AddWithValue("@codigo", detalle.CodigoProducto);
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cantidad = reader.GetInt32(0);
                            }
                        }
                    }

                    int nuevo = cantidad - detalle.Cantidad;

                    using (var comando = conexion.CreateCommand())
                    {
                        comando.Transaction = tx;
                        comando.CommandText = "UPDATE Producto SET Cantidad = @stock WHERE Codigo = @codex";
                        comando.Parameters.AddWithValue("@stock", nuevo);
                        comando.Parameters.AddWithValue("@codex", detalle.CodigoProducto);
                        comando.ExecuteNonQuery();
                    }
                }

                tx.Commit();
            }
        }

        // TODO: NRE preexistente, sin uso — no arreglado en esta migración
        public TodaLaVenta ObtenerTodaUnaFactura(int numFactura)
        {
            TodaLaVenta venta = null;
            Venta laVenta = null;
            List<DetalleDeVenta> listaDetalles = new List<DetalleDeVenta>();

            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT Codigo, CodigoCajero, Fecha, Importe FROM Venta WHERE Codigo = @codigo";
                comando.Parameters.AddWithValue("@codigo", numFactura);
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        laVenta = new Venta
                            (reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2),
                            Convert.ToDecimal(reader.GetValue(3)));
                    }
                }
            }
            venta.Venta = laVenta;

            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT CodigoVenta, CodigoProducto, PrecioUnitario, Cantidad FROM DetalleDeLaVenta WHERE CodigoVenta = @codigo";
                comando.Parameters.AddWithValue("@codigo", venta.Venta.Codigo);
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaDetalles.Add(new DetalleDeVenta
                            (reader.GetInt32(0), reader.GetInt32(1), Convert.ToDecimal(reader.GetValue(2)), reader.GetInt32(3)));
                    }
                }
            }
            venta.Detalle = listaDetalles;
            return venta;
        }//Fin obtener toda una factura

        public List<Venta> ObtenerTodasLasVentas()
        {
            List<Venta> listaVentas = new List<Venta>();

            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT Codigo, CodigoCajero, Fecha, Importe FROM Venta";
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaVentas.Add(new Venta(reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2),
                            Convert.ToDecimal(reader.GetValue(3))));
                    }
                }
            }
            return listaVentas;
        }//Fin obtener ventas

        public List<DetalleDeVenta> ObtenerDetalleDeUnaVenta(Venta venta)
        {
            List<DetalleDeVenta> listaDetalles = new List<DetalleDeVenta>();

            using (var conexion = AbrirConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT CodigoVenta, CodigoProducto, PrecioUnitario, Cantidad FROM DetalleDeLaVenta WHERE CodigoVenta = @codigo";
                comando.Parameters.AddWithValue("@codigo", venta.Codigo);
                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaDetalles.Add(new DetalleDeVenta
                            (reader.GetInt32(0), reader.GetInt32(1), Convert.ToDecimal(reader.GetValue(2)), reader.GetInt32(3)));
                    }
                }
            }
            return listaDetalles;
        }//Fin obtener detalle de una venta
    }
}
