using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using ElBuenPrecio.Entidades;
using System.IO;
using ElBuenPrecio.ADO;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;

namespace ElBuenPrecioServidor
{
    public partial class VentanaServidor : Form
    {
        bool iniciado = false;
        Thread threadEscucharClientes;
        TcpListener listener;
        int contador = 0;
        ActualizarContadorDeCLientes actualizar;
        ActualizarLaBitacora bitacora;
        ActualizarClientesConectados actualizarClientes;
        Datos datos = new Datos();

        public VentanaServidor()
        {
            InitializeComponent();
            lblStatus.ForeColor = Color.Red;
            btnApagar.Enabled = false;
            actualizar = new ActualizarContadorDeCLientes(AumentarContador);
            bitacora = new ActualizarLaBitacora(ActualizarBitacora);
            actualizarClientes = new ActualizarClientesConectados(ActualizarClientes);
            /*Antes de abrir el programa, pueba la conexión a la base de datos,
            si no funciona, lanza una ventana de advertencia.*/
            try
            {
                datos.Conectar();
            }
            catch (SqlException)
            {
                MessageBox.Show("Al parecer hay un error con la conexión con la base de datos.\nVerifique que ésta existe" +
                    " y que la cadena de conexión en App.Config esté correctamente configurada.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//Fin constructor

        private delegate void ActualizarContadorDeCLientes(int contador);
        private delegate void ActualizarLaBitacora(string cliente, string accion);
        private delegate void ActualizarClientesConectados(string cliente, int conexion);

        private void btnEncender_Click(object sender, EventArgs e)
        {
            threadEscucharClientes = new Thread(new ThreadStart(EscucharClientes));
            threadEscucharClientes.Start();
            threadEscucharClientes.IsBackground = true;

            iniciado = true;
            lblStatus.Text = "Escuchando clientes en 127.0.0.1 : 16830";
            lblStatus.ForeColor = Color.Green;
            btnEncender.Enabled = false;
            btnApagar.Enabled = true;
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            iniciado = false;
            listener.Stop();
            threadEscucharClientes.Abort();

            lblStatus.Text = "Apagado";
            lblStatus.ForeColor = Color.Red;
            btnEncender.Enabled = true;
            btnApagar.Enabled = false;
        }

        private void EscucharClientes()
        {
            IPAddress ipLocal = IPAddress.Parse("127.0.0.1"); //Establece el local host
            listener = new TcpListener(ipLocal, 16830); //Le pasa el IP y el puerto
            listener.Start();

            while (iniciado)
            {
                //Acepta al cliente
                TcpClient cliente = listener.AcceptTcpClient();
                //Le crea un hilo
                Thread threadCliente = new Thread(new ParameterizedThreadStart(ComunicacionCliente));
                threadCliente.Start(cliente);
                //Aumenta el contador de clientes conectados
                contador++;
                lblClientesConectados.Invoke(actualizar, new object[] { contador });
            }
        }

        private void ComunicacionCliente(object cliente)
        {
            TcpClient clienteTcp = (TcpClient)cliente;
            StreamReader reader = new StreamReader(clienteTcp.GetStream());
            StreamWriter writer = new StreamWriter(clienteTcp.GetStream());//El Writter debe ser único por cliente, se pasa por referencia

            while (iniciado)
            {
                try
                {
                    var mensaje = reader.ReadLine();//lee las peticiones del cliente
                    Mensajero<object> comando = JsonConvert.DeserializeObject<Mensajero<object>>(mensaje);//Convierte un mensaje de json a un mensajero
                    SeleccionarMetodo(comando.Comando, comando.Cliente, mensaje, ref writer);
                }
                catch (Exception)
                {
                    break;
                }
            }
            clienteTcp.Close();
        }

        private void SeleccionarMetodo(string comando, string cliente, string mensaje, ref StreamWriter writer)
        {
            bool resp = false;
            switch (comando)
            {
                case "Conectar":
                    Cajero cajero;
                    Mensajero<int> Mensaje = JsonConvert.DeserializeObject<Mensajero<int>>(mensaje);
                    cajero = new Cajero(datos.ObtenerUnCajero(Mensaje.Objeto));
                    txtBitacora.Invoke(bitacora, new object[] { cliente, " está haciendo login..." });
                    if (cajero.iD == -1)
                    {
                        txtBitacora.Invoke(bitacora, new object[] { "Un nuevo cajero", " se está registrando..." });
                    }
                    else if (cajero.Estado == 0)
                    {
                        txtBitacora.Invoke(bitacora, new object[] { cajero.Nombre, " quiere hacer login pero aún no está activado..." });
                    }
                    else
                    {
                        txtBitacora.Invoke(bitacora, new object[] { cajero.Nombre, " se conectó..." });
                        txtBitacora.Invoke(actualizarClientes, new object[] { cajero.Nombre, 1 });
                    }
                    writer.WriteLine(JsonConvert.SerializeObject(cajero));
                    writer.Flush();
                    break;
                case "AgregarCajero":
                    Mensajero<Cajero> mensajeCrearCajero = JsonConvert.DeserializeObject<Mensajero<Cajero>>(mensaje);
                    try
                    {
                        datos.IngresarCajero(mensajeCrearCajero.Objeto);
                        resp = true;
                    }
                    catch (SqlException)
                    {
                        resp = false;
                    }
                    writer.WriteLine(JsonConvert.SerializeObject(resp));
                    writer.Flush();
                    break;
                case "Desconectar":
                    txtBitacora.Invoke(bitacora, new object[] { cliente, " se desconectó..." });
                    contador--;
                    lblClientesConectados.Invoke(actualizar, new object[] { contador });
                    txtBitacora.Invoke(actualizarClientes, new object[] { cliente, 0 });
                    break;
                case "TodosLosProductos":
                    List<Producto> listaProductos = datos.ObtenerTodosLosProductos();
                    writer.WriteLine(JsonConvert.SerializeObject(listaProductos));
                    txtBitacora.Invoke(bitacora, new object[] { cliente, " está consultando inventario..." });
                    writer.Flush();
                    break;
                case "AlgunosProductos":
                    Mensajero<string> mensajeUnProducto = JsonConvert.DeserializeObject<Mensajero<string>>(mensaje);
                    List<Producto> algunosProductos = datos.ObtenerProductosPorNombre(mensajeUnProducto.Objeto.ToString());
                    writer.WriteLine(JsonConvert.SerializeObject(algunosProductos));
                    txtBitacora.Invoke(bitacora, new object[] { cliente, " está consultando inventario..." });
                    writer.Flush();
                    break;
                case "HaySuficientes":
                    Mensajero<VerificadorDeStock> comprobarExistencia = JsonConvert.DeserializeObject<Mensajero<VerificadorDeStock>>(mensaje);
                    if (comprobarExistencia.Objeto.Cantidad <= datos.ObtenerStockDeUnProducto(comprobarExistencia.Objeto.IDProducto))
                    {
                        writer.WriteLine(JsonConvert.SerializeObject(true));
                    }
                    else
                    {
                        writer.WriteLine(JsonConvert.SerializeObject(false));
                    }
                    writer.Flush();
                    txtBitacora.Invoke(bitacora, new object[] { cliente, " está consultando inventario..." });
                    break;
                case "Facturar":
                    Mensajero<TodaLaVenta> facturacion = JsonConvert.DeserializeObject<Mensajero<TodaLaVenta>>(mensaje);
                    datos.RegistrarUnaVenta(facturacion.Objeto);
                    txtBitacora.Invoke(bitacora, new object[] { cliente, " ha facturado una venta..." });
                    break;
            }
        }



        private void AumentarContador(int contador)
        {
            lblClientesConectados.Text = Convert.ToString(contador);
        }

        private void ActualizarBitacora(string cliente, string accion)
        {
            txtBitacora.Items.Add(string.Format("{0}{1}", cliente, accion));
        }

        private void ActualizarClientes(string cliente, int conexion)
        {
            if (conexion == 1)
                txtClientesConectados.Items.Add(string.Format("{0}", cliente));
            else
                txtClientesConectados.Items.Remove(cliente);
        }

        private void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            MasCategorias masCategorias = new MasCategorias();
            masCategorias.Show();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            MasProductos masProductos = new MasProductos();
            masProductos.Show();
        }

        private void btnImprimeCajeros_Click(object sender, EventArgs e)
        {
            MostrarCajeros mostrarCajeros = new MostrarCajeros();
            mostrarCajeros.Show();
        }

        private void btnImprimeCategorias_Click(object sender, EventArgs e)
        {
            MostrarCategorias mostrarCategorias = new MostrarCategorias();
            mostrarCategorias.Show();
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            DevuelveUnProducto todosLosProductos = new DevuelveUnProducto();
            todosLosProductos.Show();
        }

        private void btnPorCategoria_Click(object sender, EventArgs e)
        {
            MostrarProductosPorCategoria porCategoria = new MostrarProductosPorCategoria();
            porCategoria.Show();
        }

        private void btnAgregarCajero_Click(object sender, EventArgs e)
        {
            ActualizarCajeros actualizarCajeros = new ActualizarCajeros();
            actualizarCajeros.Show();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            ActualizarInventario actualizar = new ActualizarInventario();
            actualizar.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MostrarDetalleDeUnaVenta mostrarDetalle = new MostrarDetalleDeUnaVenta();
            mostrarDetalle.Show();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Muestra la portada de la tarea
            MessageBox.Show("\nUNIVERSIDAD ESTATAL A DISTANCIA" +
                "\nVICERRECTORÍA ACADÉMICA" +
                "\nESCUELA DE CIENCIAS EXACTAS Y NATURALES" +
                "\nCÁTEDRA DE DESARROLLO DE SISTEMAS" +
                "\nDIPLOMADO EN INFORMÁTICA" +
                "\n\nCódigo: 00830" +
                "\n\nProgramación avanzada" +
                "\n\nCentro Universitario: San Isidro (13)" +
                "\nGrupo: 1" +
                "\n\nEstudiante: Sergio Ignacio Saborío Segura" +
                "\nCédula: 1-1717-0701" +
                "\n\nPROYECTO FINAL DEL SEGUNDO CUATRIMESTRE, 2020", "Acerca de",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
