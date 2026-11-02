using System;
using System.Drawing;
using System.Windows.Forms;
using FlightLib;

namespace InterfazGrafica
{
    
    public partial class FormSimulacion : Form
    {
       
        private PictureBox avion1;
        private PictureBox avion2;
        private double distanciaSeg;

        private FlightPlan p1;
        private FlightPlan p2;
        private FlightPlanList listaVuelos;
        private double tiempoCiclo = 10;


        // El constructor recibe los planes de vuelo desde Form1
        public FormSimulacion(FlightPlan plan1, FlightPlan plan2, double distancia)
        {
            this.p1 = plan1;
            this.p2 = plan2;
            this.distanciaSeg = distancia;
            this.listaVuelos = new FlightPlanList();
            this.listaVuelos.AddFlightPlan(p1);
            this.listaVuelos.AddFlightPlan(p2);

            InitializeComponent();

            
            lblVuelo1.Text = p1.GetId() + ":";
            lblVuelo2.Text = p2.GetId() + ":";
            txtVel1.Text = p1.GetVelocidad().ToString();
            txtVel2.Text = p2.GetVelocidad().ToString();

            ConfigurarAviones();
        }

        private void ConfigurarAviones()
        {
            // Dibujamos el Avión 1 (Usaremos un cuadrado azul por ahora)
            avion1 = new PictureBox();
            avion1.Width = 20;
            avion1.Height = 20;
            avion1.BackColor = Color.Blue;
            avion1.Location = new Point((int)p1.GetInitialPosition().GetX(), (int)p1.GetInitialPosition().GetY());
            avion1.Tag = p1;
            avion1.Click += new EventHandler(this.avion_Click);
            panelEspacioAereo.Controls.Add(avion1);



            // Dibujamos el Avión 2 (Usaremos un cuadrado rojo por ahora)
            avion2 = new PictureBox();
            avion2.Width = 20;
            avion2.Height = 20;
            avion2.BackColor = Color.Red;
            avion2.Location = new Point((int)p2.GetInitialPosition().GetX(), (int)p2.GetInitialPosition().GetY());
            avion2.Tag = p2;
            avion2.Click += new EventHandler(this.avion_Click);
            panelEspacioAereo.Controls.Add(avion2);
        }
        private void AvanzarCiclo()
        {

            // Movemos todos los aviones de la lista a la vez
            listaVuelos.Mover(tiempoCiclo);

            // Actualizamos la posición en el panel 
            avion1.Location = new Point((int)p1.GetCurrentPosition().GetX(), (int)p1.GetCurrentPosition().GetY());
            avion2.Location = new Point((int)p2.GetCurrentPosition().GetX(), (int)p2.GetCurrentPosition().GetY());
            // Hacemos que panel borre todo y lo vuelva a dibujar
            panelEspacioAereo.Invalidate();
            //para estado alarma!!!
            if (p1.Conflicto(p2, distanciaSeg))
            {
                lblEstadoAlarma.Text = "¡ALARMA: PÉRDIDA DE SEPARACIÓN ENTRE VUELOS!";
                lblEstadoAlarma.BackColor = Color.Red;
            }
            else
            {
                lblEstadoAlarma.Text = "ESTADO: SECTOR SEGURO";
                lblEstadoAlarma.BackColor = Color.Green;
            }

            // Si activamoss simulación automática y los dos han llegado se paran los dos
            if (timerSimulador.Enabled && p1.EstaDestino() && p2.EstaDestino())
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
            p1.Restart();
            p2.Restart();

            avion1.Location = new Point((int)p1.GetCurrentPosition().GetX(), (int)p1.GetCurrentPosition().GetY());
            avion2.Location = new Point((int)p2.GetCurrentPosition().GetX(), (int)p2.GetCurrentPosition().GetY());

            panelEspacioAereo.Invalidate();
            lblEstadoAlarma.Text = "ESTADO: SECTOR SEGURO";
            lblEstadoAlarma.BackColor = Color.Green;
        }
        
        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            ReiniciarSimulacion();
        }

