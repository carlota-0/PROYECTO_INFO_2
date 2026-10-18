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
            this.dgvVuelos = new DataGridView();
            this.SuspendLayout();
            
            // Configuración visual de la tabla
            this.dgvVuelos.Location = new Point(20, 20);
            this.dgvVuelos.Size = new Size(450, 150);
            this.dgvVuelos.AllowUserToAddRows = false; 
            this.dgvVuelos.RowHeadersVisible = false;

            // Creamos las columnas
            this.dgvVuelos.ColumnCount = 3;
            this.dgvVuelos.Columns[0].Name = "ID";
            this.dgvVuelos.Columns[1].Name = "Velocidad";
            this.dgvVuelos.Columns[2].Name = "Posición Actual";
            this.dgvVuelos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            // Asignamos el evento de clic en la celda
            this.dgvVuelos.CellClick += new DataGridViewCellEventHandler(this.dgvVuelos_CellClick);

            this.ClientSize = new Size(500, 200);
            this.Controls.Add(this.dgvVuelos);
            this.Text = "Datos de Vuelos";
            this.ResumeLayout(false);
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
    }
}