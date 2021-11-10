using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElBuenPrecio.Entidades
{
    public class Venta
    {
        
        private int codigoCajero;
        private decimal importe;
        private int codigo;
        private DateTime fecha;

        public Venta(int codigo, int codigoCajero, DateTime fecha, decimal importe)
        {
            Codigo = codigo;
            CodigoCajero = codigoCajero;
            Fecha = fecha;
            Importe = importe;
        }

        public Venta(int codigoCajero, decimal importe)
        {
            CodigoCajero = codigoCajero;
            Importe = importe;
        }
        public Venta()
        {

        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }


        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public int CodigoCajero { get { return codigoCajero; } set { codigoCajero = value; } }
        public decimal Importe { get { return importe; } set { importe = value; } }
    }
}
