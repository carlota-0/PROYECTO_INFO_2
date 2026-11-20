namespace InterfazGrafica
{
    partial class FormProponerVelocidades
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
            lblInfo = new Label();
            dgvPropuestas = new DataGridView();
            Aplicar = new DataGridViewCheckBoxColumn();
            Vuelo = new DataGridViewTextBoxColumn();
            Vel1 = new DataGridViewTextBoxColumn();
            Vel2 = new DataGridViewTextBoxColumn();
            btnAplicar = new Button();
            btnSinCambios = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPropuestas).BeginInit();
            SuspendLayout();
            // 
            // lblInfo
            // 
            lblInfo.BackColor = SystemColors.ControlLightLight;
            lblInfo.Location = new Point(199, 42);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(889, 105);
            lblInfo.TabIndex = 0;
            // 
            // dgvPropuestas
            // 
            dgvPropuestas.AllowUserToAddRows = false;
            dgvPropuestas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPropuestas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPropuestas.Columns.AddRange(new DataGridViewColumn[] { Aplicar, Vuelo, Vel1, Vel2 });
            dgvPropuestas.Location = new Point(90, 177);
            dgvPropuestas.Name = "dgvPropuestas";
            dgvPropuestas.RowHeadersVisible = false;
            dgvPropuestas.RowHeadersWidth = 82;
            dgvPropuestas.Size = new Size(1136, 365);
            dgvPropuestas.TabIndex = 1;
            // 
            // Aplicar
            // 
            Aplicar.HeaderText = "Aplicar";
            Aplicar.MinimumWidth = 10;
            Aplicar.Name = "Aplicar";
            // 
            // Vuelo
            // 
            Vuelo.HeaderText = "Vuelo";
            Vuelo.MinimumWidth = 10;
            Vuelo.Name = "Vuelo";
            // 
            // Vel1
            // 
            Vel1.HeaderText = "Velocidad actual";
            Vel1.MinimumWidth = 10;
            Vel1.Name = "Vel1";
            // 
            // Vel2
            // 
            Vel2.HeaderText = "Velocidad Propuesta";
            Vel2.MinimumWidth = 10;
            Vel2.Name = "Vel2";
            // 
            // btnAplicar
            // 
            btnAplicar.Location = new Point(416, 580);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(200, 59);
            btnAplicar.TabIndex = 2;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = true;
            btnAplicar.Click += btnAplicar_Click;
            // 
            // btnSinCambios
            // 
            btnSinCambios.Location = new Point(676, 580);
            btnSinCambios.Name = "btnSinCambios";
            btnSinCambios.Size = new Size(200, 59);
            btnSinCambios.TabIndex = 3;
            btnSinCambios.Text = "Sin cambios";
            btnSinCambios.UseVisualStyleBackColor = true;
            btnSinCambios.Click += btnSinCambios_Click;
            // 
            // FormProponerVelocidades
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1319, 725);
            Controls.Add(btnSinCambios);
            Controls.Add(btnAplicar);
            Controls.Add(dgvPropuestas);
            Controls.Add(lblInfo);
            Name = "FormProponerVelocidades";
            Text = "FormProponerVelocidades";
            ((System.ComponentModel.ISupportInitialize)dgvPropuestas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblInfo;
        private DataGridView dgvPropuestas;
        private DataGridViewCheckBoxColumn Aplicar;
        private DataGridViewTextBoxColumn Vuelo;
        private DataGridViewTextBoxColumn Vel1;
        private DataGridViewTextBoxColumn Vel2;
        private Button btnAplicar;
        private Button btnSinCambios;
    }
}