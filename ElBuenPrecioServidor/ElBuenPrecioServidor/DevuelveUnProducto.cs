using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElBuenPrecio.Entidades;
using ElBuenPrecio.ADO;

namespace ElBuenPrecioServidor
{
    public partial class DevuelveUnProducto : FormularioPadreConDataGridView
    {
        Datos datos = new Datos();
        private Producto producto;

        public Producto Producto
        {
            //Devuelve el cajero seleccionado en el DataGridView
            get { return producto; }
            set { producto = value; }
        }


        public DevuelveUnProducto()
        {
            InitializeComponent();
            LLAtrás.Visible = false;
            //Llena el DataGridView con la lista de cajeros
            List<Producto> listaProductos = datos.ObtenerTodosLosProductos();
            dgvVista.DataSource = listaProductos;
            dgvVista.Columns["Categoria"].Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbBusqueda.Text)) {
                List<Producto> listadeProductos = datos.ObtenerProductosPorNombre(tbBusqueda.Text.Trim());
                dgvVista.DataSource = listadeProductos;
                if (listadeProductos.Any())
                {
                    dgvVista.DataSource = listadeProductos;
                    dgvVista.Columns["Categoria"].Visible = false;
                }
                else
                {
                    MessageBox.Show("No se encontró nada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                return;
            }
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            Producto = (Producto)dgvVista.CurrentRow.DataBoundItem;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
