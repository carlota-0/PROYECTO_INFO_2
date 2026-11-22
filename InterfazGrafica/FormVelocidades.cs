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
        private bool aplicado = false;

        // Constructor sin parámetros 
        public FormVelocidades()
        {
            InitializeComponent();
        }

        //Recibe la lista de vuelos desde el formulario de simulación y carga la tabla
        public FormVelocidades(FlightPlanList lista)
        {
            InitializeComponent();
            this.lista = lista;
            CargarDatos();
        }

        //Devuelve true si el usuario ha aplicado cambios de velocidad
        public bool HaAplicado()
        {
            return aplicado;
        }

        //Rellena la tabla con los identificadores y velocidades actuales de los vuelos
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

        //Valida las nuevas velocidades introducidas en la tabla y las aplica a los vuelos
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (lista == null)
            {
                return;
            }

            dgvVelocidades.EndEdit();
            double[] nuevas = new double[lista.GetCount()];

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

            for (int i = 0; i < lista.GetCount(); i++)
            {
                lista.GetFlightPlan(i).SetVelocidad(nuevas[i]);
            }

            lista.Restart();
            aplicado = true;
            MessageBox.Show("Velocidades aplicadas.");
        }

        // Cierra el formulario de modificación de velocidades
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormVelocidades_Load(object sender, EventArgs e)
        {

        }
    }
}