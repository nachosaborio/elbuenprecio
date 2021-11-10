using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElBuenPrecio.ADO;
using ElBuenPrecio.Entidades;

namespace ElBuenPrecioServidor
{
    public partial class MostrarCajeros : FormularioPadreConDataGridView
    {
        Datos datos = new Datos();
        public MostrarCajeros()
        {
            InitializeComponent();
        }

        private void Boton_Click_1(object sender, EventArgs e)
        {
            //Llena el DataGridView con la lista de cajeros
            List<Cajero> listaCajeros = datos.ObtenerCajeros();
            dgvVista.DataSource = listaCajeros;
        }

    }
}
