namespace ElBuenPrecioServidor
{
    partial class FormularioPadreConDataGridView
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
            this.dgvVista = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVista)).BeginInit();
            this.SuspendLayout();
            // 
            // Boton
            // 
            this.Boton.Location = new System.Drawing.Point(255, 22);
            this.Boton.Text = "Buscar";
            // 
            // LLAtrás
            // 
            this.LLAtrás.Location = new System.Drawing.Point(30, 387);
            // 
            // dgvVista
            // 
            this.dgvVista.AllowUserToAddRows = false;
            this.dgvVista.AllowUserToDeleteRows = false;
            this.dgvVista.AllowUserToResizeColumns = false;
            this.dgvVista.AllowUserToResizeRows = false;
            this.dgvVista.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVista.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvVista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVista.Location = new System.Drawing.Point(12, 76);
            this.dgvVista.MultiSelect = false;
            this.dgvVista.Name = "dgvVista";
            this.dgvVista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVista.Size = new System.Drawing.Size(567, 284);
            this.dgvVista.TabIndex = 20;
            // 
            // FormularioPadreConDataGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 423);
            this.Controls.Add(this.dgvVista);
            this.Name = "FormularioPadreConDataGridView";
            this.Text = "FormularioPadreConDataGridView";
            this.Controls.SetChildIndex(this.LLAtrás, 0);
            this.Controls.SetChildIndex(this.Boton, 0);
            this.Controls.SetChildIndex(this.dgvVista, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvVista;
    }
}