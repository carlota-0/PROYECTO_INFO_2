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
        private Label lblEstadoAlarma;
        private Label lblVuelo1;
        private Label lblVuelo2;
        private TextBox txtVel1;
        private TextBox txtVel2;
        private Button btnAplicarVel;


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
            btnDatos = new Button();
            lblEstadoAlarma = new Label();
            lblVuelo1 = new Label();
            lblVuelo2 = new Label();
            txtVel1 = new TextBox();
            txtVel2 = new TextBox();
            btnAplicarVel = new Button();
            SuspendLayout();
            // 
            // panelEspacioAereo
            // 
            panelEspacioAereo.BackColor = Color.LightGray;
            panelEspacioAereo.Location = new Point(20, 20);
            panelEspacioAereo.Name = "panelEspacioAereo";
            panelEspacioAereo.Size = new Size(738, 405);
            panelEspacioAereo.TabIndex = 1;
            panelEspacioAereo.Paint += panelEspacioAereo_Paint;
            // 
            // btnMover
            // 
            btnMover.Location = new Point(555, 484);
            btnMover.Name = "btnMover";
            btnMover.Size = new Size(150, 46);
            btnMover.TabIndex = 0;
            btnMover.Text = "Avanzar Ciclo";
            btnMover.Click += btnMover_Click;
            // 
            // btnReiniciar
            // 
            btnReiniciar.Location = new Point(782, 170);
            btnReiniciar.Name = "btnReiniciar";
            btnReiniciar.Size = new Size(150, 48);
            btnReiniciar.TabIndex = 0;
            btnReiniciar.Text = "Reiniciar";
            btnReiniciar.Click += btnReiniciar_Click;
            // 
            // btnAuto
            // 
            btnAuto.Location = new Point(78, 486);
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
            btnDetener.Location = new Point(331, 486);
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
            btnPredecir.Location = new Point(782, 94);
            btnPredecir.Name = "btnPredecir";
            btnPredecir.Size = new Size(150, 54);
            btnPredecir.TabIndex = 1;
            btnPredecir.Text = "Predecir Conflicto";
            btnPredecir.Click += btnPredecir_Click;
            // 
            // btnDatos
            // 
            btnDatos.Location = new Point(782, 20);
            btnDatos.Name = "btnDatos";
            btnDatos.Size = new Size(150, 46);
            btnDatos.TabIndex = 0;
            btnDatos.Text = "Mostrar Datos";
            btnDatos.Click += btnDatos_Click;
            // 
            // lblEstadoAlarma
            // 
            lblEstadoAlarma.BackColor = Color.Green;
            lblEstadoAlarma.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblEstadoAlarma.ForeColor = Color.White;
            lblEstadoAlarma.Location = new Point(20, 435);
            lblEstadoAlarma.Name = "lblEstadoAlarma";
            lblEstadoAlarma.Size = new Size(738, 30);
            lblEstadoAlarma.TabIndex = 0;
            lblEstadoAlarma.Text = "ESTADO: SECTOR SEGURO";
            lblEstadoAlarma.TextAlign = ContentAlignment.MiddleCenter;

            // lblVuelo1
            lblVuelo1.AutoSize = true;
            lblVuelo1.Font = new Font("Segoe UI", 12F);
            lblVuelo1.Location = new Point(782, 262);
            lblVuelo1.Text = p1.GetId() + ":";
            // txtVel1
            txtVel1.Font = new Font("Segoe UI", 12F);
            txtVel1.Location = new Point(930, 255);
            txtVel1.Size = new Size(100, 35);
            txtVel1.Text = p1.GetVelocidad().ToString();
            // lblVuelo2
            lblVuelo2.AutoSize = true;
            lblVuelo2.Font = new Font("Segoe UI", 12F);
            lblVuelo2.Location = new Point(782, 322);
            lblVuelo2.Text = p2.GetId() + ":";
            // txtVel2
            txtVel2.Font = new Font("Segoe UI", 12F);
            txtVel2.Location = new Point(930, 315);
            txtVel2.Size = new Size(100, 35);
            txtVel2.Text = p2.GetVelocidad().ToString();
            // btnAplicarVel
            btnAplicarVel.Location = new Point(782, 380);
            btnAplicarVel.Size = new Size(300, 45);
            btnAplicarVel.Text = "Cambiar velocidades y reiniciar";
            btnAplicarVel.UseVisualStyleBackColor = true;
            btnAplicarVel.Click += btnAplicarVel_Click;
            // 
            // FormSimulacion
            // 
            ClientSize = new Size(1270, 765);
            Controls.Add(lblEstadoAlarma);
            Controls.Add(btnDatos);
            Controls.Add(btnPredecir);
            Controls.Add(btnDetener);
            Controls.Add(btnAuto);
            Controls.Add(btnReiniciar);
            Controls.Add(btnMover);
            Controls.Add(panelEspacioAereo);

            Controls.Add(lblVuelo1);
            Controls.Add(txtVel1);
            Controls.Add(lblVuelo2);
            Controls.Add(txtVel2);
            Controls.Add(btnAplicarVel);

            Name = "FormSimulacion";
            Text = "Simulación del Espacio Aéreo";
            FormClosing += FormSimulacion_FormClosing;
            Load += FormSimulacion_Load;
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
        /////COSAS VERSION 1/////
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
        
        ///COSAS VERSION 1///

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