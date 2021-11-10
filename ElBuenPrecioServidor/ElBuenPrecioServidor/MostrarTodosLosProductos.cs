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
    public partial class MostrarTodosLosProductos : FormularioPadreConDataGridView
    {
        Datos datos = new Datos();
        public MostrarTodosLosProductos()
        {
            InitializeComponent();
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            //Llena el DataGridView con la lista de todos los productos
            List<Producto> listaProductos = datos.ObtenerTodosLosProductos();
            dgvVista.DataSource = listaProductos;
            dgvVista.Columns["Categoria"].Visible = false;
        }
    }
}
