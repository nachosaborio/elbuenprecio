using ElBuenPrecio.ADO;
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

namespace ElBuenPrecioServidor
{
    public partial class MostrarDetalleDeUnaVenta : FormularioPadreConDataGridView
    {
        Datos datos = new Datos();
        public MostrarDetalleDeUnaVenta()
        {
            InitializeComponent();
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            DevuelveUnaVenta unaVenta = new DevuelveUnaVenta();
            if (unaVenta.ShowDialog() == DialogResult.OK)
            {
                Venta venta = unaVenta.Venta;
                dgvVista.DataSource = datos.ObtenerDetalleDeUnaVenta(venta);
                lblTotal.Text = venta.Importe.ToString();
            }
        }
    }
}
