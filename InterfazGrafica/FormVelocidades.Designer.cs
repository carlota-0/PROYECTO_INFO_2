namespace InterfazGrafica
{
    partial class FormVelocidades
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
            dgvVelocidades = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colVelocidad = new DataGridViewTextBoxColumn();
            textBox1 = new TextBox();
            btnAceptar = new Button();
            btnCerrar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvVelocidades).BeginInit();
            SuspendLayout();
            // 
            // dgvVelocidades
            // 
            dgvVelocidades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVelocidades.Columns.AddRange(new DataGridViewColumn[] { colID, colVelocidad });
            dgvVelocidades.Location = new Point(95, 84);
            dgvVelocidades.Margin = new Padding(2, 2, 2, 2);
            dgvVelocidades.Name = "dgvVelocidades";
            dgvVelocidades.RowHeadersWidth = 82;
            dgvVelocidades.Size = new Size(375, 177);
            dgvVelocidades.TabIndex = 0;
            // 
            // colID
            // 
            colID.HeaderText = "ID";
            colID.MinimumWidth = 10;
            colID.Name = "colID";
            colID.Width = 200;
            // 
            // colVelocidad
            // 
            colVelocidad.HeaderText = "Velocidad";
            colVelocidad.MinimumWidth = 10;
            colVelocidad.Name = "colVelocidad";
            colVelocidad.Width = 200;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(192, 192, 255);
            textBox1.Location = new Point(129, 30);
            textBox1.Margin = new Padding(2, 2, 2, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(276, 31);
            textBox1.TabIndex = 1;
            textBox1.Text = "    Velocidades de los aviones";
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = SystemColors.ButtonHighlight;
            btnAceptar.Location = new Point(502, 102);
            btnAceptar.Margin = new Padding(2, 2, 2, 2);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(145, 55);
            btnAceptar.TabIndex = 2;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(502, 176);
            btnCerrar.Margin = new Padding(2, 2, 2, 2);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(145, 55);
            btnCerrar.TabIndex = 3;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // FormVelocidades
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(805, 439);
            Controls.Add(btnCerrar);
            Controls.Add(btnAceptar);
            Controls.Add(textBox1);
            Controls.Add(dgvVelocidades);
            Margin = new Padding(2, 2, 2, 2);
            Name = "FormVelocidades";
            Text = "FormVelocidades";
            Load += FormVelocidades_Load;
            ((System.ComponentModel.ISupportInitialize)dgvVelocidades).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvVelocidades;
        private TextBox textBox1;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colVelocidad;
        private Button btnAceptar;
        private Button btnCerrar;
    }
}