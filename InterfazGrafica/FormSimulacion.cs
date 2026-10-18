using System;
using System.Drawing;
using System.Windows.Forms;
using FlightLib;

namespace InterfazGrafica
{
    public class FormSimulacion : Form
    {
        private Panel panelEspacioAereo;
        private PictureBox avion1;
        private PictureBox avion2;
        private Button btnMover;
        private Button btnReiniciar;
        private double distanciaSeg;
        private Button btnPredecir;
        private Button btnDatos;

        private FlightPlan p1;
        private FlightPlan p2;
        private Button btnAuto;
        private Button btnDetener;
        private System.Windows.Forms.Timer timerSimulador;
        private System.ComponentModel.IContainer components;
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
            ConfigurarAviones();
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelEspacioAereo = new Panel();
            btnMover = new Button();
            btnReiniciar = new Button();
            btnAuto = new Button();
            btnDetener = new Button();
            timerSimulador = new System.Windows.Forms.Timer(components);
            btnPredecir = new Button();
            SuspendLayout();
            // 
            // panelEspacioAereo
            // 
            panelEspacioAereo.BackColor = Color.LightGray;
            panelEspacioAereo.Location = new Point(20, 20);
            panelEspacioAereo.Name = "panelEspacioAereo";
            panelEspacioAereo.Size = new Size(726, 385);
            panelEspacioAereo.TabIndex = 1;
            panelEspacioAereo.Paint += panelEspacioAereo_Paint;
            // 
            // btnMover
            // 
            btnMover.Location = new Point(367, 464);
            btnMover.Name = "btnMover";
            btnMover.Size = new Size(150, 46);
            btnMover.TabIndex = 0;
            btnMover.Text = "Avanzar Ciclo";
            btnMover.Click += btnMover_Click;
            // 
            // btnReiniciar
            // 
            btnReiniciar.Location = new Point(570, 466);
            btnReiniciar.Name = "btnReiniciar";
            btnReiniciar.Size = new Size(150, 44);
            btnReiniciar.TabIndex = 0;
            btnReiniciar.Text = "Reiniciar";
            btnReiniciar.Click += btnReiniciar_Click;
            // 
            // btnAuto
            // 
            btnAuto.Location = new Point(20, 464);
            btnAuto.Name = "btnAuto";
            btnAuto.Size = new Size(171, 46);
            btnAuto.TabIndex = 2;
            btnAuto.Text = "Automático";
            btnAuto.UseVisualStyleBackColor = true;
            btnAuto.Click += btnAuto_Click;
            // 
            // btnDetener
            // 
            btnDetener.Enabled = false;
            btnDetener.Location = new Point(223, 463);
            btnDetener.Name = "btnDetener";
            btnDetener.Size = new Size(112, 44);
            btnDetener.TabIndex = 3;
            btnDetener.Text = "Detener";
            btnDetener.UseVisualStyleBackColor = true;
            btnDetener.Click += btnDetener_Click;
            // 
            // timerSimulador
            // 
            timerSimulador.Interval = 500;
            timerSimulador.Tick += timerSimulador_Tick;
            // 
            // btnPredecir
            // 
            btnPredecir.Location = new Point(223, 550);
            btnPredecir.Name = "btnPredecir";
            btnPredecir.Size = new Size(218, 81);
            btnPredecir.TabIndex = 1;
            btnPredecir.Text = "Predecir Conflicto";
            btnPredecir.Click += btnPredecir_Click;
            //
            //btn Datos Vuelo
            //
            this.btnDatos = new Button();
            this.btnDatos.Location = new Point(250, 460); // Justo debajo del botón Mover
            this.btnDatos.Size = new Size(150, 30);
            this.btnDatos.Text = "Mostrar Datos";
            this.btnDatos.Click += new EventHandler(this.btnDatos_Click);
            this.Controls.Add(this.btnDatos);

// Aumentamos un pelín el alto total de la ventana para que quepa este nuevo botón
            this.ClientSize = new Size(650, 510);
            // 
            // FormSimulacion
            // 
            ClientSize = new Size(1270, 765);
            Controls.Add(btnPredecir);
            Controls.Add(btnDetener);
            Controls.Add(btnAuto);
            Controls.Add(btnReiniciar);
            Controls.Add(btnMover);
            Controls.Add(panelEspacioAereo);
            Name = "FormSimulacion";
            Text = "Simulación del Espacio Aéreo";
            FormClosing += FormSimulacion_FormClosing; 
            ResumeLayout(false);
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


