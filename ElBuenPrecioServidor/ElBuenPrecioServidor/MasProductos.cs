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
using System.Data.SqlClient;
using ElBuenPrecio.ADO;

namespace ElBuenPrecioServidor
{
    public partial class MasProductos : FormularioPadreBasico
    {
        Datos datos = new Datos();
        public MasProductos()
        {
            InitializeComponent();
            List<Categoria> listaCategorias = datos.ObtenerCategorias();
            cbCategoria.DisplayMember = "descripcionCategoria";
            cbCategoria.DataSource = listaCategorias;
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            try
            {
                //Evita que haya campos en blanco
                if (string.IsNullOrEmpty(tbID.Text) || string.IsNullOrEmpty(tbDescripcion.Text) || string.IsNullOrEmpty(tbPrecio.Text)
                    || string.IsNullOrEmpty(tbStock.Text))
                {
                    MessageBox.Show("No puede dejar espacios en blanco.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Si todo está bien, regista el producto en la Base de Datos
                    Categoria categoria = (Categoria)cbCategoria.SelectedItem;
                    Producto producto = new Producto(Convert.ToInt32(tbID.Text), tbDescripcion.Text, Convert.ToDecimal(tbPrecio.Text),
                        Convert.ToInt32(tbStock.Text), categoria);
                    datos.IngresarProducto(producto);
                    MessageBox.Show("Producto agregado con éxito.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbID.Text = ""; tbDescripcion.Text = ""; tbPrecio.Text = ""; tbStock.Text = "";
                }
            }
            //Evita que se introduca caracteres en espacios numéricos
            catch (FormatException)
            {
                MessageBox.Show("Ingresó algún valor de un tipo incorrecto, por favor intente de nuevo.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Todos los campos, menos la descripción, solo aceptan números. El precio acepta decimales. " +
                    "\n\nIMPORTANTE: Algunas computadoras usan el punto para los decimales, otras usan la coma, si llenó correctamente el formulario " +
                    "y aún así le saltó este error, pruebe usando el otro símbolo para los decimales.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbID.Text = ""; tbDescripcion.Text = ""; tbPrecio.Text = ""; tbStock.Text = "";
            }
            //Evita que se registren dos productos con el mismo ID
            catch (SqlException)
            {
                MessageBox.Show("Ya existe un producto con este código.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
