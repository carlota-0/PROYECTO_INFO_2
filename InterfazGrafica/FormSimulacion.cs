// VERSION DOS CON LA LISTA DE VUELOS EN VEX DE DOS FLIGHT PLANS
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic; 
using FlightLib;

namespace InterfazGrafica
{
    public partial class FormSimulacion : Form
    {
        // 1. ATRIBUTOS
        private double distanciaSeg;
        private FlightPlanList listaVuelos;
        private double tiempoCiclo;

        // Creamos una lista para guardar los "dibujitos" (PictureBox) de todos los aviones
        private List<PictureBox> dibujosAviones = new List<PictureBox>();

        // 2. CONSTRUCTOR ACTUALIZADO: Recibe la lista desde Form1
        public FormSimulacion(FlightPlanList listaVuelos, double distancia, double tiempoCiclo)
        {
            this.listaVuelos = listaVuelos;
            this.distanciaSeg = distancia;
            this.tiempoCiclo = tiempoCiclo;


            InitializeComponent();
            ConfigurarAviones();

        }

        // 3. CONFIGURAR AVIONES: Bucle para dibujar todos los aviones de la lista
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
                    pb.BackColor = Color.Blue; // Por defecto todos azules
                    pb.Location = new Point((int)plan.GetInitialPosition().GetX(), (int)plan.GetInitialPosition().GetY());
                    pb.Tag = plan;
                    pb.Click += new EventHandler(this.avion_Click);

