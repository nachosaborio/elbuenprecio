using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElBuenPrecio.Entidades
{
    public class TodaLaVenta
    {
        private Venta venta;

        public TodaLaVenta()
        {
                
        }

        public TodaLaVenta(Venta venta, List<DetalleDeVenta> detalle)
        {
            Venta = venta;
            Detalle = detalle;
        }

        private List<DetalleDeVenta> detalle;

        public List<DetalleDeVenta> Detalle { get { return detalle; } set { detalle = value; } }

        public Venta Venta
        {
            get { return venta; }
            set { venta = value; }
        }

    }
}
