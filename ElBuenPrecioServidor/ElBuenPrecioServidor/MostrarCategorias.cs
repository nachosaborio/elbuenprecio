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
    public partial class MostrarCategorias : FormularioPadreConDataGridView
    {
        Datos datos = new Datos();

        public MostrarCategorias()
        {
            InitializeComponent();
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            //LLena el DataGridView con la lista de categorías
            List<Categoria> listaCategorias = datos.ObtenerCategorias();
            dgvVista.DataSource = listaCategorias;
        }
    }
}
