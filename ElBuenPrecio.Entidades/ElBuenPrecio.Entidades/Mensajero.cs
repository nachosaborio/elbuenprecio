using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElBuenPrecio.Entidades
{
    [Serializable]
    public class Mensajero<T>
    {
        private string comando;

        private string cliente;

        private T objeto;

        //Con este string, el cliente le dice al servidor qué quiere
        public string Comando
        {
            get { return comando; }
            set { comando = value; }
        }

        public string Cliente {
            get { return cliente; }
            set { cliente = value; }
        }

        //Con este objeto, el cliente le da al servidor aquello que quiere que haga con el comando anterior
        public T Objeto
        {
            get { return objeto; }
            set { objeto = value; }
        }
    }
}
