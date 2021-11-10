using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElBuenPrecio.Entidades
{
    public class Producto
    {
        //Declaración de variables
        private int idProducto;
        private string descripcionProducto;
        private decimal precio;
        private int stock;
        private Categoria categoria;

        //Constructor
        public Producto(int idProducto, string descripcionProducto, decimal precio, int stock, Categoria categoria)
        {
            this.idProducto = idProducto;
            this.descripcionProducto = descripcionProducto;
            this.precio = precio;
            this.stock = stock;
            this.categoria = categoria;
        }//Fin constructor

        //Getters y setters
        public int IdProducto {
            get { return idProducto; }
            set { idProducto = value; }
        }

        public string DescripcionProducto {
            get { return descripcionProducto; }
            set { descripcionProducto = value; }
        }

        public decimal Precio {
            get { return precio; }
            set { precio = value; }
        }

        public int Stock {
            get { return stock; }
            set { stock = value; }
        }

        public Categoria Categoria {
            get { return categoria; }
            set { categoria = value; }
        }
    }
}
