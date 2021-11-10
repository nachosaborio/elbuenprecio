namespace ElBuenPrecioServidor
{
    partial class DevuelveUnProducto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbBusqueda = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Boton
            // 
            this.Boton.Location = new System.Drawing.Point(239, 375);
            this.Boton.Text = "Seleccionar";
            this.Boton.Click += new System.EventHandler(this.Boton_Click);
            // 
            // LLAtrás
            // 
            this.LLAtrás.Location = new System.Drawing.Point(23, 410);
            // 
            // tbBusqueda
            // 
            this.tbBusqueda.Location = new System.Drawing.Point(26, 28);
            this.tbBusqueda.Name = "tbBusqueda";
            this.tbBusqueda.Size = new System.Drawing.Size(395, 20);
            this.tbBusqueda.TabIndex = 21;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(449, 15);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(94, 44);
            this.btnBuscar.TabIndex = 22;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // DevuelveUnProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 439);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.tbBusqueda);
            this.Name = "DevuelveUnProducto";
            this.Text = "DevuelveUnProducto";
            this.Controls.SetChildIndex(this.LLAtrás, 0);
            this.Controls.SetChildIndex(this.Boton, 0);
            this.Controls.SetChildIndex(this.tbBusqueda, 0);
            this.Controls.SetChildIndex(this.btnBuscar, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbBusqueda;
        private System.Windows.Forms.Button btnBuscar;
    }
}