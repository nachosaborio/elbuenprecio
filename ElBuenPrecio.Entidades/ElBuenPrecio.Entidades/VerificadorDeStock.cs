using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElBuenPrecio.Entidades
{
    public class VerificadorDeStock
    {
        private int idProducto;
        private int cantidad;

        public int IDProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public VerificadorDeStock(int id, int cantidad)
        {
            this.IDProducto = id;
            this.Cantidad = cantidad;
        }

    }
}
