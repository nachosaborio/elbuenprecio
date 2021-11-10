namespace ElBuenPrecioServidor
{
    partial class ActualizarCajeros
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.tbNombre = new System.Windows.Forms.TextBox();
            this.tbApellido1 = new System.Windows.Forms.TextBox();
            this.tbApellido2 = new System.Windows.Forms.TextBox();
            this.tbCaja = new System.Windows.Forms.TextBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Boton
            // 
            this.Boton.Location = new System.Drawing.Point(120, 288);
            this.Boton.Text = "Actualizar";
            this.Boton.Click += new System.EventHandler(this.Boton_Click);
            // 
            // LLAtrás
            // 
            this.LLAtrás.Location = new System.Drawing.Point(23, 344);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(98, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 40);
            this.button1.TabIndex = 20;
            this.button1.Text = "Mostrar cajeros existentes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Identificacion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Nombre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "PrimerApellido";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Segundo apellido";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Caja";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 249);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Estado";
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(138, 83);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(162, 20);
            this.tbID.TabIndex = 27;
            // 
            // tbNombre
            // 
            this.tbNombre.Location = new System.Drawing.Point(138, 114);
            this.tbNombre.Name = "tbNombre";
            this.tbNombre.Size = new System.Drawing.Size(162, 20);
            this.tbNombre.TabIndex = 28;
            // 
            // tbApellido1
            // 
            this.tbApellido1.Location = new System.Drawing.Point(138, 143);
            this.tbApellido1.Name = "tbApellido1";
            this.tbApellido1.Size = new System.Drawing.Size(162, 20);
            this.tbApellido1.TabIndex = 29;
            // 
            // tbApellido2
            // 
            this.tbApellido2.Location = new System.Drawing.Point(138, 180);
            this.tbApellido2.Name = "tbApellido2";
            this.tbApellido2.Size = new System.Drawing.Size(162, 20);
            this.tbApellido2.TabIndex = 30;
            // 
            // tbCaja
            // 
            this.tbCaja.Location = new System.Drawing.Point(138, 216);
            this.tbCaja.Name = "tbCaja";
            this.tbCaja.Size = new System.Drawing.Size(162, 20);
            this.tbCaja.TabIndex = 31;
            // 
            // cbStatus
            // 
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "Desactivado",
            "Activado"});
            this.cbStatus.Location = new System.Drawing.Point(138, 249);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(162, 21);
            this.cbStatus.TabIndex = 32;
            // 
            // ActualizarCajeros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 382);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.tbCaja);
            this.Controls.Add(this.tbApellido2);
            this.Controls.Add(this.tbApellido1);
            this.Controls.Add(this.tbNombre);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "ActualizarCajeros";
            this.Text = "ActualizarCajeros";
            this.Controls.SetChildIndex(this.LLAtrás, 0);
            this.Controls.SetChildIndex(this.Boton, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.tbID, 0);
            this.Controls.SetChildIndex(this.tbNombre, 0);
            this.Controls.SetChildIndex(this.tbApellido1, 0);
            this.Controls.SetChildIndex(this.tbApellido2, 0);
            this.Controls.SetChildIndex(this.tbCaja, 0);
            this.Controls.SetChildIndex(this.cbStatus, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.TextBox tbNombre;
        private System.Windows.Forms.TextBox tbApellido1;
        private System.Windows.Forms.TextBox tbApellido2;
        private System.Windows.Forms.TextBox tbCaja;
        private System.Windows.Forms.ComboBox cbStatus;
    }
}