using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FlightLib;

namespace InterfazGrafica
{
    public partial class FormDatosVuelos : Form
    {
        private FlightPlan p1;
        private FlightPlan p2;
        public FormDatosVuelos()
        {
            InitializeComponent();

        }

        // Recibe los dos planes de vuelo e inicializa la tabla
        public FormDatosVuelos(FlightPlan p1, FlightPlan p2)
        {
            InitializeComponent();
            this.p1 = p1;
            this.p2 = p2;
            CargarDatos();
        }

        // Añade las filas con el identificador, velocidad y posición actual de cada avión
        private void CargarDatos()
        {
            if (p1 == null || p2 == null)
            {
                return;
            }

            dgvVuelos.Rows.Clear();

            string pos1 = "(" + p1.GetCurrentPosition().GetX().ToString("F2") + ", " +
                                p1.GetCurrentPosition().GetY().ToString("F2") + ")";
            string pos2 = "(" + p2.GetCurrentPosition().GetX().ToString("F2") + ", " +
                                p2.GetCurrentPosition().GetY().ToString("F2") + ")";

            dgvVuelos.Rows.Add(p1.GetId(), p1.GetVelocidad().ToString("F2"), pos1);
            dgvVuelos.Rows.Add(p2.GetId(), p2.GetVelocidad().ToString("F2"), pos2);
            dgvVuelos.ClearSelection();
        }

        // Muestra un mensaje con la distancia actual entre ambos vuelos al clicar una fila
        private void dgvVuelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && p1 != null && p2 != null)
            {
                double distancia = p1.Distancia(p2);
                MessageBox.Show("La distancia actual entre el vuelo " + p1.GetId() + " y el vuelo " + p2.GetId() +
                                " es de " + distancia.ToString("F2") + " unidades.",
                                "Distancia entre vuelos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormDatosVuelos_Load(object sender, EventArgs e)
        {

        }
    }
}
