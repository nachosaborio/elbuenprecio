using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ElBuenPrecio.Entidades;

namespace ElBuenPrecio.ADO
{
    public class Datos
    {
        //Esta función se usa para probar si la base de datos existe y si el App.Config está bien configurado
        public void Conectar()
        {

            string cadena;
            SqlConnection conexion;

            cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            conexion = new SqlConnection(cadena);
            conexion.Open();
            conexion.Close();
        }

        public List<Cajero> ObtenerCajeros()
        {
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            List<Cajero> listaCajeros = new List<Cajero>();
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            SqlDataReader reader;

            conexion = new SqlConnection(cadena);
            query = "Select Identificacion, Nombre, PrimerApellido, SegundoApellido, CajaAsignada, Activo From Cajero";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar

            conexion.Open(); //Abre la conexion

            reader = comando.ExecuteReader(); //Ejecuta la sentencia


            //Si la consulta devuelve algo
            if (reader.HasRows)
            {
                //Mientras haya algo que leer...
                while (reader.Read())
                {
                    listaCajeros.Add(new Cajero
                        (reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                        reader.GetInt32(4), reader.GetByte(5)));
                }//Fin while
            }//fin if
            conexion.Close();
            return listaCajeros;
        }//Fin obtener cajeros

        public Cajero ObtenerUnCajero(int ID)
        {
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            SqlDataReader reader;
            Cajero cajero = null;

            conexion = new SqlConnection(cadena);
            query = "Select Identificacion, Nombre, PrimerApellido, SegundoApellido, CajaAsignada, Activo from Cajero where Identificacion = @user";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar
            comando.Parameters.AddWithValue("@user", ID);

            conexion.Open(); //Abre la conexion

            reader = comando.ExecuteReader(); //Ejecuta la sentencia


            //Si la consulta devuelve algo
            if (reader.HasRows)
            {
                //Mientras haya algo que leer...
                while (reader.Read())
                {
                    cajero = new Cajero(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                        reader.GetInt32(4), reader.GetByte(5));
                }//Fin while
            }//fin if
            else
            {
                cajero = new Cajero(-1, "", "", "", 0, 0);
            }
            conexion.Close();

            return cajero;
        }

        public List<Categoria> ObtenerCategorias()
        {

            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            List<Categoria> listaCategorias = new List<Categoria>();
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            SqlDataReader reader;

            conexion = new SqlConnection(cadena);
            query = "Select Codigo, Descripcion From Categoria";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar

            conexion.Open(); //Abre la conexion

            reader = comando.ExecuteReader(); //Ejecuta la sentencia


            //Si la consulta devuelve algo
            if (reader.HasRows)
            {
                //Mientras haya algo que leer...
                while (reader.Read())
                {
                    listaCategorias.Add(new Categoria(reader.GetInt32(0), reader.GetString(1)));
                }//Fin while
            }//fin if
            conexion.Close();

            return listaCategorias;
        }//Fin obtener categorias

        public Categoria ObtenerUnaCategoria(int ID)
        {
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            SqlDataReader reader;
            Categoria categoria = null;

            conexion = new SqlConnection(cadena);
            query = "Select Codigo, Descripcion From Categoria where Codigo = @id";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar
            comando.Parameters.AddWithValue("@id", ID);

            conexion.Open(); //Abre la conexion

            reader = comando.ExecuteReader(); //Ejecuta la sentencia


            //Si la consulta devuelve algo
            if (reader.HasRows)
            {
                //Mientras haya algo que leer...
                while (reader.Read())
                {
                    categoria = new Categoria(reader.GetInt32(0), reader.GetString(1));
                }//Fin while
            }//fin if
            else
            {
                return null;
            }
            conexion.Close();

            return categoria;
        }

