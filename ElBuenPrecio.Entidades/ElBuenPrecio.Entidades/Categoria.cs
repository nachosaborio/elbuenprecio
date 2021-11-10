using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElBuenPrecio.Entidades
{
    public class Categoria
    {
        //Declaración de variables
        private int idCategoria;

        private string descripcionCategoria;

        //Constructor
        public Categoria(int idCategoria, string descripcionCategoria)
        {
            this.idCategoria = idCategoria;
            this.descripcionCategoria = descripcionCategoria;
        }//Fin constructor

        //Getters y setters
        public int IdCategoria {
            get { return idCategoria; }
            set { idCategoria = value; }
        }

        public string DescripcionCategoria {
            get { return descripcionCategoria; }
            set { descripcionCategoria = value; }
        }
    }
}
