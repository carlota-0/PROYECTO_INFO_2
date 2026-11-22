namespace InterfazGrafica
{
    partial class FormDatosVuelos
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
            dgvVuelos = new DataGridView();
            IDColumn = new DataGridViewTextBoxColumn();
            VelocidadColumn = new DataGridViewTextBoxColumn();
            PosiciónColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvVuelos).BeginInit();
            SuspendLayout();
            // 
            // dgvVuelos
            // 
            dgvVuelos.AllowUserToAddRows = false;
            dgvVuelos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVuelos.Columns.AddRange(new DataGridViewColumn[] { IDColumn, VelocidadColumn, PosiciónColumn });
            dgvVuelos.Location = new Point(49, 44);
            dgvVuelos.Name = "dgvVuelos";
            dgvVuelos.ReadOnly = true;
            dgvVuelos.RowHeadersWidth = 62;
            dgvVuelos.Size = new Size(513, 266);
            dgvVuelos.TabIndex = 0;
            dgvVuelos.CellClick += dgvVuelos_CellClick;
            // 
            // IDColumn
            // 
            IDColumn.HeaderText = "ID";
            IDColumn.MinimumWidth = 8;
            IDColumn.Name = "IDColumn";
            IDColumn.ReadOnly = true;
            IDColumn.Width = 150;
            // 
            // VelocidadColumn
            // 
            VelocidadColumn.HeaderText = "Velocidad";
            VelocidadColumn.MinimumWidth = 8;
            VelocidadColumn.Name = "VelocidadColumn";
            VelocidadColumn.ReadOnly = true;
            VelocidadColumn.Width = 150;
            // 
            // PosiciónColumn
            // 
            PosiciónColumn.HeaderText = "Posición";
            PosiciónColumn.MinimumWidth = 8;
            PosiciónColumn.Name = "PosiciónColumn";
            PosiciónColumn.ReadOnly = true;
            PosiciónColumn.Width = 150;
            // 
            // FormDatosVuelos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(703, 450);
            Controls.Add(dgvVuelos);
            Name = "FormDatosVuelos";
            Text = "FormDatosVuelos";
            Load += FormDatosVuelos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvVuelos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvVuelos;
        private DataGridViewTextBoxColumn IDColumn;
        private DataGridViewTextBoxColumn VelocidadColumn;
        private DataGridViewTextBoxColumn PosiciónColumn;
    }
}