        public List<Producto> ObtenerTodosLosProductos()
        {
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            List<Producto> listaProductos = new List<Producto>();
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            SqlDataReader reader;

            conexion = new SqlConnection(cadena);
            query = "Select Codigo, Descripcion, Precio, Cantidad, CodigoCategoria  From Producto";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar

            conexion.Open(); //Abre la conexion

            reader = comando.ExecuteReader(); //Ejecuta la sentencia


            //Si la consulta devuelve algo
            if (reader.HasRows)
            {
                //Mientras haya algo que leer...
                while (reader.Read())
                {
                    listaProductos.Add(new Producto
                        (reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetInt32(3), ObtenerUnaCategoria(reader.GetInt32(4))));
                }//Fin while
            }//fin if
            conexion.Close();
            return listaProductos;
        }//Fin obtener productos

        public List<Producto> ObtenerProductosPorNombre(string nombre)
        {
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            List<Producto> listaProductos = new List<Producto>();
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            SqlDataReader reader;

            conexion = new SqlConnection(cadena);
            query = "Select * From Producto Where Descripcion like ('%" + nombre + "%')";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar

            conexion.Open(); //Abre la conexion

            reader = comando.ExecuteReader(); //Ejecuta la sentencia


            //Si la consulta devuelve algo
            if (reader.HasRows)
            {
                //Mientras haya algo que leer...
                while (reader.Read())
                {
                    listaProductos.Add(new Producto
                        ((Int32)reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetInt32(3),
                        ObtenerUnaCategoria(reader.GetInt32(4))));
                }//Fin while
            }//fin if
            conexion.Close();
            return listaProductos;
        }//Fin obtener productos

        public int ObtenerStockDeUnProducto(int codigo)
        {
            int stock = 0;
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            List<Producto> listaProductos = new List<Producto>();
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            SqlDataReader reader;

            conexion = new SqlConnection(cadena);
            query = "Select Cantidad From Producto Where Codigo = @codigo";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar
            comando.Parameters.AddWithValue("@codigo", codigo);

            conexion.Open(); //Abre la conexion

            reader = comando.ExecuteReader(); //Ejecuta la sentencia


            //Si la consulta devuelve algo
            if (reader.HasRows)
            {
                //Mientras haya algo que leer...
                while (reader.Read())
                {
                    stock = (Int32)reader.GetInt32(0);
                }//Fin while
            }//fin if
            conexion.Close();
            return stock;
        }//Fin obtener productos

        public List<Producto> ObtenerProductosPorCategoria(int codigoCategoria)
        {
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            List<Producto> listaProductos = new List<Producto>();
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            SqlDataReader reader;

            conexion = new SqlConnection(cadena);
            query = "Select Codigo, Descripcion, Precio, Cantidad, CodigoCategoria From Producto Where CodigoCategoria = @codigo";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar
            comando.Parameters.AddWithValue("@codigo", codigoCategoria);

            conexion.Open(); //Abre la conexion

            reader = comando.ExecuteReader(); //Ejecuta la sentencia


            //Si la consulta devuelve algo
            if (reader.HasRows)
            {
                //Mientras haya algo que leer...
                while (reader.Read())
                {
                    listaProductos.Add(new Producto
                        ((Int32)reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetInt32(3),
                        ObtenerUnaCategoria(reader.GetInt32(4))));
                }//Fin while
            }//fin if
            conexion.Close();
            return listaProductos;
        }//Fin obtener productos

        public void IngresarCajero(Cajero cajero)
        {
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();

            conexion = new SqlConnection(cadena);
            query = "Insert into Cajero values (@ID, @Nombre, @Apellido1, @Apellido2, @Caja, @activo)";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar
            comando.Parameters.AddWithValue("@ID", cajero.iD);
            comando.Parameters.AddWithValue("@Nombre", cajero.Nombre);
            comando.Parameters.AddWithValue("@Apellido1", cajero.Apellido1);
            comando.Parameters.AddWithValue("@Apellido2", cajero.Apellido2);
            comando.Parameters.AddWithValue("@Caja", cajero.Caja);
            comando.Parameters.AddWithValue("@activo", cajero.Estado);

            conexion.Open(); //Abre la conexion

            comando.ExecuteNonQuery(); //Ejecuta la sentencia

            conexion.Close();
        }//Fin ingresar cajero

