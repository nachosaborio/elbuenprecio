using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using ElBuenPrecio.Entidades;
using Microsoft.SqlServer.Server;

namespace ElBuenPrecioCliente
{
    public class Comando
    {
        private static IPAddress iPAddress;
        private static TcpClient tcpClient;
        private static StreamWriter writer;
        private static StreamReader reader;

        public static bool Conectar(int usuario)
        {
            try
            {
                iPAddress = IPAddress.Parse("127.0.0.1");
                tcpClient = new TcpClient();
                tcpClient.Connect(iPAddress, 16830);
                Mensajero<int> mensaje = new Mensajero<int> { Comando = "Conectar", Cliente = "Un cajero", Objeto = usuario };

                reader = new StreamReader(tcpClient.GetStream());
                writer = new StreamWriter(tcpClient.GetStream());
                writer.WriteLine(JsonConvert.SerializeObject(mensaje));
                writer.Flush();
            }
            catch (SocketException)
            {
                return false;
            }
            return true;
        }//Fin conectar

        public static Cajero ObtenerCajero()
        {
            var respuesta = reader.ReadLine();
            Cajero resp = JsonConvert.DeserializeObject<Cajero>(respuesta);
            return resp;
        }

        public static bool EscucharRespuesta()
        {
            var respuesta = reader.ReadLine();
            bool resp = JsonConvert.DeserializeObject<bool>(respuesta);
            return resp;
        }

        public static void EnviarCajero(Cajero cajero)
        {
            Mensajero<Cajero> mensaje = new Mensajero<Cajero> { Comando = "AgregarCajero", Cliente = cajero.Nombre, Objeto = cajero };
            writer.WriteLine(JsonConvert.SerializeObject(mensaje));
            writer.Flush();
        }

        public static void Desconectar(Cajero cajero)
        {
            Mensajero<Cajero> mensaje = new Mensajero<Cajero> { Comando = "Desconectar", Cliente = cajero.Nombre, Objeto = cajero };
            writer.WriteLine(JsonConvert.SerializeObject(mensaje));
            writer.Flush();
            tcpClient.Close();
        }

        public static List<Producto> PedirTodosLosProductos(Cajero cajero)
        {
            Mensajero<string> mensaje = new Mensajero<string> { Comando = "TodosLosProductos", Cliente = cajero.Nombre, Objeto = "" };
            writer.WriteLine(JsonConvert.SerializeObject(mensaje));
            writer.Flush();
            var respuesta = reader.ReadLine();
            List<Producto> listaProductos = JsonConvert.DeserializeObject<List<Producto>>(respuesta);
            return listaProductos;
        }

        public static List<Producto> PedirAlgunosProductos(Cajero cajero, string cmd)
        {
            Mensajero<string> mensaje = new Mensajero<string> { Comando = "AlgunosProductos", Cliente = cajero.Nombre, Objeto = cmd };
            writer.WriteLine(JsonConvert.SerializeObject(mensaje));
            writer.Flush();
            var respuesta = reader.ReadLine();
            List<Producto> listaProductos = JsonConvert.DeserializeObject<List<Producto>>(respuesta);
            return listaProductos;
        }

        public static bool ConsultarExistencia(Cajero cajero, VerificadorDeStock verificador)
        {
            Mensajero<VerificadorDeStock> mensaje = new Mensajero<VerificadorDeStock> { Comando = "HaySuficientes", Cliente = cajero.Nombre, Objeto = verificador };
            writer.WriteLine(JsonConvert.SerializeObject(mensaje));
            writer.Flush();
            var respuesta = reader.ReadLine();
            bool resp = JsonConvert.DeserializeObject<bool>(respuesta);
            return resp;
        }

        public static void Facturar(Cajero cajero, TodaLaVenta venta)
        {
            Mensajero<TodaLaVenta> mensaje = new Mensajero<TodaLaVenta> { Comando = "Facturar", Cliente = cajero.Nombre, Objeto = venta };
            writer.WriteLine(JsonConvert.SerializeObject(mensaje));
            writer.Flush();

        }
    }
}
