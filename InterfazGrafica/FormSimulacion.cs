/* METODO USADO EN LA VERSION 1 CON SOLAMENTE DOS AVIONES
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

        FlightPlanList listaVuelos;
        double distanciaSeguridad;
        private FlightPlanList listaVuelos;
        private double tiempoCiclo = 10;


        // El constructor recibe los planes de vuelo desde Form1
        

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


            //CAMBIO: Si hay conflicto entre aviones cambiar color de las circumferencias
            Color colorCirculo;
            int grosor;

            if(p1.Conflicto(p2, distanciaSeg))
            {
                colorCirculo = Color.Red;
                grosor = 3;
                avion1.BackColor = Color.Red;
                avion2.BackColor = Color.Red;
            }
            else
            {
                // Si el sector está seguro: círculos verdes 
                colorCirculo = Color.Green;
                grosor = 2;
                avion1.BackColor = Color.Blue;
                avion2.BackColor = Color.Red;
            }

         
            Pen penSeguridad = new Pen(colorCirculo,grosor);

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
                //Obtenemos velocidad actual y propuesta antes de preguntar
                double velocidadActual = p2.GetVelocidad();
                double velocidadPropuesta = p1.ObtenerVelocidadPropuesta(p2, distanciaSeg, tiempoCiclo);

                DialogResult respuesta = MessageBox.Show(
                    "CONFLICTO DETECTADO EN LA RUTA.\n\n" +
                    "Propuesta de cambio para el vuelo " + p2.GetId() + ":\n" +
                    "• Velocidad actual: " + velocidadActual.ToString("F2") + "\n" +
                    "• Velocidad propuesta: " + velocidadPropuesta.ToString("F2") + "\n\n" +
                    "¿Deseas aplicar esta nueva velocidad?",
                    "Conflicto detectado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {

                    // Aplicamos la velocidad y actualizamos el TextBox de la pantalla
                    p2.SetVelocidad(velocidadPropuesta);
                    txtVel2.Text = velocidadPropuesta.ToString("F2");

                    MessageBox.Show("Velocidad de " + p2.GetId() + " cambiada a " + velocidadPropuesta.ToString("F2"));
                }

                else
                {
                    MessageBox.Show("No se ha encontrado ninguna velocidad que evite el conflicto. La simulación continúa sin cambios.");
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
            // 1. Cargamos las velocidades iniciales en las cajas de texto (por si estaban vacías)
            txtVel1.Text = p1.GetVelocidad().ToString("F2");
            txtVel2.Text = p2.GetVelocidad().ToString("F2");

            // 2. Extendemos las trayectorias de ambos vuelos hasta los bordes del panel
            double anchoPanel = panelEspacioAereo.Width;
            double altoPanel = panelEspacioAereo.Height;

            p1.ExtenderHastaBordeSector(anchoPanel, altoPanel);
            p2.ExtenderHastaBordeSector(anchoPanel, altoPanel);

            // 3. Forzamos a repintar las líneas de trayectoria completassssssssss
            panelEspacioAereo.Invalidate();

        }
    }
}
*/
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
        private double tiempoCiclo = 10;

        // Creamos una lista para guardar los "dibujitos" (PictureBox) de todos los aviones
        private List<PictureBox> dibujosAviones = new List<PictureBox>();

        // 2. CONSTRUCTOR ACTUALIZADO: Recibe la lista desde Form1
        public FormSimulacion(FlightPlanList listaVuelos, double distancia)
        {
            this.listaVuelos = listaVuelos;
            this.distanciaSeg = distancia;

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

        // FASE 5: Consultamos info al hacer clic en un avion
        private void avion_Click(object sender, EventArgs e)
        {
            PictureBox avionClicado = (PictureBox)sender;
            FlightPlan plan = (FlightPlan)avionClicado.Tag;

            string info = $"Identificador: {plan.GetId()}\n" +
                          $"Velocidad: {plan.GetVelocidad():F2}\n" +
                          $"Posición actual: ({plan.GetCurrentPosition().GetX():F2}, {plan.GetCurrentPosition().GetY():F2})";

            MessageBox.Show(info, "Información del Vuelo");
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

                string mensaje = "Conflictos detectados:\n";
                for (int i = 0; i < conflictos.Count; i++)
                    mensaje += "• " + conflictos[i] + "\n";

                mensaje += "\nCambios de velocidad propuestos:\n";
                bool hayCambios = false;
                for (int i = 0; i < listaVuelos.GetCount(); i++)
                {
                    FlightPlan plan = listaVuelos.GetFlightPlan(i);
                    if (Math.Abs(propuestas[i] - plan.GetVelocidad()) > 0.001)
                    {
                        mensaje += "• " + plan.GetId() + ": " + plan.GetVelocidad().ToString("F2")
                                 + " → " + propuestas[i].ToString("F2") + "\n";
                        hayCambios = true;
                    }
                }

                if (!hayCambios)
                {
                    mensaje += "(ninguno: no se ha encontrado solución)\n\n¿Iniciar la simulación igualmente?";
                    if (MessageBox.Show(mensaje, "Conflictos detectados", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) == DialogResult.No)
                        return;
                }
                else
                {
                    if (!resuelto)
                        mensaje += "\nAviso: aun con estos cambios quedarían conflictos sin resolver.\n";

                    mensaje += "\nSí = aplicar los cambios\nNo = iniciar sin cambiar nada\nCancelar = no iniciar";

                    DialogResult r = MessageBox.Show(mensaje, "Conflictos detectados",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                    if (r == DialogResult.Cancel)
                        return;

                    if (r == DialogResult.Yes)
                    {
                        for (int i = 0; i < listaVuelos.GetCount(); i++)
                            listaVuelos.GetFlightPlan(i).SetVelocidad(propuestas[i]);
                    }
                }
            }

            // Para la Versión 2, simplificamos el automático para que funcione con N aviones
            // Anticipamos los conflictos antes de empezar
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

    }
}