using FlightLib;
using System.Numerics;

namespace InterfazGrafica
{
    public partial class Form1 : Form
    {
        private FlightPlan plan1;
        private FlightPlan plan2;
        public Form1()
        {
            InitializeComponent();
        }

        private void menuOpciones_Click(object sender, EventArgs e)
        {

        }

        private void menuPlanesVuelo_Click(object sender, EventArgs e)
        {
            // Creamos el formulario de planes de vuelo
            FormPlanesVuelo f = new FormPlanesVuelo();

            // Lo abrimos y esperamos que se cierre
            DialogResult resultado = f.ShowDialog();

            // Si ha dado Aceptar, recogemos los dos planes
            if (resultado == DialogResult.OK)
            {
                plan1 = f.Plan1;
                plan2 = f.Plan2;
                MessageBox.Show("Planes de vuelo guardados correctamente");
            }

        }

        private void menuParametros_Click(object sender, EventArgs e)
        {
            FormParametros p = new FormParametros();
            DialogResult resultado = p.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                // Guardamos los parámetros
                double distanciaSeguridad = p.DistanciaSeguridad;
                double tiempoCiclo = p.TiempoCiclo;
                MessageBox.Show($"Parámetros guardados correctamente:\nDistancia de seguridad: {distanciaSeguridad}\nTiempo de ciclo: {tiempoCiclo}");
            }
        }
    }
}
