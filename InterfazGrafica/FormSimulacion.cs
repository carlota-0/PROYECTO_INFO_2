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
        
        private FlightPlan p1;
        private FlightPlan p2;

        // El constructor recibe los planes de vuelo desde Form1
        public FormSimulacion(FlightPlan plan1, FlightPlan plan2)
        {
            this.p1 = plan1;
            this.p2 = plan2;
            
            InitializeComponent();
            ConfigurarAviones();
        }

        private void InitializeComponent()
        {
            this.panelEspacioAereo = new Panel();
            this.SuspendLayout();
            
            // 
            // panelEspacioAereo
            // 
            this.panelEspacioAereo.BackColor = Color.LightGray; // Fondo gris para distinguirlo
            this.panelEspacioAereo.Location = new Point(20, 20);
            this.panelEspacioAereo.Name = "panelEspacioAereo";
            this.panelEspacioAereo.Size = new Size(600, 400); 
            this.panelEspacioAereo.Paint += new PaintEventHandler(this.panelEspacioAereo_Paint);
            // 
            // btnMover
            // 
            this.btnMover = new Button();
            this.btnMover.Location = new Point(250, 425); // Lo centramos debajo del panel
            this.btnMover.Size = new Size(150, 30);
            this.btnMover.Text = "Avanzar Ciclo";
            this.btnMover.Click += new EventHandler(this.btnMover_Click);

            this.ClientSize = new Size(650, 480); // Hacemos la ventana un poco más alta para que quepa el botón
            this.Controls.Add(this.btnMover);
            
            // 
            // FormSimulacion
            // 
            this.Controls.Add(this.panelEspacioAereo);
            this.Name = "FormSimulacion";
            this.Text = "Simulación del Espacio Aéreo";
            this.ResumeLayout(false);
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
        private void btnMover_Click(object sender, EventArgs e)
        {
            // Usamos 10 como tiempo de ciclo por defecto (como en SimulatorConsole)
            double tiempoCiclo = 10; 
    
            // Movemos los datos lógicos del avión
            p1.Mover(tiempoCiclo);
            p2.Mover(tiempoCiclo);

            // Actualizamos la posición visual en el panel
            avion1.Location = new Point((int)p1.GetCurrentPosition().GetX(), (int)p1.GetCurrentPosition().GetY());
            avion2.Location = new Point((int)p2.GetCurrentPosition().GetX(), (int)p2.GetCurrentPosition().GetY());
            // Obliga al panel a borrar y volver a dibujar todo (líneas, aviones y círculos) en la nueva posición
            panelEspacioAereo.Invalidate();
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
    
            // Creamos un "lápiz" negro para dibujar las trayectorias
            Pen myPen = new Pen(Color.Black);

            // Dibujar trayectoria del Avión 1 (desde posición inicial a la final)
            Point inicio1 = new Point((int)p1.GetInitialPosition().GetX(), (int)p1.GetInitialPosition().GetY());
            Point fin1 = new Point((int)p1.GetFinalPosition().GetX(), (int)p1.GetFinalPosition().GetY());
            graphics.DrawLine(myPen, inicio1, fin1);

            // Dibujar trayectoria del Avión 2
            Point inicio2 = new Point((int)p2.GetInitialPosition().GetX(), (int)p2.GetInitialPosition().GetY());
            Point fin2 = new Point((int)p2.GetFinalPosition().GetX(), (int)p2.GetFinalPosition().GetY());
            graphics.DrawLine(myPen, inicio2, fin2);

            // Liberamos el recurso del lápiz
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
    }
}