            // Movemos todos los aviones de la lista a la vez
            listaVuelos.Mover(tiempoCiclo);

            // Actualizamos la posición visual en el panel
            avion1.Location = new Point((int)p1.GetCurrentPosition().GetX(), (int)p1.GetCurrentPosition().GetY());
            avion2.Location = new Point((int)p2.GetCurrentPosition().GetX(), (int)p2.GetCurrentPosition().GetY());
            // Obliga al panel a borrar y volver a dibujar todo (líneas, aviones y círculos) en la nueva posición
            panelEspacioAereo.Invalidate();
            // Alarma en tiempo real
            if (p1.Conflicto(p2, distanciaSeg))
            {
                MessageBox.Show("¡ALARMA! Los aviones han entrado en la distancia de seguridad.", "Alarma de Conflicto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            // Reiniciamos las coordenadas lógicas
            p1.Restart();
            p2.Restart();

            // Actualizamos visualmente
            avion1.Location = new Point((int)p1.GetCurrentPosition().GetX(), (int)p1.GetCurrentPosition().GetY());
            avion2.Location = new Point((int)p2.GetCurrentPosition().GetX(), (int)p2.GetCurrentPosition().GetY());

            panelEspacioAereo.Invalidate();
        }
        private void btnPredecir_Click(object sender, EventArgs e)
        {
            // Creamos copias exactas para no mover los originales de la pantalla
            FlightPlan test1 = new FlightPlan(p1.GetId(), p1.GetInitialPosition().GetX(), p1.GetInitialPosition().GetY(), p1.GetFinalPosition().GetX(), p1.GetFinalPosition().GetY(), p1.GetVelocidad());
            FlightPlan test2 = new FlightPlan(p2.GetId(), p2.GetInitialPosition().GetX(), p2.GetInitialPosition().GetY(), p2.GetFinalPosition().GetX(), p2.GetFinalPosition().GetY(), p2.GetVelocidad());

            bool habraConflicto = false;

            // Simulamos ciclo a ciclo hasta que ambos lleguen al destino
            while (!test1.EstaDestino() || !test2.EstaDestino())
            {
                test1.Mover(10);
                test2.Mover(10);

                if (test1.Conflicto(test2, distanciaSeg))
                {
                    habraConflicto = true;
                    break;
                }
            }

            // Mostrar el resultado de la predicción
            if (habraConflicto)
                MessageBox.Show("PREDICCIÓN: Sí, los aviones entrarán en conflicto en algún momento.", "Predicción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("PREDICCIÓN: Ruta segura. No se detectan conflictos.", "Predicción", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
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
            Graphics graphics = e.Graphics;
            Pen myPen = new Pen(Color.Black);

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
            int radioSeguridad = 40;
            // Como definimos que los aviones miden 20x20, su centro está a 10 píxeles
            int mitadAvion = 10;

            // Creamos un lápiz naranja
            Pen penSeguridad = new Pen(Color.Orange);

            // Centro exacto del Avión 1 y dibujo de su elipse
            int centroX1 = avion1.Location.X + mitadAvion;
            int centroY1 = avion1.Location.Y + mitadAvion;
            // Se resta el radio para que el centro del círculo coincida con el centro del avión
            graphics.DrawEllipse(penSeguridad, centroX1 - radioSeguridad, centroY1 - radioSeguridad, radioSeguridad * 2, radioSeguridad * 2);

            // Centro exacto del Avión 2 y dibujo de su elipse
            int centroX2 = avion2.Location.X + mitadAvion;
            int centroY2 = avion2.Location.Y + mitadAvion;
            graphics.DrawEllipse(penSeguridad, centroX2 - radioSeguridad, centroY2 - radioSeguridad, radioSeguridad * 2, radioSeguridad * 2);

            penSeguridad.Dispose();
        }
        
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

            // A partir de aquí es lo mismo que tenías
            timerSimulador.Start();
            btnAuto.Enabled = false;
            btnDetener.Enabled = true;
            btnMover.Enabled = false;
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            PararAutomatica();

        }
        
        private void btnDatos_Click(object sender, EventArgs e)
        {
            FormDatosVuelos formDatos = new FormDatosVuelos(p1, p2);
            formDatos.ShowDialog();
        }

        private void timerSimulador_Tick(object sender, EventArgs e)
        {
            AvanzarCiclo();
        }
        //añadimos método para que el timer no siga cuado cerramos formulario y hayamos activado el automatico
        private void FormSimulacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerSimulador.Stop();
        }
        
        

        

    }
}