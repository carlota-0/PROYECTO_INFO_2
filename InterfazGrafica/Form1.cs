using FlightLib;
using System.Numerics;
using System.IO;

namespace InterfazGrafica
{
    public partial class Form1 : Form
    {
        private FlightPlanList listaVuelos = new FlightPlanList();
        private double distanciaSeguridad = 50; // Valor por defecto
        private double tiempoCiclo = 10;
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
                FlightPlan p1 = f.GetPlan1();
                FlightPlan p2 = f.GetPlan2();
                if (p1 != null)
                {
                    listaVuelos.AddFlightPlan(p1);
                }
                if (p2 != null)
                {
                    listaVuelos.AddFlightPlan(p2);
                }
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
                this.distanciaSeguridad = p.DistanciaSeguridad;
                this.tiempoCiclo = p.TiempoCiclo;
                MessageBox.Show($"Parámetros guardados correctamente:\nDistancia de seguridad: {distanciaSeguridad}\nTiempo de ciclo: {tiempoCiclo}");
            }
        }
        private void menuSimulacion_Click(object sender, EventArgs e)
        {
            if (listaVuelos.GetFlightPlan(0) == null)
            {
                MessageBox.Show(" Porfavor, introduce o carga primero los planes de vuelo");
                return; 
            }
            //Li passem la llista sencera d'avions al simualdor
            FormSimulacion formSim = new FormSimulacion(listaVuelos, this.distanciaSeguridad);
            formSim.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cargarPlanesDeVueloDesdeArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de texto (*.txt)|*.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader r = new StreamReader(ofd.FileName);
                    string linea = r.ReadLine();

                    while (linea != null)
                    {
                        if (linea.StartsWith("//") || linea.Trim() == "")
                        {
                            linea = r.ReadLine();
                            continue;
                        }
                        string[] trozos = linea.Split(' ');

                        string id = trozos[0];
                        double ix = Convert.ToDouble(trozos[1]);
                        double iy = Convert.ToDouble(trozos[2]);
                        double fx = Convert.ToDouble(trozos[3]);
                        double fy = Convert.ToDouble(trozos[4]);
                        double vel = Convert.ToDouble(trozos[5]);

                        FlightPlan avion = new FlightPlan(id, ix, iy, fx, fy, vel);

                       listaVuelos.AddFlightPlan(avion);

                        linea = r.ReadLine();
                    }

                    r.Close();
                    MessageBox.Show("¡Vuelos cargados correctamente!");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Error: El fichero tiene un formato incorrecto.");
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Error: No se ha encontrado el fichero.");
                }
            }
        }
    }
}