        public void ActualizarCajero(Cajero cajero)
        {
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();

            conexion = new SqlConnection(cadena);
            query = "Update Cajero set Identificacion=@ID, Nombre=@Nombre, PrimerApellido=@Apellido1, SegundoApellido=@Apellido2," +
                " CajaAsignada=@Caja, Activo=@activo where Identificacion = @ID";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar
            comando.Parameters.AddWithValue("@ID", cajero.iD);
            comando.Parameters.AddWithValue("@Nombre", cajero.Nombre);
            comando.Parameters.AddWithValue("@Apellido1", cajero.Apellido1);
            comando.Parameters.AddWithValue("@Apellido2", cajero.Apellido2);
            comando.Parameters.AddWithValue("@Caja", cajero.Caja);
            comando.Parameters.AddWithValue("@activo", cajero.Estado);

            conexion.Open(); //Abre la conexion

            comando.ExecuteNonQuery(); //Ejecuta la sentencia

            conexion.Close();
        }

        public void IngresarCategoria(Categoria categoria)
        {
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();

            conexion = new SqlConnection(cadena);
            query = "Insert into Categoria values (@ID, @Descripcion)";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar
            comando.Parameters.AddWithValue("@ID", categoria.IdCategoria);
            comando.Parameters.AddWithValue("@Descripcion", categoria.DescripcionCategoria);

            conexion.Open(); //Abre la conexion

            comando.ExecuteNonQuery(); //Ejecuta la sentencia

            conexion.Close();
        }//Fin ingresar categoría

        public void IngresarProducto(Producto producto)
        {

            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();

            conexion = new SqlConnection(cadena);
            query = "Insert into Producto values (@ID, @Descripcion, @Precio, @Stock, @Categoria)";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar
            comando.Parameters.AddWithValue("@ID", producto.IdProducto);
            comando.Parameters.AddWithValue("@Descripcion", producto.DescripcionProducto);
            comando.Parameters.AddWithValue("@Precio", producto.Precio);
            comando.Parameters.AddWithValue("@Stock", producto.Stock);
            comando.Parameters.AddWithValue("@Categoria", producto.Categoria.IdCategoria);

            conexion.Open(); //Abre la conexion

            comando.ExecuteNonQuery(); //Ejecuta la sentencia

            conexion.Close();
        }//Fin ingresar producto

        public void ActualizarProductos(Producto producto)
        {
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();

            conexion = new SqlConnection(cadena);
            query = "Update Producto set Codigo=@ID, Descripcion=@descripcion, Precio=@precio, Cantidad=@stock," +
                " CodigoCategoria=@categoria where Codigo = @ID";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar
            comando.Parameters.AddWithValue("@ID", producto.IdProducto);
            comando.Parameters.AddWithValue("@descripcion", producto.DescripcionProducto);
            comando.Parameters.AddWithValue("@precio", producto.Precio);
            comando.Parameters.AddWithValue("@stock", producto.Stock);
            comando.Parameters.AddWithValue("@categoria", producto.Categoria.IdCategoria);

            conexion.Open(); //Abre la conexion

            comando.ExecuteNonQuery(); //Ejecuta la sentencia

            conexion.Close();
        }