                    panelEspacioAereo.Controls.Add(pb);
                    dibujosAviones.Add(pb); // Lo guardamos en nuestra lista de PictureBox
                }
            }
        }

        // 4. AVANZAR CICLO: Mueve toda la lista a la vez
        private void AvanzarCiclo()
        {
            listaVuelos.Mover(tiempoCiclo);

            bool todosHanLlegado = true;
            bool hayConflicto = false;

            // Actualizamos la posición de cada PictureBox y comprobamos estado
            for (int i = 0; i < listaVuelos.GetCount(); i++)
            {
                FlightPlan plan = listaVuelos.GetFlightPlan(i);
                PictureBox pb = dibujosAviones[i];

                if (plan != null)
                {
                    pb.Location = new Point((int)plan.GetCurrentPosition().GetX(), (int)plan.GetCurrentPosition().GetY());

                    if (!plan.EstaDestino())
                        todosHanLlegado = false;

                    // Comprobamos conflicto de este avión con cualquier otro de la lista
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

            panelEspacioAereo.Invalidate();

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

            if (timerSimulador.Enabled && todosHanLlegado)
            {
                PararAutomatica();
                MessageBox.Show("Todos los vuelos han llegado a su destino. Simulación terminada.");
            }
        }

        private void PararAutomatica()
        {
            timerSimulador.Stop();
            btnAuto.Enabled = true;
            btnDetener.Enabled = false;
            btnMover.Enabled = true;
        }

        private void btnMover_Click(object sender, EventArgs e)
        {
            AvanzarCiclo();
        }

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

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            ReiniciarSimulacion();
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

        // 5. EVENTO PAINT: Dibuja las líneas y radios de seguridad de TODA la lista
        private void panelEspacioAereo_Paint(object sender, PaintEventArgs e)
        {
            if (listaVuelos == null || listaVuelos.GetCount() == 0) return;

            Graphics graphics = e.Graphics;
            Pen penTrayectoria = new Pen(Color.Black);
            Font fontID = new Font("Arial", 9, FontStyle.Bold);
            Brush brushID = Brushes.Black;

            int centro = 10;
            int radioSeguridad = (int)this.distanciaSeg;
            int mitadAvion = 10;

            // 1. Dibujar trayectorias
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

            // 2. Dibujar círculos de seguridad y comprobar conflictos
            for (int i = 0; i < listaVuelos.GetCount(); i++)
            {
                FlightPlan plan = listaVuelos.GetFlightPlan(i);
                PictureBox pb = dibujosAviones[i];

                if (plan != null)
                {
                    bool enConflicto = false;
                    for (int j = 0; j < listaVuelos.GetCount(); j++)
                    {
                        if (i != j) // No compararse consigo mismo
                        {
                            FlightPlan otroPlan = listaVuelos.GetFlightPlan(j);
                            if (otroPlan != null && plan.Conflicto(otroPlan, distanciaSeg))
                            {
                                enConflicto = true;
                                break;
                            }
                        }
                    }

                    Color colorCirculo = enConflicto ? Color.Red : Color.Green;
                    int grosor = enConflicto ? 3 : 2;
                    pb.BackColor = enConflicto ? Color.Red : Color.Blue;

                    Pen penSeguridad = new Pen(colorCirculo, grosor);
                    int centroX = pb.Location.X + mitadAvion;
                    int centroY = pb.Location.Y + mitadAvion;

                    graphics.DrawEllipse(penSeguridad, centroX - radioSeguridad, centroY - radioSeguridad, radioSeguridad * 2, radioSeguridad * 2);
                    graphics.DrawString(plan.GetId(), fontID, brushID, pb.Location.X + 22, pb.Location.Y - 5);

                    penSeguridad.Dispose();
                }
            }
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            List<string> conflictos = listaVuelos.DetectarConflictos(distanciaSeg);

            if (conflictos.Count > 0)
            {
                bool resuelto;
                double[] propuestas = listaVuelos.ProponerVelocidades(distanciaSeg, out resuelto);

                // Se abre la tabla. Pulse lo que pulse el usuario (Aplicar, Sin cambios,
                // Cancelar o la X), cuando se cierre seguimos y la simulación arranca.
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

        private void btnDatos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad deshabilitada para listas en V2.", "Aviso");
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

        private void btnPredecir_Click(object sender, EventArgs e)
        {
            List<string> conflictos = listaVuelos.DetectarConflictos(distanciaSeg);

            // Si no hay conflictos, no hay nada que proponer
            if (conflictos.Count == 0)
            {
                MessageBox.Show("No se producirá ningún conflicto.", "Predicción");
                return;
            }

            // 1. Parejas que entran en conflicto
            string mensaje = "Se produce conflicto entre:\n";
            for (int i = 0; i < conflictos.Count; i++)
            {
                mensaje += "• " + conflictos[i] + "\n";
            }

            // 2. Calculamos las velocidades propuestas (no cambia nada de verdad)
            bool resuelto;
            double[] propuestas = listaVuelos.ProponerVelocidades(distanciaSeg, out resuelto);

            // 3. Velocidades propuestas: solo los vuelos que cambiarían
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
                mensaje += "(no se ha encontrado ninguna solución)\n";
            }
            else if (!resuelto)
            {
                mensaje += "\nAviso: aun con estas velocidades quedarían conflictos sin resolver.\n";
            }

            MessageBox.Show(mensaje, "Predicción");
        }

        private void btn_Deshacer_Click(object sender, EventArgs e)
        {
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
                    panelEspacioAereo.Invalidate();
                }
                else
                {
                    MessageBox.Show("No hay pasos anteriores para deshacer.");
                }
            }
        }
        private void FormSimulacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerSimulador.Stop();
        }

        private void FormSimulacion_Load(object sender, EventArgs e)
        {
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
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Pausar el temporizador para que los aviones se queden quietos mientras guardas
            timerSimulador.Stop();

            // 2. Preparar la ventanita para elegir dónde guardar
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivos de texto (*.txt)|*.txt";
            sfd.Title = "Guardar estado de la simulación";
            sfd.FileName = "Vuelos_Guardados.txt";

            // 3. Si el usuario le da a "Guardar"
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // Llamamos a tu función de guardado
                // (Asegúrate de que 'milista' sea el nombre de tu variable FlightPlanList en este Form)
                listaVuelos.GuardarEnArchivo(sfd.FileName);

                MessageBox.Show("Simulación guardada correctamente.");
            }

            // 4. Como no salimos del programa, tenemos que reanudar el vuelo.
            // Esto se ejecuta tanto si guarda como si cancela la ventana.
            timerSimulador.Start();
        }

        private void btnListaVuelos_Click(object sender, EventArgs e)
        {
            // Paramos el automático para que la lista no quede desactualizada
            PararAutomatica();

            FormListaVuelos f = new FormListaVuelos(listaVuelos);
            f.ShowDialog();
        }
    }
}