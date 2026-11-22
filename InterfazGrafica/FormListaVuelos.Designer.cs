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
            ID = new DataGridViewTextBoxColumn();
            Compañía = new DataGridViewTextBoxColumn();
            Velocidad = new DataGridViewTextBoxColumn();
            actual = new DataGridViewTextBoxColumn();
            lblTotal = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvLista).BeginInit();
            SuspendLayout();
            // 
            // dgvLista
            // 
            dgvLista.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLista.Columns.AddRange(new DataGridViewColumn[] { ID, Compañía, Velocidad, actual });
            dgvLista.Location = new Point(216, 84);
            dgvLista.Margin = new Padding(2);
            dgvLista.Name = "dgvLista";
            dgvLista.RowHeadersWidth = 82;
            dgvLista.Size = new Size(675, 425);
            dgvLista.TabIndex = 0;
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
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(553, 35);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(59, 25);
            lblTotal.TabIndex = 1;
            lblTotal.Text = "label1";
            // 
            // FormListaVuelos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1114, 613);
            Controls.Add(lblTotal);
            Controls.Add(dgvLista);
            Margin = new Padding(2);
            Name = "FormListaVuelos";
            Text = "Lista de vuelos";
            Load += FormListaVuelos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLista).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvLista;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Compañía;
        private DataGridViewTextBoxColumn Velocidad;
        private DataGridViewTextBoxColumn actual;
        private Label lblTotal;
    }
}