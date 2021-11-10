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
    public partial class MasCategorias : FormularioPadreBasico
    {
        Datos datos = new Datos();

        public MasCategorias()
        {
            InitializeComponent();
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            try
            {
                //Evita que se introduzcan espacios en blanco
                if (string.IsNullOrEmpty(tbID.Text) || string.IsNullOrEmpty(tbDescripcion.Text))
                {
                    MessageBox.Show("No puede dejar espacios en blanco.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Si todo está correcto, registra la categoría en la Base de Datos
                else
                {
                    Categoria categoria = new Categoria(Convert.ToInt32(tbID.Text), tbDescripcion.Text);
                    datos.IngresarCategoria(categoria);
                    MessageBox.Show("Categoría agregada con éxito.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbID.Text = ""; tbDescripcion.Text = "";
                }
            }
            //Evita la introducción de caracteres en espacios numéricos
            catch (FormatException)
            {
                MessageBox.Show("Ingresó algún valor de un tipo incorrecto, por favor intente de nuevo.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("El campo de categoria solo acepta números, el de descripción acepta cualquier otro tipo de caracter.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbID.Text = ""; tbDescripcion.Text = "";
            }
            //Evita que se registren dos categorías con la misma identificación
            catch (SqlException)
            {
                MessageBox.Show("Ya existe una categoria con este código.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
