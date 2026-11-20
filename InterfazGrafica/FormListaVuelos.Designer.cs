namespace InterfazGrafica
{
    partial class FormListaVuelos
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
            dgvLista = new DataGridView();
            lblTotal = new Label();
            ID = new DataGridViewTextBoxColumn();
            Compañía = new DataGridViewTextBoxColumn();
            Velocidad = new DataGridViewTextBoxColumn();
            actual = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvLista).BeginInit();
            SuspendLayout();
            // 
            // dgvLista
            // 
            dgvLista.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLista.Columns.AddRange(new DataGridViewColumn[] { ID, Compañía, Velocidad, actual });
            dgvLista.Location = new Point(300, 144);
            dgvLista.Name = "dgvLista";
            dgvLista.RowHeadersWidth = 82;
            dgvLista.Size = new Size(878, 544);
            dgvLista.TabIndex = 0;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(692, 53);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(78, 32);
            lblTotal.TabIndex = 1;
            lblTotal.Text = "label1";
            // 
            // ID
            // 
            ID.HeaderText = "ID";
            ID.MinimumWidth = 10;
            ID.Name = "ID";
            ID.Width = 200;
            // 
            // Compañía
            // 
            Compañía.HeaderText = "Compañía";
            Compañía.MinimumWidth = 10;
            Compañía.Name = "Compañía";
            Compañía.Width = 200;
            // 
            // Velocidad
            // 
            Velocidad.HeaderText = "Velocidad";
            Velocidad.MinimumWidth = 10;
            Velocidad.Name = "Velocidad";
            Velocidad.Width = 200;
            // 
            // actual
            // 
            actual.HeaderText = "Posición Actual";
            actual.MinimumWidth = 10;
            actual.Name = "actual";
            actual.Width = 200;
            // 
            // FormListaVuelos
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1448, 785);
            Controls.Add(lblTotal);
            Controls.Add(dgvLista);
            Name = "FormListaVuelos";
            Text = "Lista de vuelos";
            ((System.ComponentModel.ISupportInitialize)dgvLista).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvLista;
        private Label lblTotal;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Compañía;
        private DataGridViewTextBoxColumn Velocidad;
        private DataGridViewTextBoxColumn actual;
    }
}