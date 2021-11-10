using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElBuenPrecio.Entidades
{
    public class DetalleDeVenta
    {
        private int codigoProducto;
        private decimal precioUnitario;
        private int cantidad;
        private int codigoVenta;


        public DetalleDeVenta(int codigoVenta, int codigoProducto, decimal precioUnitario, int cantidad)
        {
            CodigoVenta = codigoVenta;
            CodigoProducto = codigoProducto;
            PrecioUnitario = precioUnitario;
            Cantidad = cantidad;
        }

        public DetalleDeVenta(int codigoProducto, decimal precioUnitario, int cantidad)
        {
            CodigoProducto = codigoProducto;
            PrecioUnitario = precioUnitario;
            Cantidad = cantidad;
        }

        public DetalleDeVenta()
        {

        }

        public int CodigoVenta
        {
            get { return codigoVenta; }
            set { codigoVenta = value; }
        }


        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public decimal PrecioUnitario
        {
            get { return precioUnitario; }
            set { precioUnitario = value; }
        }

        public int CodigoProducto
        {
            get { return codigoProducto; }
            set { codigoProducto = value; }
        }

    }
}
