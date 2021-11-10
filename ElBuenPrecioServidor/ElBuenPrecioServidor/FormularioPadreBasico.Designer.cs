namespace ElBuenPrecioServidor
{
    partial class FormularioPadreBasico
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
            this.Boton = new System.Windows.Forms.Button();
            this.LLAtrás = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // Boton
            // 
            this.Boton.Location = new System.Drawing.Point(139, 42);
            this.Boton.Name = "Boton";
            this.Boton.Size = new System.Drawing.Size(104, 39);
            this.Boton.TabIndex = 19;
            this.Boton.Text = "Agregar";
            this.Boton.UseVisualStyleBackColor = true;
            // 
            // LLAtrás
            // 
            this.LLAtrás.AutoSize = true;
            this.LLAtrás.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LLAtrás.Location = new System.Drawing.Point(41, 291);
            this.LLAtrás.Name = "LLAtrás";
            this.LLAtrás.Size = new System.Drawing.Size(56, 16);
            this.LLAtrás.TabIndex = 18;
            this.LLAtrás.TabStop = true;
            this.LLAtrás.Text = "Atrás...";
            this.LLAtrás.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LLAtrás_LinkClicked);
            // 
            // FormularioPadreBasico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(343, 372);
            this.ControlBox = false;
            this.Controls.Add(this.Boton);
            this.Controls.Add(this.LLAtrás);
            this.Name = "FormularioPadreBasico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormularioPadreBasico";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button Boton;
        public System.Windows.Forms.LinkLabel LLAtrás;
    }
}