        /////COSAS VERSION 1/////
        private void btnAplicarVel_Click(object sender, EventArgs e)
        {
            double v1, v2;

            try
            {
                v1 = Convert.ToDouble(txtVel1.Text);
                v2 = Convert.ToDouble(txtVel2.Text);
            }
            catch
            {
                MessageBox.Show("Escribe números válidos en las velocidades");
                return;
            }

            if (v1 <= 0 || v2 <= 0)
            {
                MessageBox.Show("Las velocidades deben ser mayores que 0");
                return;
            }

            PararAutomatica();

            p1.SetVelocidad(v1);
            p2.SetVelocidad(v2);

            ReiniciarSimulacion();

            if (p1.HabraConflicto(p2, distanciaSeg, tiempoCiclo))
                MessageBox.Show("Con las nuevas velocidades sigue habiendo conflicto.");
            else
                MessageBox.Show("Con las nuevas velocidades no habrá conflicto.");
        }
       

        //FASE 5: CONsultamos info al hacer clic en un avion
        private void avion_Click(object sender, EventArgs e)
        {
            // Recuperamos el PictureBox que ha sido clicado
            PictureBox avionClicado = (PictureBox)sender;

            // Recuperamos el plan de vuelo que guardamos en su propiedad Tag
            FlightPlan plan = (FlightPlan)avionClicado.Tag;

            // Preparamos el texto con la información
            string info = $"Identificador: {plan.GetId()}\n" +
                          $"Velocidad: {plan.GetVelocidad():F2}\n" +
                          $"Posición actual: ({plan.GetCurrentPosition().GetX():F2}, {plan.GetCurrentPosition().GetY():F2})";

            MessageBox.Show(info, "Información del Vuelo");
        }
        private void panelEspacioAereo_Paint(object sender, PaintEventArgs e)
        {
            // En tiempo de diseño los aviones aún no existen: no dibujamos nada
            if (p1 == null || p2 == null || avion1 == null || avion2 == null)
                return;

            Graphics graphics = e.Graphics;
            Pen myPen = new Pen(Color.Black);

            //AÑADIMOS ETIQUETA
            Font fontID = new Font("Arial", 9, FontStyle.Bold);
            Brush brushID = Brushes.Black;
            //-----------------
            // Como los aviones miden 20x20, sumamos 10 para apuntar al centro
            int centro = 10;

            // --- TRAYECTORIA AVIÓN 1 ---
            Point ini1 = new Point((int)p1.GetInitialPosition().GetX() + centro, (int)p1.GetInitialPosition().GetY() + centro);
            Point fin1 = new Point((int)p1.GetFinalPosition().GetX() + centro, (int)p1.GetFinalPosition().GetY() + centro);

            // Calculamos la dirección y creamos puntos extremadamente lejanos para que crucen toda la pantalla
            int dx1 = fin1.X - ini1.X;
            int dy1 = fin1.Y - ini1.Y;
            Point bordeIni1 = new Point(ini1.X - dx1 * 100, ini1.Y - dy1 * 100);
            Point bordeFin1 = new Point(ini1.X + dx1 * 100, ini1.Y + dy1 * 100);

            graphics.DrawLine(myPen, bordeIni1, bordeFin1);

            // --- TRAYECTORIA AVIÓN 2 ---
            Point ini2 = new Point((int)p2.GetInitialPosition().GetX() + centro, (int)p2.GetInitialPosition().GetY() + centro);
            Point fin2 = new Point((int)p2.GetFinalPosition().GetX() + centro, (int)p2.GetFinalPosition().GetY() + centro);

            int dx2 = fin2.X - ini2.X;
            int dy2 = fin2.Y - ini2.Y;
            Point bordeIni2 = new Point(ini2.X - dx2 * 100, ini2.Y - dy2 * 100);
            Point bordeFin2 = new Point(ini2.X + dx2 * 100, ini2.Y + dy2 * 100);

            graphics.DrawLine(myPen, bordeIni2, bordeFin2);

            myPen.Dispose();
            // --- FASE 7: DISTANCIA DE SEGURIDAD ---
            // Usaremos un radio de 40 píxeles como representación visual
            int radioSeguridad = (int)this.distanciaSeg;
            // Como definimos que los aviones miden 20x20, su centro está a 10 píxeles
            int mitadAvion = 10;

            // Creamos un lápiz naranja
            Pen penSeguridad = new Pen(Color.Orange);

            // Centro exacto del Avión 1 y dibujo de su elipse
            int centroX1 = avion1.Location.X + mitadAvion;
            int centroY1 = avion1.Location.Y + mitadAvion;
            // Se resta el radio para que el centro del círculo coincida con el centro del avión
            graphics.DrawEllipse(penSeguridad, centroX1 - radioSeguridad, centroY1 - radioSeguridad, radioSeguridad * 2, radioSeguridad * 2);



            // ETIQUETA ID VUELO 1 (Se dibuja 22px a la derecha del avión)
            graphics.DrawString(p1.GetId(), fontID, brushID, avion1.Location.X + 22, avion1.Location.Y - 5);
            // Centro exacto del Avión 2 y dibujo de su elipse
            int centroX2 = avion2.Location.X + mitadAvion;
            int centroY2 = avion2.Location.Y + mitadAvion;
            graphics.DrawEllipse(penSeguridad, centroX2 - radioSeguridad, centroY2 - radioSeguridad, radioSeguridad * 2, radioSeguridad * 2);
            // ETIQUETA ID VUELO 2
            graphics.DrawString(p2.GetId(), fontID, brushID, avion2.Location.X + 22, avion2.Location.Y - 5);
            penSeguridad.Dispose();
        }


