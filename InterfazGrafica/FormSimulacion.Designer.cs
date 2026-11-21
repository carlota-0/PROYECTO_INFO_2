using System.Drawing;
using System.Windows.Forms;

namespace InterfazGrafica
{
    partial class FormSimulacion
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelEspacioAereo = new Panel();
            btnMover = new Button();
            btnReiniciar = new Button();
            btnAuto = new Button();
            btnDetener = new Button();
            timerSimulador = new System.Windows.Forms.Timer(components);
            lblEstadoAlarma = new Label();
            btnAplicarVel = new Button();
            btnPredecir = new Button();
            btnDeshacer = new Button();
            btnGuardar = new Button();
            btnListaVuelos = new Button();
            SuspendLayout();
            // 
            // panelEspacioAereo
            // 
            panelEspacioAereo.BackColor = Color.LightGray;
            panelEspacioAereo.Location = new Point(20, 20);
            panelEspacioAereo.Name = "panelEspacioAereo";
            panelEspacioAereo.Size = new Size(1009, 580);
            panelEspacioAereo.TabIndex = 1;
            panelEspacioAereo.Paint += panelEspacioAereo_Paint;
            // 
            // btnMover
            // 
            btnMover.Location = new Point(453, 684);
            btnMover.Name = "btnMover";
            btnMover.Size = new Size(171, 46);
            btnMover.TabIndex = 0;
            btnMover.Text = "Avanzar Ciclo";
            btnMover.Click += btnMover_Click;
            // 
            // btnReiniciar
            // 
            btnReiniciar.Location = new Point(858, 683);
            btnReiniciar.Name = "btnReiniciar";
            btnReiniciar.Size = new Size(171, 46);
            btnReiniciar.TabIndex = 0;
            btnReiniciar.Text = "Reiniciar";
            btnReiniciar.Click += btnReiniciar_Click;
            // 
            // btnAuto
            // 
            btnAuto.Location = new Point(20, 683);
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
            btnDetener.Location = new Point(230, 683);
            btnDetener.Name = "btnDetener";
            btnDetener.Size = new Size(171, 46);
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
            // lblEstadoAlarma
            // 
            lblEstadoAlarma.BackColor = Color.Green;
            lblEstadoAlarma.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblEstadoAlarma.ForeColor = Color.White;
            lblEstadoAlarma.Location = new Point(20, 620);
            lblEstadoAlarma.Name = "lblEstadoAlarma";
            lblEstadoAlarma.Size = new Size(1009, 30);
            lblEstadoAlarma.TabIndex = 0;
            lblEstadoAlarma.Text = "ESTADO: SECTOR SEGURO";
            lblEstadoAlarma.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnAplicarVel
            // 
            btnAplicarVel.Location = new Point(1049, 20);
            btnAplicarVel.Name = "btnAplicarVel";
            btnAplicarVel.Size = new Size(278, 123);
            btnAplicarVel.TabIndex = 4;
            btnAplicarVel.Text = "Cambiar velocidades y reiniciar";
            btnAplicarVel.UseVisualStyleBackColor = true;
            btnAplicarVel.Click += btnAplicarVel_Click;
            // 
            // btnPredecir
            // 
            btnPredecir.Location = new Point(1049, 172);
            btnPredecir.Name = "btnPredecir";
            btnPredecir.Size = new Size(278, 123);
            btnPredecir.TabIndex = 5;
            btnPredecir.Text = "Predecir Conflicto";
            btnPredecir.UseVisualStyleBackColor = true;
            btnPredecir.Click += btnPredecir_Click;
            // 
            // btnDeshacer
            // 
            btnDeshacer.Location = new Point(675, 683);
            btnDeshacer.Name = "btnDeshacer";
            btnDeshacer.Size = new Size(141, 49);
            btnDeshacer.TabIndex = 6;
            btnDeshacer.Text = "Deshacer";
            btnDeshacer.UseVisualStyleBackColor = true;
            btnDeshacer.Click += btn_Deshacer_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(1051, 325);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(278, 125);
            btnGuardar.TabIndex = 7;
            btnGuardar.Text = "Guardar Simulación Actual ";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnListaVuelos
            // 
            btnListaVuelos.Location = new Point(1049, 475);
            btnListaVuelos.Name = "btnListaVuelos";
            btnListaVuelos.Size = new Size(280, 125);
            btnListaVuelos.TabIndex = 8;
            btnListaVuelos.Text = "Lista de Vuelos";
            btnListaVuelos.UseVisualStyleBackColor = true;
            btnListaVuelos.Click += btnListaVuelos_Click;
            // 
            // FormSimulacion
            // 
            ClientSize = new Size(1655, 828);
            Controls.Add(btnListaVuelos);
            Controls.Add(btnGuardar);
            Controls.Add(btnDeshacer);
            Controls.Add(btnPredecir);
            Controls.Add(btnAplicarVel);
            Controls.Add(lblEstadoAlarma);
            Controls.Add(btnDetener);
            Controls.Add(btnAuto);
            Controls.Add(btnReiniciar);
            Controls.Add(btnMover);
            Controls.Add(panelEspacioAereo);
            Name = "FormSimulacion";
            Text = "Simulación del Espacio Aéreo";
            FormClosing += FormSimulacion_FormClosing;
            Load += FormSimulacion_Load;
            ResumeLayout(false);
        }

        #endregion

        private Panel panelEspacioAereo;
        private Button btnMover;
        private Button btnReiniciar;
        private Button btnAuto;
        private Button btnDetener;
        private System.Windows.Forms.Timer timerSimulador;
        private Label lblEstadoAlarma;
        private Button btnAplicarVel;
        private Button btnPredecir;
        private Button btnDeshacer;
        private Button btnGuardar;
        private Button btnListaVuelos;
    }
}
