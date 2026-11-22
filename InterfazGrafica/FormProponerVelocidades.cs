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
    public partial class FormProponerVelocidades : Form
    {
        private FlightPlanList listaVuelos;
        private double[] propuestas;
        private double distanciaSeg;

        // Recibe la lista de vuelos, las velocidades propuestas y los conflictos detectados
        public FormProponerVelocidades(FlightPlanList lista, double[] propuestas,
                                       List<string> conflictos, double distanciaSeg)
        {
            InitializeComponent();

            this.listaVuelos = lista;
            this.propuestas = propuestas;
            this.distanciaSeg = distanciaSeg;

            string texto = "Conflictos detectados:\n";
            for (int i = 0; i < conflictos.Count; i++)
            {
                texto += "• " + conflictos[i] + "\n";
            }
            texto += "\nMarca los vuelos cuya velocidad quieras cambiar. " +
                     "Puedes modificar la velocidad propuesta antes de aplicar.";
            lblInfo.Text = texto;

            CargarTabla();
        }

        // Rellena la tabla con TODOS los vuelos marca aquellos cuya velocidad propuesta varía
        private void CargarTabla()
        {
            if (listaVuelos == null)
            {
                return;
            }
            dgvPropuestas.Rows.Clear();

            for (int i = 0; i < listaVuelos.GetCount(); i++)
            {
                FlightPlan plan = listaVuelos.GetFlightPlan(i);
                // Marcamos la casilla solo si la propuesta cambia la velocidad
                bool cambia = Math.Abs(propuestas[i] - plan.GetVelocidad()) > 0.001;

                dgvPropuestas.Rows.Add(cambia,
                                       plan.GetId(),
                                       plan.GetVelocidad().ToString("F2"),
                                       propuestas[i].ToString("F2"));
            }
        }

        //Valida y aplica las velocidades propuestas de los vuelos seleccionados comprobando si persisten conflictos
        private void btnAplicar_Click(object sender, EventArgs e)
        {
            // Confirmamos cualquier celda que se esté editando
            dgvPropuestas.EndEdit();

            int cantidad = listaVuelos.GetCount();
            double[] originales = new double[cantidad];
            double[] nuevas = new double[cantidad];

            for (int i = 0; i < cantidad; i++)
            {
                FlightPlan plan = listaVuelos.GetFlightPlan(i);
                originales[i] = plan.GetVelocidad();
                nuevas[i] = originales[i];

                bool marcado = Convert.ToBoolean(dgvPropuestas.Rows[i].Cells[0].Value);
                if (marcado)
                {
                    double v;
                    string texto = Convert.ToString(dgvPropuestas.Rows[i].Cells[3].Value);
                    if (texto == null)
                    {
                        texto = "";
                    }

                    if (!double.TryParse(texto, out v) || v <= 0)
                    {
                        MessageBox.Show("La velocidad del vuelo " + plan.GetId() +
                                        " no es válida. Escribe un número mayor que 0.");
                        return;
                    }
                    nuevas[i] = v;
                }
            }

            for (int i = 0; i < cantidad; i++)
            {
                listaVuelos.GetFlightPlan(i).SetVelocidad(nuevas[i]);
            }

            List<string> restantes = listaVuelos.DetectarConflictos(distanciaSeg);
            if (restantes.Count > 0)
            {
                DialogResult r = MessageBox.Show(
                    "Con estas velocidades todavía quedan " + restantes.Count + " conflicto(s).\n" +
                    "¿Quieres continuar igualmente?",
                    "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (r == DialogResult.No)
                {
                    // Devolvemos las velocidades originales y dejamos al usuario seguir eligiendo
                    for (int i = 0; i < cantidad; i++)
                    {
                        listaVuelos.GetFlightPlan(i).SetVelocidad(originales[i]);
                    }
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            MessageBox.Show("Se han aplicado los cambios a las velocidades de los vuelos.");
        }

        //Cierra el formulario sin modificar las velocidades de ningún vuelo
        private void btnSinCambios_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;   // cierra el formulario
            MessageBox.Show("No se han aplicado cambios a las velocidades de los vuelos.");
        }

        private void dgvPropuestas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}