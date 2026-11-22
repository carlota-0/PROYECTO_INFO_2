using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InterfazGrafica
{
    public partial class FormListaVuelos : Form
    {
        private FlightPlanList listaVuelos;

        // Recibe la lista de vuelos desde FormSimulacion
        public FormListaVuelos(FlightPlanList lista)
        {
            InitializeComponent();
            this.listaVuelos = lista;
            CargarTabla();
        }

        // Añade una fila por cada vuelo del espacio aéreo y actualiza el contador
        private void CargarTabla()
        {
            if (listaVuelos == null)
            {
                return;
            }

            for (int i = 0; i < listaVuelos.GetCount(); i++)
            {
                FlightPlan plan = listaVuelos.GetFlightPlan(i);

                string actual = "(" + plan.GetCurrentPosition().GetX().ToString("F2") + ", " +
                                      plan.GetCurrentPosition().GetY().ToString("F2") + ")";

                dgvLista.Rows.Add(plan.GetId(), plan.GetCompany(),
                                  plan.GetVelocidad().ToString("F2"), actual);
            }

            dgvLista.ClearSelection();
            lblTotal.Text = "Total de vuelos: " + listaVuelos.GetCount();
        }

        private void FormListaVuelos_Load(object sender, EventArgs e)
        {

        }
    }
}