        public void RegistrarUnaVenta(TodaLaVenta venta)
        {
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            SqlConnection conexion;
            SqlDataReader reader;
            SqlCommand comando = new SqlCommand();
            int codigo = 0;

            conexion = new SqlConnection(cadena);
            query = "Select MAX(Codigo) as ultimo from Venta ";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar

            conexion.Open(); //Abre la conexion

            reader = comando.ExecuteReader(); //Ejecuta la sentencia
            //Si la consulta devuelve algo
            if (reader.HasRows)
            {
                //Mientras haya algo que leer...
                while (reader.Read())
                {
                    if (reader.IsDBNull(0))
                        codigo = 0;
                    else
                        codigo = reader.GetInt32(0) + 1;
                }//Fin while
            }//fin if
            conexion.Close();

            conexion = new SqlConnection(cadena);
            query = "Insert into Venta values (@numfac, @cajero, GetDate(), @importe)";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar
            comando.Parameters.AddWithValue("@numfac", codigo);
            comando.Parameters.AddWithValue("@cajero", venta.Venta.CodigoCajero);
            comando.Parameters.AddWithValue("@importe", venta.Venta.Importe);


            conexion.Open(); //Abre la conexion

            comando.ExecuteNonQuery(); //Ejecuta la sentencia

            conexion.Close();

            int a = 0, b = 0, c = 0, d = 0;
            foreach (DetalleDeVenta detalle in venta.Detalle)
            {
                conexion = new SqlConnection(cadena);
                query = "Insert into DetalleDeLaVenta (CodigoVenta,CodigoProducto,PrecioUnitario,Cantidad) values (@factura"+a+", " +
                    "@codigoProducto"+b+", @precioUnitario"+c+", @cantidad"+d+")";

                comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
                comando.CommandText = query; //Asigna el query al comando
                comando.Connection = conexion; //Le dice con cuál conexion trabajar
                comando.Parameters.AddWithValue("@factura"+a+"", codigo);
                comando.Parameters.AddWithValue("@codigoProducto"+b+"", detalle.CodigoProducto);
                comando.Parameters.AddWithValue("@precioUnitario"+c+"", detalle.PrecioUnitario);
                comando.Parameters.AddWithValue("@cantidad"+d+"", detalle.Cantidad);
                ++a; ++b; ++c; ++d;

                conexion.Open(); //Abre la conexion

                comando.ExecuteNonQuery(); //Ejecuta la sentencia
                

                conexion.Close();
                
            }

            int x = 0, y =0, z=0;
            foreach (DetalleDeVenta detalle in venta.Detalle)
            {
                conexion = new SqlConnection(cadena);
                int cantidad = 0;

                conexion = new SqlConnection(cadena);
                query = "Select * from  Producto where Codigo = @codigo"+x+"";

                comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
                comando.CommandText = query; //Asigna el query al comando
                comando.Connection = conexion; //Le dice con cuál conexion trabajar
                comando.Parameters.AddWithValue("@codigo"+x+"", detalle.CodigoProducto);
                conexion.Open(); //Abre la conexion

                reader = comando.ExecuteReader(); //Ejecuta la sentencia
                                                  //Si la consulta devuelve algo
                if (reader.HasRows)
                {
                    //Mientras haya algo que leer...
                    while (reader.Read())
                    {
                       Producto producto = new Producto(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetInt32(3),
                        ObtenerUnaCategoria(reader.GetInt32(4)));
                        cantidad = producto.Stock;
                    }//Fin while
                }//fin if
                ++x;
                conexion.Close();

                int nuevo = cantidad - detalle.Cantidad;

                query = "Update Producto set Cantidad =  @stock"+y+" Where Codigo = @codex"+z+"";

                comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
                comando.CommandText = query; //Asigna el query al comando
                comando.Connection = conexion; //Le dice con cuál conexion trabajar
                comando.Parameters.AddWithValue("@stock" + y + "", nuevo);
                comando.Parameters.AddWithValue("@codex" + z + "", detalle.CodigoProducto);

                conexion.Open(); //Abre la conexion

                comando.ExecuteNonQuery(); //Ejecuta la sentencia
                ++y; ++z;
                conexion.Close();
            }
        }

