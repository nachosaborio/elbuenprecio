namespace ElBuenPrecioServidor
{
    partial class VentanaServidor
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentanaServidor));
            this.btnTodos = new System.Windows.Forms.Button();
            this.btnPorCategoria = new System.Windows.Forms.Button();
            this.btnImprimeCategorias = new System.Windows.Forms.Button();
            this.btnAgregarCajero = new System.Windows.Forms.Button();
            this.btnImprimeCajeros = new System.Windows.Forms.Button();
            this.btnAgregarCategoria = new System.Windows.Forms.Button();
            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblClientesConectados = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnEncender = new System.Windows.Forms.Button();
            this.btnApagar = new System.Windows.Forms.Button();
            this.btnInventario = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtBitacora = new System.Windows.Forms.ListBox();
            this.txtClientesConectados = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTodos
            // 
            this.btnTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTodos.ForeColor = System.Drawing.Color.Green;
            this.btnTodos.Location = new System.Drawing.Point(804, 317);
            this.btnTodos.Name = "btnTodos";
            this.btnTodos.Size = new System.Drawing.Size(162, 44);
            this.btnTodos.TabIndex = 8;
            this.btnTodos.Text = "Mostrar todos los productos";
            this.btnTodos.UseVisualStyleBackColor = true;
            this.btnTodos.Click += new System.EventHandler(this.btnTodos_Click);
            // 
            // btnPorCategoria
            // 
            this.btnPorCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPorCategoria.ForeColor = System.Drawing.Color.Green;
            this.btnPorCategoria.Location = new System.Drawing.Point(611, 317);
            this.btnPorCategoria.Name = "btnPorCategoria";
            this.btnPorCategoria.Size = new System.Drawing.Size(162, 44);
            this.btnPorCategoria.TabIndex = 7;
            this.btnPorCategoria.Text = "Mostrar productos por categoría";
            this.btnPorCategoria.UseVisualStyleBackColor = true;
            this.btnPorCategoria.Click += new System.EventHandler(this.btnPorCategoria_Click);
            // 
            // btnImprimeCategorias
            // 
            this.btnImprimeCategorias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimeCategorias.ForeColor = System.Drawing.Color.Green;
            this.btnImprimeCategorias.Location = new System.Drawing.Point(804, 232);
            this.btnImprimeCategorias.Name = "btnImprimeCategorias";
            this.btnImprimeCategorias.Size = new System.Drawing.Size(162, 28);
            this.btnImprimeCategorias.TabIndex = 6;
            this.btnImprimeCategorias.Text = "Mostrar categorías";
            this.btnImprimeCategorias.UseVisualStyleBackColor = true;
            this.btnImprimeCategorias.Click += new System.EventHandler(this.btnImprimeCategorias_Click);
            // 
            // btnAgregarCajero
            // 
            this.btnAgregarCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarCajero.ForeColor = System.Drawing.Color.Green;
            this.btnAgregarCajero.Location = new System.Drawing.Point(611, 66);
            this.btnAgregarCajero.Name = "btnAgregarCajero";
            this.btnAgregarCajero.Size = new System.Drawing.Size(162, 28);
            this.btnAgregarCajero.TabIndex = 2;
            this.btnAgregarCajero.Text = "Activar cajeros";
            this.btnAgregarCajero.UseVisualStyleBackColor = true;
            this.btnAgregarCajero.Click += new System.EventHandler(this.btnAgregarCajero_Click);
            // 
            // btnImprimeCajeros
            // 
            this.btnImprimeCajeros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimeCajeros.ForeColor = System.Drawing.Color.Green;
            this.btnImprimeCajeros.Location = new System.Drawing.Point(611, 232);
            this.btnImprimeCajeros.Name = "btnImprimeCajeros";
            this.btnImprimeCajeros.Size = new System.Drawing.Size(162, 28);
            this.btnImprimeCajeros.TabIndex = 5;
            this.btnImprimeCajeros.Text = "Mostrar cajeros";
            this.btnImprimeCajeros.UseVisualStyleBackColor = true;
            this.btnImprimeCajeros.Click += new System.EventHandler(this.btnImprimeCajeros_Click);
            // 
            // btnAgregarCategoria
            // 
            this.btnAgregarCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarCategoria.ForeColor = System.Drawing.Color.Green;
            this.btnAgregarCategoria.Location = new System.Drawing.Point(804, 66);
            this.btnAgregarCategoria.Name = "btnAgregarCategoria";
            this.btnAgregarCategoria.Size = new System.Drawing.Size(162, 28);
            this.btnAgregarCategoria.TabIndex = 3;
            this.btnAgregarCategoria.Text = "Registro de categorías";
            this.btnAgregarCategoria.UseVisualStyleBackColor = true;
            this.btnAgregarCategoria.Click += new System.EventHandler(this.btnAgregarCategoria_Click);
            // 
            // btnAgregarProducto
            // 
            this.btnAgregarProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarProducto.ForeColor = System.Drawing.Color.Green;
            this.btnAgregarProducto.Location = new System.Drawing.Point(611, 146);
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.Size = new System.Drawing.Size(162, 28);
            this.btnAgregarProducto.TabIndex = 4;
            this.btnAgregarProducto.Text = "Registro de productos";
            this.btnAgregarProducto.UseVisualStyleBackColor = true;
            this.btnAgregarProducto.Click += new System.EventHandler(this.btnAgregarProducto_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Clientes conectados:";
            // 
            // lblClientesConectados
            // 
            this.lblClientesConectados.AutoSize = true;
            this.lblClientesConectados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientesConectados.Location = new System.Drawing.Point(423, 175);
            this.lblClientesConectados.Name = "lblClientesConectados";
            this.lblClientesConectados.Size = new System.Drawing.Size(14, 13);
            this.lblClientesConectados.TabIndex = 16;
            this.lblClientesConectados.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Estado del servidor:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(130, 36);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 13);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Apagado";
            // 
            // btnEncender
            // 
            this.btnEncender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEncender.ForeColor = System.Drawing.Color.Green;
            this.btnEncender.Location = new System.Drawing.Point(27, 71);
            this.btnEncender.Name = "btnEncender";
            this.btnEncender.Size = new System.Drawing.Size(75, 23);
            this.btnEncender.TabIndex = 0;
            this.btnEncender.Text = "Encender";
            this.btnEncender.UseVisualStyleBackColor = true;
            this.btnEncender.Click += new System.EventHandler(this.btnEncender_Click);
            // 
            // btnApagar
            // 
            this.btnApagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApagar.ForeColor = System.Drawing.Color.Red;
            this.btnApagar.Location = new System.Drawing.Point(133, 71);
            this.btnApagar.Name = "btnApagar";
            this.btnApagar.Size = new System.Drawing.Size(75, 23);
            this.btnApagar.TabIndex = 1;
            this.btnApagar.Text = "Apagar";
            this.btnApagar.UseVisualStyleBackColor = true;
            this.btnApagar.Click += new System.EventHandler(this.btnApagar_Click);
            // 
            // btnInventario
            // 
            this.btnInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventario.ForeColor = System.Drawing.Color.Green;
            this.btnInventario.Location = new System.Drawing.Point(804, 146);
            this.btnInventario.Name = "btnInventario";
            this.btnInventario.Size = new System.Drawing.Size(162, 28);
            this.btnInventario.TabIndex = 17;
            this.btnInventario.Text = "Actualizar inventario";
            this.btnInventario.UseVisualStyleBackColor = true;
            this.btnInventario.Click += new System.EventHandler(this.btnInventario_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(351, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(184, 173);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Bitácora:";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Green;
            this.button2.Location = new System.Drawing.Point(611, 403);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(355, 43);
            this.button2.TabIndex = 21;
            this.button2.Text = "Consultar detalle de una factura";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtBitacora
            // 
            this.txtBitacora.FormattingEnabled = true;
            this.txtBitacora.Location = new System.Drawing.Point(27, 203);
            this.txtBitacora.Name = "txtBitacora";
            this.txtBitacora.ScrollAlwaysVisible = true;
            this.txtBitacora.Size = new System.Drawing.Size(279, 264);
            this.txtBitacora.TabIndex = 22;
            // 
            // txtClientesConectados
            // 
            this.txtClientesConectados.FormattingEnabled = true;
            this.txtClientesConectados.Location = new System.Drawing.Point(324, 203);
            this.txtClientesConectados.Name = "txtClientesConectados";
            this.txtClientesConectados.Size = new System.Drawing.Size(239, 264);
            this.txtClientesConectados.TabIndex = 23;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1018, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.acercaDeToolStripMenuItem.Text = "Acerca de...";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // VentanaServidor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1018, 496);
            this.Controls.Add(this.txtClientesConectados);
            this.Controls.Add(this.txtBitacora);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnInventario);
            this.Controls.Add(this.btnApagar);
            this.Controls.Add(this.btnEncender);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblClientesConectados);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnTodos);
            this.Controls.Add(this.btnPorCategoria);
            this.Controls.Add(this.btnImprimeCategorias);
            this.Controls.Add(this.btnAgregarCajero);
            this.Controls.Add(this.btnImprimeCajeros);
            this.Controls.Add(this.btnAgregarCategoria);
            this.Controls.Add(this.btnAgregarProducto);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "VentanaServidor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "El Buen Precio - Servidor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTodos;
        private System.Windows.Forms.Button btnPorCategoria;
        private System.Windows.Forms.Button btnImprimeCategorias;
        private System.Windows.Forms.Button btnAgregarCajero;
        private System.Windows.Forms.Button btnImprimeCajeros;
        private System.Windows.Forms.Button btnAgregarCategoria;
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblClientesConectados;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnEncender;
        private System.Windows.Forms.Button btnApagar;
        private System.Windows.Forms.Button btnInventario;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox txtBitacora;
        private System.Windows.Forms.ListBox txtClientesConectados;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
    }
}

