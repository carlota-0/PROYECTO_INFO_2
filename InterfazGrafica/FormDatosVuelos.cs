using System;
using System.Drawing;
using System.Windows.Forms;
using FlightLib;

namespace InterfazGrafica
{
    public class FormDatosVuelos : Form
    {
        private DataGridView dgvVuelos;
        private FlightPlan p1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private FlightPlan p2;

        public FormDatosVuelos(FlightPlan p1, FlightPlan p2)
        {
            this.p1 = p1;
            this.p2 = p2;
            InitializeComponent();
            CargarDatos();
        }

        private void InitializeComponent()
        {
            dgvVuelos = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvVuelos).BeginInit();
            SuspendLayout();
            // 
            // dgvVuelos
            // 
            dgvVuelos.AllowUserToAddRows = false;
            dgvVuelos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVuelos.ColumnHeadersHeight = 34;
            dgvVuelos.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            dgvVuelos.Location = new Point(20, 20);
            dgvVuelos.Name = "dgvVuelos";
            dgvVuelos.RowHeadersVisible = false;
            dgvVuelos.RowHeadersWidth = 62;
            dgvVuelos.Size = new Size(597, 242);
            dgvVuelos.TabIndex = 0;
            dgvVuelos.CellClick += dgvVuelos_CellClick;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.MinimumWidth = 8;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.MinimumWidth = 8;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.MinimumWidth = 8;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // FormDatosVuelos
            // 
            ClientSize = new Size(629, 318);
            Controls.Add(dgvVuelos);
            Name = "FormDatosVuelos";
            Text = "Datos de Vuelos";
            Load += FormDatosVuelos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvVuelos).EndInit();
            ResumeLayout(false);

            // dataGridViewTextBoxColumn1
            dataGridViewTextBoxColumn1.HeaderText = "ID";
            dataGridViewTextBoxColumn1.MinimumWidth = 8;
            dataGridViewTextBoxColumn1.Name = "ID";
            // dataGridViewTextBoxColumn2
            dataGridViewTextBoxColumn2.HeaderText = "Velocidad";
            dataGridViewTextBoxColumn2.MinimumWidth = 8;
            dataGridViewTextBoxColumn2.Name = "Velocidad";
            // dataGridViewTextBoxColumn3
            dataGridViewTextBoxColumn3.HeaderText = "Posición";
            dataGridViewTextBoxColumn3.MinimumWidth = 8;
            dataGridViewTextBoxColumn3.Name = "Posición";
        }

        private void CargarDatos()
        {
            // Añadimos las filas con la información de cada avión
            dgvVuelos.Rows.Add(p1.GetId(), p1.GetVelocidad().ToString("F2"), $"({p1.GetCurrentPosition().GetX():F2}, {p1.GetCurrentPosition().GetY():F2})");
            dgvVuelos.Rows.Add(p2.GetId(), p2.GetVelocidad().ToString("F2"), $"({p2.GetCurrentPosition().GetX():F2}, {p2.GetCurrentPosition().GetY():F2})");
            dgvVuelos.ClearSelection();
        }

        private void dgvVuelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Si el usuario hace clic en una fila de datos (no en el encabezado)
            if (e.RowIndex >= 0)
            {
                double distancia = p1.Distancia(p2);
                MessageBox.Show($"La distancia actual entre el vuelo {p1.GetId()} y el vuelo {p2.GetId()} es de {distancia:F2} unidades.", "Distancia entre vuelos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormDatosVuelos_Load(object sender, EventArgs e)
        {

        }
    }
}