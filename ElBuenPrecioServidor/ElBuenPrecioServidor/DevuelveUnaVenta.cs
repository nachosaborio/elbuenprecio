using ElBuenPrecio.Entidades;
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

namespace ElBuenPrecioServidor
{
    public partial class DevuelveUnaVenta : FormularioPadreConDataGridView
    {
        Datos datos = new Datos();
        public DevuelveUnaVenta()
        {
            InitializeComponent();
        }

        private void DevuelveUnaVenta_Load(object sender, EventArgs e)
        {
            LLAtrás.Visible = false;
            //Llena el DataGridView con la lista de cajeros
            List<Venta> listaCajeros = datos.ObtenerTodasLasVentas();
            dgvVista.DataSource = listaCajeros;
        }

        private Venta venta;

        public Venta Venta
        {
            get { return venta; }
            set { venta = value; }
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            Venta = (Venta)dgvVista.CurrentRow.DataBoundItem;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