        public TodaLaVenta ObtenerTodaUnaFactura(int numFactura)
        {
            TodaLaVenta venta = null;
            Venta laVenta = null;
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            List<DetalleDeVenta> listaDetalles = new List<DetalleDeVenta>();
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            SqlDataReader reader;

            conexion = new SqlConnection(cadena);
            query = "Select *  From Venta where Codigo = @codigo";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar
            comando.Parameters.AddWithValue("@codigo", numFactura);

            conexion.Open(); //Abre la conexion

            reader = comando.ExecuteReader(); //Ejecuta la sentencia


            //Si la consulta devuelve algo
            if (reader.HasRows)
            {
                //Mientras haya algo que leer...
                while (reader.Read())
                {
                    laVenta = new Venta
                        (reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2),
                        reader.GetDecimal(3));
                }//Fin while
            }//fin if
            conexion.Close();
            venta.Venta = laVenta;
            conexion = new SqlConnection(cadena);
            query = "Select * From DetalleDeLaVenta where CodigoVenta = @codigo";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar
            comando.Parameters.AddWithValue("@codigo", venta.Venta.Codigo);

            conexion.Open(); //Abre la conexion

            reader = comando.ExecuteReader(); //Ejecuta la sentencia


            //Si la consulta devuelve algo
            if (reader.HasRows)
            {
                //Mientras haya algo que leer...
                while (reader.Read())
                {
                    listaDetalles.Add(new DetalleDeVenta
                        (reader.GetInt32(0), reader.GetInt32(1), reader.GetDecimal(2), reader.GetInt32(3)));
                }//Fin while
            }//fin if
            conexion.Close();
            venta.Detalle = listaDetalles;
            return venta;
        }//Fin obtener productos

        public List<Venta> ObtenerTodasLasVentas()
        {
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            List<Venta> listaVentas = new List<Venta>();
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            SqlDataReader reader;

            conexion = new SqlConnection(cadena);
            query = "Select * From Venta";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar

            conexion.Open(); //Abre la conexion

            reader = comando.ExecuteReader(); //Ejecuta la sentencia


            //Si la consulta devuelve algo
            if (reader.HasRows)
            {
                //Mientras haya algo que leer...
                while (reader.Read())
                {
                    listaVentas.Add(new Venta(reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2),
                        reader.GetDecimal(3)));
                }//Fin while
            }//fin if
            conexion.Close();
            return listaVentas;
        }//Fin obtener productos

        public List<DetalleDeVenta> ObtenerDetalleDeUnaVenta(Venta venta)
        {
            string query;
            string cadena = ConfigurationManager.ConnectionStrings["cadenaDeConexion"].ConnectionString; //Lee el nombre de la cadena de conexion del appconfig
            List<DetalleDeVenta> listaDetalles = new List<DetalleDeVenta>();
            SqlConnection conexion;
            SqlCommand comando = new SqlCommand();
            SqlDataReader reader;

            conexion = new SqlConnection(cadena);
            query = "Select * From DetalleDeLaVenta where CodigoVenta = @codigo";

            comando.CommandType = CommandType.Text;//Indica qué tipo de conexion es, si texto o un procedimiento almacenado
            comando.CommandText = query; //Asigna el query al comando
            comando.Connection = conexion; //Le dice con cuál conexion trabajar
            comando.Parameters.AddWithValue("@codigo", venta.Codigo);

            conexion.Open(); //Abre la conexion

            reader = comando.ExecuteReader(); //Ejecuta la sentencia

            //Si la consulta devuelve algo
            if (reader.HasRows)
            {
                //Mientras haya algo que leer...
                while (reader.Read())
                {
                    listaDetalles.Add(new DetalleDeVenta
                        (reader.GetInt32(0), reader.GetInt32(1), reader.GetDecimal(2), reader.GetInt32(3)));
                }//Fin while
            }//fin if
            conexion.Close();
            return listaDetalles;
        }//Fin obtener productos
    }
}
