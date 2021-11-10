namespace ElBuenPrecioServidor
{
    partial class DevuelveUnaVenta
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
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Boton
            // 
            this.Boton.Location = new System.Drawing.Point(228, 379);
            this.Boton.Text = "Seleccionar";
            this.Boton.Click += new System.EventHandler(this.Boton_Click);
            // 
            // LLAtrás
            // 
            this.LLAtrás.Location = new System.Drawing.Point(31, 402);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(116, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(365, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "Seleccione una venta, luego dele clic a seleccionar";
            // 
            // DevuelveUnaVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 432);
            this.Controls.Add(this.label1);
            this.Name = "DevuelveUnaVenta";
            this.Text = "DevuelveUnaVenta";
            this.Load += new System.EventHandler(this.DevuelveUnaVenta_Load);
            this.Controls.SetChildIndex(this.LLAtrás, 0);
            this.Controls.SetChildIndex(this.Boton, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
    }
}