        //FASE 8: Boton para realizar simulacion automatica
        private void btnAuto_Click(object sender, EventArgs e)
        {
            if (p1.EstaDestino() && p2.EstaDestino())
            {
                MessageBox.Show("Los vuelos ya han llegado. Pulsa Reiniciar para empezar de nuevo.");
                return;
            }

            // FASE 11: antes de empezar, comprobamos si habrá conflicto
            if (p1.HabraConflicto(p2, distanciaSeg, tiempoCiclo))
            {
                DialogResult respuesta = MessageBox.Show(
                    "Los vuelos entrarán en conflicto. ¿Quieres resolverlo cambiando la velocidad de un vuelo?",
                    "Conflicto detectado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    double velocidadAntes = p2.GetVelocidad();

                    if (p1.ResolverConflicto(p2, distanciaSeg, tiempoCiclo))
                    {
                        txtVel1.Text = p1.GetVelocidad().ToString();
                        txtVel2.Text = p2.GetVelocidad().ToString();
                        MessageBox.Show("Conflicto resuelto. La velocidad de " + p2.GetId() +
                            " ha cambiado de " + velocidadAntes.ToString("F2") +
                            " a " + p2.GetVelocidad().ToString("F2"));
                    }
                    else
                    {
                        MessageBox.Show("No se ha encontrado ninguna velocidad que evite el conflicto. La simulación continúa sin cambios.");
                    }
                }
                // Si responde que No, no hacemos nada y la simulación sigue como siempre
            }

            //Iniciamos automatico
            timerSimulador.Start();
            btnAuto.Enabled = false;
            btnDetener.Enabled = true;
            btnMover.Enabled = false;
        }

        //FASE 8: Boton detener
        private void btnDetener_Click(object sender, EventArgs e)
        {
            PararAutomatica();

        }
        private void timerSimulador_Tick(object sender, EventArgs e)
        {
            AvanzarCiclo();
        }
        //FASE 9: Abre formulario con los datos de los aviones
        private void btnDatos_Click(object sender, EventArgs e)
        {
            FormDatosVuelos formDatos = new FormDatosVuelos(p1, p2);
            formDatos.ShowDialog();
        }


        //FASE 10
        private void btnPredecir_Click(object sender, EventArgs e)
        {
            if (p1.HabraConflicto(p2, distanciaSeg, tiempoCiclo))
                MessageBox.Show("Se producirá un conflicto entre los vuelos.", "Predicción");
            else
                MessageBox.Show("No se producirá ningún conflicto.", "Predicción");
        }

        //añadimos método para que el timer no siga cuado cerramos formulario y hayamos activado el automatico
        private void FormSimulacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerSimulador.Stop();
        }

        private void FormSimulacion_Load(object sender, EventArgs e)
        {

        }
    }
}