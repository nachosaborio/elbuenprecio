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
    public partial class DevuelveUnCajero : FormularioPadreConDataGridView
    {
        Datos datos = new Datos();

        public DevuelveUnCajero()
        {
            InitializeComponent();
            LLAtrás.Visible = false;
            //Llena el DataGridView con la lista de cajeros
            List<Cajero> listaCajeros = datos.ObtenerCajeros();
            dgvVista.DataSource = listaCajeros;
        }

        private Cajero cajero;

        public Cajero Cajero
        {
            //Devuelve el cajero seleccionado en el DataGridView
            get { return cajero; }
            set { cajero = value; }
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            Cajero = (Cajero)dgvVista.CurrentRow.DataBoundItem;
            DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
