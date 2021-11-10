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

namespace ElBuenPrecioCliente
{
    public partial class DevuelveUnProducto : Form
    {
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
            //Llena el DataGridView con la lista de cajeros
            List<Producto> listaProductos = Comando.PedirTodosLosProductos(Main.esteCajero);
            dgvVista.DataSource = listaProductos;
            dgvVista.Columns["Categoria"].Visible = false;
        }


        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            Producto = (Producto)dgvVista.CurrentRow.DataBoundItem;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbBusqueda.Text))
            {
                List<Producto> listadeProductos = Comando.PedirAlgunosProductos(Main.esteCajero,tbBusqueda.Text);
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
    }
}
