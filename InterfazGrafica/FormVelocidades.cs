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
    public partial class FormVelocidades : Form
    {
        private FlightPlanList lista;
        private bool aplicado = false;   // se pone a true cuando pulsamos Aplicar

        // Constructor sin parámetros (lo necesita el diseñador de Visual Studio)
        public FormVelocidades()
        {
            InitializeComponent();
        }

        // Constructor que se usa desde FormSimulacion
        public FormVelocidades(FlightPlanList lista)
        {
            InitializeComponent();
            this.lista = lista;
            CargarDatos();
        }

        // Para que FormSimulacion pueda preguntar si se aplicó algo
        public bool HaAplicado()
        {
            return aplicado;
        }

        // Rellena la tabla con los vuelos de la lista
        private void CargarDatos()
        {
            if (lista == null) return;

            dgvVelocidades.Rows.Clear();
            for (int i = 0; i < lista.GetCount(); i++)
            {
                FlightPlan p = lista.GetFlightPlan(i);
                dgvVelocidades.Rows.Add(p.GetId(), p.GetVelocidad().ToString("F2"));
            }
        }



        // Botón Aplicar: lee la tabla y cambia las velocidades, sin cerrar la ventana
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (lista == null) return;

            double[] nuevas = new double[lista.GetCount()];

            // 1º comprobamos que todas las velocidades están bien
            for (int i = 0; i < lista.GetCount(); i++)
            {
                string texto = Convert.ToString(dgvVelocidades.Rows[i].Cells["colVelocidad"].Value);

                if (!double.TryParse(texto, out nuevas[i]))
                {
                    MessageBox.Show("Vuelo " + lista.GetFlightPlan(i).GetId() + ": escribe un número");
                    return;
                }

                if (nuevas[i] <= 0)
                {
                    MessageBox.Show("Vuelo " + lista.GetFlightPlan(i).GetId() + ": la velocidad debe ser mayor que 0");
                    return;
                }
            }

            // 2º si todo está bien, cambiamos las velocidades
            for (int i = 0; i < lista.GetCount(); i++)
            {
                lista.GetFlightPlan(i).SetVelocidad(nuevas[i]);
            }

            lista.Restart();
            aplicado = true;
            MessageBox.Show("Velocidades aplicadas.");
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
            lista.Restart();
        }
    }
}