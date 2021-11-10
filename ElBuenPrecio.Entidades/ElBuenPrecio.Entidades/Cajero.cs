using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElBuenPrecio.Entidades
{
    public class Cajero
    {
        //Declaración de variables
        private int ID;
        private string nombre;
        private string apellido1;
        private string apellido2;
        private int caja;
        private byte estado;

        //Constructor
        public Cajero(int ID, string nombre, string apellido1, string apellido2, int caja, byte estado)
        {
            this.ID = ID;
            this.nombre = nombre;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.caja = caja;
            this.estado = estado;
        }//Fin constructor

        public Cajero(Cajero cajero) : this(cajero.iD, cajero.Nombre, cajero.Apellido1, cajero.Apellido2,
            cajero.Caja, cajero.Estado) {}

        //Getters y setters
        public int iD { get { return ID; } set { ID = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Apellido1 { get { return apellido1; }  set { apellido1 = value; } }
        public string Apellido2 { get { return apellido2; } set { apellido2 = value; } }
        public int Caja { get { return caja; } set { caja = value; } }
        public byte Estado { get { return estado; } set { estado = value; } }

        public Cajero() { }
    }
}
