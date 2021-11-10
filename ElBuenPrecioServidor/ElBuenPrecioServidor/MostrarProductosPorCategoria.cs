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
    public partial class MostrarProductosPorCategoria : FormularioPadreConDataGridView
    {
        Datos datos = new Datos();
        public MostrarProductosPorCategoria()
        {
            InitializeComponent();
            //Llena el combobox con todas las categorías para poder filtrar la búsqueda
            List<Categoria> listaCategorias = datos.ObtenerCategorias();
            cbCategorias.DisplayMember = "descripcionCategoria";
            cbCategorias.DataSource = listaCategorias;
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            Categoria categoria = (Categoria)cbCategorias.SelectedItem;
            //Compara los IDs de la categoría seleccionada con los ID de categoría de cada prducto y solo selecciona los que coinciden
            List<Producto> listaProductos = datos.ObtenerProductosPorCategoria(categoria.IdCategoria);
            //Si esa categoría tiene productos asignados, los muestra, si no, lanza un error
            if (listaProductos.Any())
            {
                dgvVista.DataSource = listaProductos;
                dgvVista.Columns["Categoria"].Visible = false;
            }
            else
            {
                MessageBox.Show("No hay productos en esta categoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
