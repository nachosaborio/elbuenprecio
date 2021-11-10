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
using System.Data.SqlClient;

namespace ElBuenPrecioServidor
{
    public partial class ActualizarCajeros : FormularioPadreBasico
    {
        Cajero cajero;
        Datos datos = new Datos();

        public ActualizarCajeros()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DevuelveUnCajero unCajero = new DevuelveUnCajero();
            if (unCajero.ShowDialog() == DialogResult.OK) {
                cajero = unCajero.Cajero;
                tbID.Text = Convert.ToString(cajero.iD);
                tbNombre.Text = cajero.Nombre;
                tbApellido1.Text = cajero.Apellido1;
                tbApellido2.Text = cajero.Apellido2;
                tbCaja.Text = Convert.ToString(cajero.Caja);
                if (cajero.Estado == 0)
                {
                    cbStatus.SelectedIndex = cbStatus.FindStringExact("Desactivado");
                }
                else {
                    cbStatus.SelectedIndex = cbStatus.FindStringExact("Activado");
                }
            }
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbCaja.Text) > 0)
                {
                    cajero = new Cajero(Convert.ToInt32(tbID.Text), tbNombre.Text, tbApellido1.Text, tbApellido2.Text, Convert.ToInt32(tbCaja.Text),
                        (String)cbStatus.SelectedItem == "Desactivado" ? 0 : 1);
                    if (datos.ObtenerUnCajero(cajero.iD).iD == -1)
                    {
                        datos.IngresarCajero(cajero);
                        MessageBox.Show(this, "Cajero agregado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        datos.ActualizarCajero(cajero);
                        MessageBox.Show(this, "Cajero actualizado con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tbID.Text = ""; tbNombre.Text = ""; tbApellido1.Text = ""; tbApellido2.Text = ""; tbCaja.Text = "";
                    }
                }
                else
                    MessageBox.Show("La caja asignada no puede ser cero o negativo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException) {
                if(string.IsNullOrEmpty(tbID.Text)|| string.IsNullOrEmpty(tbNombre.Text) || string.IsNullOrEmpty(tbApellido1.Text) ||
                    string.IsNullOrEmpty(tbApellido2.Text) || string.IsNullOrEmpty(tbCaja.Text))
                    MessageBox.Show("No puede dejar espacios en blanco.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                MessageBox.Show("Las casillas de identificacion y caja solo admiten números enteros.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
