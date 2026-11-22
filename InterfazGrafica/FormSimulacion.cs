
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using FlightLib;

namespace InterfazGrafica
{
    public partial class FormSimulacion : Form
    {
        private double distanciaSeg;
        private FlightPlanList listaVuelos;
        private double tiempoCiclo;
        private List<PictureBox> dibujosAviones = new List<PictureBox>();

        //inicializa el formulario de simulación con la lista de vuelos, distancia de seguridad y tiempo de ciclo
        public FormSimulacion(FlightPlanList listaVuelos, double distancia, double tiempoCiclo)
        {
            this.listaVuelos = listaVuelos;
            this.distanciaSeg = distancia;
            this.tiempoCiclo = tiempoCiclo;


            InitializeComponent();
            ConfigurarAviones();

        }
        private void FormSimulacion_Load(object sender, EventArgs e)
        {
            panelEspacioAereo.BackColor = Color.Black;

            double anchoPanel = panelEspacioAereo.Width;
            double altoPanel = panelEspacioAereo.Height;

            for (int i = 0; i < listaVuelos.GetCount(); i++)
            {
                FlightPlan plan = listaVuelos.GetFlightPlan(i);
                if (plan != null)
                {
                    plan.ExtenderHastaBordeSector(anchoPanel, altoPanel);
                }
            }
            panelEspacioAereo.Invalidate();
        }

        //Crea un PictureBox por cada vuelo de la lista y lo añade al panel del espacio aéreo
        private void ConfigurarAviones()
        {
            for (int i = 0; i < listaVuelos.GetCount(); i++)
            {
                FlightPlan plan = listaVuelos.GetFlightPlan(i);

                if (plan != null)
                {
                    PictureBox pb = new PictureBox();
                    pb.Width = 20;
                    pb.Height = 20;
                    pb.BackColor = Color.Blue;
                    pb.Location = new Point((int)plan.GetInitialPosition().GetX(), (int)plan.GetInitialPosition().GetY());
                    pb.Tag = plan;
                    pb.Click += new EventHandler(this.avion_Click);

                    panelEspacioAereo.Controls.Add(pb);
                    dibujosAviones.Add(pb); 
                }
            }
        }

        //Avanza un ciclo a toda la lista de aviones a la vez 
        private void AvanzarCiclo()
        {
            listaVuelos.Mover(tiempoCiclo);

            bool todosHanLlegado = true;

            for (int i = 0; i < listaVuelos.GetCount(); i++)
            {
                FlightPlan plan = listaVuelos.GetFlightPlan(i);
                PictureBox pb = dibujosAviones[i];

                if (plan != null)
                {
                    pb.Location = new Point((int)plan.GetCurrentPosition().GetX(), (int)plan.GetCurrentPosition().GetY());

                    if (!plan.EstaDestino())
                    {
                        todosHanLlegado = false;
                    }
                }
            }

            ActualizarEstadoAlarma();
            panelEspacioAereo.Invalidate();

            if (timerSimulador.Enabled && todosHanLlegado)
            {
                PararAutomatica();
                MessageBox.Show("Todos los vuelos han llegado a su destino. Simulación terminada.");
            }
        }

        // Comprueba si existe pérdida de separación actual entre algún par de vuelos y actualiza la etiqueta de alarma
        private void ActualizarEstadoAlarma()
        {
            bool hayConflicto = false;

            for (int i = 0; i < listaVuelos.GetCount(); i++)
            {
                FlightPlan plan = listaVuelos.GetFlightPlan(i);
                if (plan != null)
                {
                    for (int j = i + 1; j < listaVuelos.GetCount(); j++)
                    {
                        FlightPlan otroPlan = listaVuelos.GetFlightPlan(j);
                        if (otroPlan != null && plan.Conflicto(otroPlan, distanciaSeg))
                        {
                            hayConflicto = true;
                        }
                    }
                }
            }

            if (hayConflicto)
            {
                lblEstadoAlarma.Text = "¡ALARMA: PÉRDIDA DE SEPARACIÓN ENTRE VUELOS!";
                lblEstadoAlarma.BackColor = Color.Red;
            }
            else
            {
                lblEstadoAlarma.Text = "ESTADO: SECTOR SEGURO";
                lblEstadoAlarma.BackColor = Color.Green;
            }
        }

        //Detiene temporizador automático
        private void PararAutomatica()
        {
            timerSimulador.Stop();
            btnAuto.Enabled = true;
            btnDetener.Enabled = false;
            btnMover.Enabled = true;
        }

        // Devuelve todos los aviones a su posición inicial y actualiza el panel y el estado de alarma
        private void ReiniciarSimulacion()
        {
            for (int i = 0; i < listaVuelos.GetCount(); i++)
            {
                FlightPlan plan = listaVuelos.GetFlightPlan(i);
                if (plan != null)
                {
                    plan.Restart();
                    dibujosAviones[i].Location = new Point((int)plan.GetCurrentPosition().GetX(), (int)plan.GetCurrentPosition().GetY());
                }
            }

            panelEspacioAereo.Invalidate();
            lblEstadoAlarma.Text = "ESTADO: SECTOR SEGURO";
            lblEstadoAlarma.BackColor = Color.Green;
        }

