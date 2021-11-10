using ElBuenPrecio.Entidades;
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
    public partial class Main : Form
    {
        public static Cajero esteCajero;
        int user;
        bool conectado = false;
        bool flag = false;

        public Main()
        {
            InitializeComponent();
            btnConectar.Enabled = true;
            lblStatus.ForeColor = Color.Red;
            btnDesconectar.Enabled = false;
            btnAgregarProducto.Enabled = false;
            btnEliminarProducto.Enabled = false;
            btnFacturar.Enabled = false;
            btnLimpiar.Enabled = false;
            lblTotal.Text = "₡0";
        }


        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                //Emula un JOptionPane.ShowInputDialog de Java para obtener el usuario
                int usuario = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox("Ingrese su numero de cedula en formato 123456789." +
                    "\nSi no tiene un usuario, ingrese un cero para proceder con el registro.", "Login"));

                if (!string.IsNullOrWhiteSpace(Convert.ToString(usuario)))
                {
                    if (Comando.Conectar(usuario))
                    {
                        esteCajero = Comando.ObtenerCajero();

                        if (esteCajero.iD == -1)
                        {
                            MessageBox.Show("Su usuario aún no ha sido creado. Debe proceder con el registro.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            RegistrarCajero();
                        }
                        else if (esteCajero.Estado == 0)
                        {
                            MessageBox.Show("Su usuario aún no ha sido activado, pídale al administrador que lo active.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Disconnected();
                        }
                        else
                        {
                            MessageBox.Show("Nos hemos conectado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            user = usuario;
                            lblCodigoCajero.Text = Convert.ToString(user);
                            lblNombreCajero.Text = esteCajero.Nombre;
                            Connected();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No es posible conectar con el servidor, revise que éste esté escuchando clientes.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No puede dejar este espacio en blanco.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {

            }
        }

        private void RegistrarCajero()
        {
            bool flag = true;
            int ID = 0;
            string nombre = null;
            string apellido1 = null;
            string apellido2 = null;
            try
            {
                try
                {
                    ID = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox("Ingrese su numero de cedula en formato 123456789", "Registro"));
                }
                catch (FormatException)
                {
                    MessageBox.Show("La casilla de identificacion solo admite números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Disconnected();
                    flag = false;
                }
                if (flag)
                {
                    nombre = Microsoft.VisualBasic.Interaction.InputBox("Ingrese su(s) nombre(s) (sin apellidos)");
                    apellido1 = Microsoft.VisualBasic.Interaction.InputBox("Ingrese su primero apellido");
                    apellido2 = Microsoft.VisualBasic.Interaction.InputBox("Ingrese su segundo apellido");
                }
                if (ID == 0 || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido1) || string.IsNullOrWhiteSpace(apellido2)) {
                    MessageBox.Show("Registro cancelado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Disconnected();
                }
                else
                {
                    Cajero cajero = new Cajero(ID, nombre, apellido1, apellido2, 0, 0);
                    Comando.EnviarCajero(cajero);
                    if (Comando.EscucharRespuesta())
                    {
                        MessageBox.Show("Usuario registrado. Pídale al administrador que active su usuario.",
                            "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Disconnected();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al registrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Disconnected();
                    }
                }
            }
            //Este es para que no envíe un objeto desechado y a medio hacer
            catch (ObjectDisposedException) { }
        }


        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            Disconnected();
        }

        private void Disconnected()
        {
            btnConectar.Enabled = true;
            btnDesconectar.Enabled = false;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = "Desconectado";
            btnAgregarProducto.Enabled = false;
            btnEliminarProducto.Enabled = false;
            btnFacturar.Enabled = false;
            Comando.Desconectar(esteCajero);
            btnLimpiar.Enabled = false;
            lblCodigoCajero.Text = "0";
            lblNombreCajero.Text = "";
            flag = false;
        }

        private void Connected()
        {
            btnConectar.Enabled = false;
            btnDesconectar.Enabled = true;
            lblStatus.ForeColor = Color.Green;
            lblStatus.Text = "Conectado al servidor en 127.0.0.1 :  16830";
            btnAgregarProducto.Enabled = true;
            btnEliminarProducto.Enabled = true;
            btnFacturar.Enabled = true;
            btnLimpiar.Enabled = true;
            flag = true;
        }

        private void horafecha_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flag == true)
                Disconnected();
            this.Dispose();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            DevuelveUnProducto unProducto = new DevuelveUnProducto();

            if (unProducto.ShowDialog() == DialogResult.OK)
            {
                Producto producto = unProducto.Producto;
                tbID.Text = Convert.ToString(producto.IdProducto);
                tbDescripcion.Text = producto.DescripcionProducto;
                tbPrecioUnitario.Text = Convert.ToString(producto.Precio);
                tbCantidad.Focus();
            }
        }

        public static int contadorDeFilas = 0;
        public static decimal total = 0;

        private void btEjecutar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbCantidad.Text) && !string.IsNullOrWhiteSpace(tbID.Text))
            {
                bool existe = false;
                int numFila = 0;
                VerificadorDeStock verificador = new VerificadorDeStock(Convert.ToInt32(tbID.Text), Convert.ToInt32(tbCantidad.Text));

                if (Comando.ConsultarExistencia(esteCajero, verificador))
                {

                    if (contadorDeFilas == 0)
                    {
                        dgvVista.Rows.Add(tbID.Text, tbDescripcion.Text, tbPrecioUnitario.Text, tbCantidad.Text);
                        decimal importe = Convert.ToDecimal(dgvVista.Rows[contadorDeFilas].Cells[2].Value)
                            * Convert.ToDecimal(dgvVista.Rows[contadorDeFilas].Cells[3].Value);
                        dgvVista.Rows[contadorDeFilas].Cells[4].Value = importe;
                        contadorDeFilas++;
                    }
                    else
                    {
                        foreach (DataGridViewRow fila in dgvVista.Rows)
                        {
                            if (fila.Cells[0].Value.ToString() == tbID.Text)
                            {
                                existe = true;
                                numFila = fila.Index;
                            }
                        }

                        if (existe)
                        {
                            dgvVista.Rows[numFila].Cells[3].Value = Convert.ToString(Convert.ToDecimal(tbCantidad.Text) +
                                Convert.ToDecimal(dgvVista.Rows[numFila].Cells[3].Value));
                            decimal importe = Convert.ToDecimal(dgvVista.Rows[numFila].Cells[2].Value)
                            * Convert.ToDecimal(dgvVista.Rows[numFila].Cells[3].Value);
                            dgvVista.Rows[numFila].Cells[4].Value = importe;
                        }
                        else
                        {
                            dgvVista.Rows.Add(tbID.Text, tbDescripcion.Text, tbPrecioUnitario.Text, tbCantidad.Text);
                            decimal importe = Convert.ToDecimal(dgvVista.Rows[contadorDeFilas].Cells[2].Value)
                                * Convert.ToDecimal(dgvVista.Rows[contadorDeFilas].Cells[3].Value);
                            dgvVista.Rows[contadorDeFilas].Cells[4].Value = importe;
                            contadorDeFilas++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No hay suficientes unidades de este producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No hay puede dejar este espacio en blanco.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            total = 0;

            foreach (DataGridViewRow fila in dgvVista.Rows)
            {
                total += Convert.ToDecimal(fila.Cells[4].Value);
            }

            lblTotal.Text = total.ToString();
            tbCantidad.Text = "";
        }//Fin boton ingresar

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (contadorDeFilas > 0)
            {
                //Resta al total el importe del producto seleccionado
                total -= Convert.ToDecimal(dgvVista.Rows[dgvVista.CurrentRow.Index].Cells[4].Value);
                lblTotal.Text = total.ToString();
                //Elimina la fila seleccionada
                dgvVista.Rows.RemoveAt(dgvVista.CurrentRow.Index);
                contadorDeFilas--;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            tbID.Text = "";
            tbDescripcion.Text = "";
            tbPrecioUnitario.Text = "";
            tbCantidad.Text = "";
            lblTotal.Text = "0";
            dgvVista.Rows.Clear();
            contadorDeFilas = 0;
            total = 0;
            btnAgregarProducto.Focus();
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            Calculadora calculadora = new Calculadora(Convert.ToDecimal(lblTotal.Text)) ;
            if (calculadora.ShowDialog() == DialogResult.OK) {
                TodaLaVenta todaLaVenta;
                Venta venta;
                DetalleDeVenta detalle;
                List<DetalleDeVenta> detalleDeVentas = new List<DetalleDeVenta>();

                venta = new Venta(Convert.ToInt32(lblCodigoCajero.Text), Convert.ToDecimal(lblTotal.Text));

                foreach (DataGridViewRow fila in dgvVista.Rows)
                {
                    detalle = new DetalleDeVenta(Convert.ToInt32(fila.Cells[0].Value.ToString()),
                         Convert.ToDecimal(fila.Cells[2].Value.ToString()), Convert.ToInt32(fila.Cells[3].Value.ToString()));
                    detalleDeVentas.Add(detalle);
                }
                todaLaVenta = new TodaLaVenta(venta, detalleDeVentas);
                Comando.Facturar(esteCajero, todaLaVenta);
                Nuevo();
                MessageBox.Show("Venta realizada existosamente","Exito",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Muestra la portada de la tarea
            MessageBox.Show("\nUNIVERSIDAD ESTATAL A DISTANCIA" +
                "\nVICERRECTORÍA ACADÉMICA" +
                "\nESCUELA DE CIENCIAS EXACTAS Y NATURALES" +
                "\nCÁTEDRA DE DESARROLLO DE SISTEMAS" +
                "\nDIPLOMADO EN INFORMÁTICA" +
                "\n\nCódigo: 00830" +
                "\n\nProgramación avanzada" +
                "\n\nCentro Universitario: San Isidro (13)" +
                "\nGrupo: 1" +
                "\n\nEstudiante: Sergio Ignacio Saborío Segura" +
                "\nCédula: 1-1717-0701" +
                "\n\nPROYECTO FINAL DEL SEGUNDO CUATRIMESTRE, 2020", "Acerca de",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
