using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElBuenPrecioCliente
{
    public partial class Calculadora : Form
    {
        decimal importe;
        public Calculadora(decimal importe)
        {
            InitializeComponent();
            this.importe = importe;
        }

        private void Calculadora_Load(object sender, EventArgs e)
        {
            lblSubTotal.Text = importe.ToString();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tbPagaCon_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(tbPagaCon.Text) < Convert.ToDecimal(lblSubTotal.Text))
                {
                    btnPagar.Enabled = false;
                    lblTotal.Text = "";
                }
                else
                {
                    lblTotal.Text = Convert.ToString(Convert.ToDecimal(tbPagaCon.Text) - Convert.ToDecimal(lblSubTotal.Text));
                    btnPagar.Enabled = true;
                }
            }
            catch (FormatException) {
                lblTotal.Text = "Sólo números";
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
