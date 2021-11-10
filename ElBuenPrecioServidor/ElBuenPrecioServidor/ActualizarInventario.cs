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
    public partial class ActualizarInventario : FormularioPadreBasico
    {
        Producto producto;
        Datos datos = new Datos();

        public ActualizarInventario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DevuelveUnProducto unProducto = new DevuelveUnProducto();
            if (unProducto.ShowDialog() == DialogResult.OK) {
                producto = unProducto.Producto;
                tbID.Text = Convert.ToString(producto.IdProducto);
                tbDescripcion.Text = producto.DescripcionProducto;
                tbPrecio.Text = Convert.ToString(producto.Precio);
                tbStock.Text = Convert.ToString(producto.Stock);
                lblCodigo.Text = Convert.ToString(producto.Categoria.IdCategoria);
                tbCategoria.Text = producto.Categoria.DescripcionCategoria;
            }
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            try
            {
                producto = new Producto(Convert.ToInt32(tbID.Text), tbDescripcion.Text,Convert.ToDecimal(tbPrecio.Text),
                    Convert.ToInt32(tbStock.Text),datos.ObtenerUnaCategoria(Convert.ToInt32(lblCodigo.Text)));
                datos.ActualizarProductos(producto);
                MessageBox.Show(this, "Producto actualizado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("La casilla de stock solo admiten números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