        //Avanza un paso manual en la simulación al pulsar el botón
        private void btnMover_Click(object sender, EventArgs e)
        {
            AvanzarCiclo();
        }

        //Comprueba conflictos futuros antes de arrancar la simulación automática y activa el temporizador
        private void btnAuto_Click(object sender, EventArgs e)
        {
            List<string> conflictos = listaVuelos.DetectarConflictos(distanciaSeg);

            if (conflictos.Count > 0)
            {
                bool resuelto;
                double[] propuestas = listaVuelos.ProponerVelocidades(distanciaSeg, out resuelto);

                if (!resuelto)
                {
                    MessageBox.Show("Aviso: el sistema no ha podido resolver todos los conflictos automáticamente.\n" +
                                    "Aun aplicando las velocidades propuestas quedarán conflictos sin resolver.",
                                    "Conflicto sin resolver", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                FormProponerVelocidades f = new FormProponerVelocidades(listaVuelos, propuestas, conflictos, distanciaSeg);
                f.ShowDialog();
            }

            timerSimulador.Start();
            btnAuto.Enabled = false;
            btnDetener.Enabled = true;
            btnMover.Enabled = false;
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            PararAutomatica();
        }

        private void timerSimulador_Tick(object sender, EventArgs e)
        {
            AvanzarCiclo();
        }

        //Retrocede un paso en el historial de posiciones de todos los vuelos 
        private void btn_Deshacer_Click(object sender, EventArgs e)
        {
            PararAutomatica();
            bool seDeshizo = listaVuelos.DeshacerPaso();

            if (seDeshizo)
            {
                int i = 0;
                while (i < listaVuelos.GetCount())
                {
                    FlightPlan plan = listaVuelos.GetFlightPlan(i);
                    dibujosAviones[i].Location = new Point((int)plan.GetCurrentPosition().GetX(), (int)plan.GetCurrentPosition().GetY());
                    i++;
                }
                ActualizarEstadoAlarma();
                panelEspacioAereo.Invalidate();
            }
            else
            {
                MessageBox.Show("No hay pasos anteriores para deshacer.");
            }

        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            ReiniciarSimulacion();
        }

        private void btnPredecir_Click(object sender, EventArgs e)
        {
            List<string> conflictos = listaVuelos.DetectarConflictos(distanciaSeg);

            // Si no hay conflictos, no hay nada que proponer
            if (conflictos.Count == 0)
            {
                MessageBox.Show("No se producirá ningún conflicto.", "Predicción");
                return;
            }

            string mensaje = "Se produce conflicto entre:\n";
            for (int i = 0; i < conflictos.Count; i++)
            {
                mensaje += "• " + conflictos[i] + "\n";
            }

            bool resuelto;
            double[] propuestas = listaVuelos.ProponerVelocidades(distanciaSeg, out resuelto);

            //Velocidades propuestas: solo los vuelos que cambiarían
            mensaje += "\nVelocidades propuestas:\n";
            bool hayCambios = false;

            for (int i = 0; i < listaVuelos.GetCount(); i++)
            {
                FlightPlan plan = listaVuelos.GetFlightPlan(i);

                if (Math.Abs(propuestas[i] - plan.GetVelocidad()) > 0.001)
                {
                    mensaje += "• " + plan.GetId() + ": " + propuestas[i].ToString("F2") + "\n";
                    hayCambios = true;
                }
            }

            if (!hayCambios)
            {
                mensaje += "(No se ha encontrado ninguna solución)\n";
            }
            else if (!resuelto)
            {
                mensaje += "\nAviso: Aun con estas velocidades quedarían conflictos sin resolver.\n";
            }

            MessageBox.Show(mensaje, "Predicción");
        }

        private void btnAplicarVel_Click(object sender, EventArgs e)
        {
            PararAutomatica();

            FormVelocidades f = new FormVelocidades(listaVuelos);
            f.ShowDialog();

            if (f.HaAplicado())
            {
                ReiniciarSimulacion();
            }
        }

        // Abre el formulario que muestra el listado completo de vuelos y sus datos
        private void btnListaVuelos_Click(object sender, EventArgs e)
        {
            PararAutomatica();

            FormListaVuelos f = new FormListaVuelos(listaVuelos);
            f.ShowDialog();
        }

        // Indica al usuario cómo consultar los datos de los vuelos mediante la lista o haciendo clic en un avión
        private void btnDatos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad deshabilitada para listas en V2.", "Aviso");
        }

        //Muestra los datos del vuelo clicado (ID, compañía, velocidad, posición) y la distancia con todos los demás
        private void avion_Click(object sender, EventArgs e)
        {
            PictureBox avionClicado = (PictureBox)sender;
            FlightPlan planSeleccionado = (FlightPlan)avionClicado.Tag;
            string info = "DATOS DEL VUELO\n" +
                  "----------------------------------------\n" +
                  "Identificador: " + planSeleccionado.GetId() + "\n" +
                  "Compañía: " + planSeleccionado.GetCompany() + "\n" +
                  "Velocidad: " + planSeleccionado.GetVelocidad().ToString("F2") + "\n" +
                  "Posición actual: (" + planSeleccionado.GetCurrentPosition().GetX().ToString("F2") + ", " +
                                         planSeleccionado.GetCurrentPosition().GetY().ToString("F2") + ")\n\n" +
                  "DISTANCIA CON OTROS VUELOS\n" +
                  "----------------------------------------\n";

            int i = 0;
            int otrosVuelos = 0;
            while (i < listaVuelos.GetCount())
            {
                FlightPlan otroPlan = listaVuelos.GetFlightPlan(i);
                if (otroPlan != null && otroPlan != planSeleccionado)
                {
                    double d = planSeleccionado.Distancia(otroPlan);
                    info = info + "• " + otroPlan.GetId() + " (" + otroPlan.GetCompany() + "): " + d.ToString("F2") + "\n";
                    otrosVuelos++;
                }
                i++;
            }
            if (otrosVuelos == 0)
            {
                info = info + "No hay otros vuelos en el sector.";
            }

            // 4. Mostramos el cuadro con toda la información
            MessageBox.Show(info, "Información y Distancias - " + planSeleccionado.GetId(), MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Paramos el modo automático y reactivamos los botones manuales
            PararAutomatica();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivos de texto (*.txt)|*.txt";
            sfd.Title = "Guardar estado de la simulación";
            sfd.FileName = "Vuelos_Guardados.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    listaVuelos.GuardarEnArchivo(sfd.FileName);
                    MessageBox.Show("Simulación guardada correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el archivo: " + ex.Message);
                }
            }
        }

        // Detiene el temporizador al cerrar el formulario de simulación
        private void FormSimulacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerSimulador.Stop();
        }


        //// Dibuja las trayectorias, círculos de seguridad e identificadores de todos los vuelos en el panel
        private void panelEspacioAereo_Paint(object sender, PaintEventArgs e)
        {
            if (listaVuelos == null || listaVuelos.GetCount() == 0) return;

            Graphics graphics = e.Graphics;
            Pen penTrayectoria = new Pen(Color.Gray,1);
            Font fontID = new Font("Arial", 9, FontStyle.Bold);
            Brush brushID = Brushes.White;

            int centro = 10;
            int radioSeguridad = (int)this.distanciaSeg;
            int mitadAvion = 10;

            //Dibujar trayectorias
            for (int i = 0; i < listaVuelos.GetCount(); i++)
            {
                FlightPlan plan = listaVuelos.GetFlightPlan(i);
                if (plan != null)
                {
                    Point ini = new Point((int)plan.GetInitialPosition().GetX() + centro, (int)plan.GetInitialPosition().GetY() + centro);
                    Point fin = new Point((int)plan.GetFinalPosition().GetX() + centro, (int)plan.GetFinalPosition().GetY() + centro);

                    int dx = fin.X - ini.X;
                    int dy = fin.Y - ini.Y;
                    Point bordeIni = new Point(ini.X - dx * 100, ini.Y - dy * 100);
                    Point bordeFin = new Point(ini.X + dx * 100, ini.Y + dy * 100);

                    graphics.DrawLine(penTrayectoria, bordeIni, bordeFin);
                }
            }
            penTrayectoria.Dispose();

            //Dibujamos los círculos de seguridad y comprobamos conflictos
            for (int i = 0; i < listaVuelos.GetCount(); i++)
            {
                FlightPlan plan = listaVuelos.GetFlightPlan(i);
                PictureBox pb = dibujosAviones[i];

                if (plan != null)
                {
                    bool enConflicto = false;
                    for (int j = 0; j < listaVuelos.GetCount(); j++)
                    {
                        if (i != j) 
                        {
                            FlightPlan otroPlan = listaVuelos.GetFlightPlan(j);
                            if (otroPlan != null && plan.Conflicto(otroPlan, distanciaSeg))
                            {
                                enConflicto = true;
                                break;
                            }
                        }
                    }

                    Color colorCirculo;
                    int grosor;

                    if (enConflicto)
                    {
                        colorCirculo = Color.Red;
                        grosor = 3;
                        pb.BackColor = Color.Red;
                    }
                    else
                    {
                        colorCirculo = Color.Green;
                        grosor = 2;
                        pb.BackColor = Color.Blue;
                    }

                    Pen penSeguridad = new Pen(colorCirculo, grosor);
                    int centroX = pb.Location.X + mitadAvion;
                    int centroY = pb.Location.Y + mitadAvion;

                    graphics.DrawEllipse(penSeguridad, centroX - radioSeguridad, centroY - radioSeguridad, radioSeguridad * 2, radioSeguridad * 2);
                    graphics.DrawString(plan.GetId(), fontID, brushID, pb.Location.X + 22, pb.Location.Y - 5);

                    penSeguridad.Dispose();
                }
            }
        }
     
    }
}