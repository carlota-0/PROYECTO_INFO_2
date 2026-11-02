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
            panelEspacioAereo.Size = new Size(1009, 580);
            panelEspacioAereo.TabIndex = 1;
            panelEspacioAereo.Paint += panelEspacioAereo_Paint;
            // 
            // btnMover
            // 
            btnMover.Location = new Point(537, 683);
            btnMover.Name = "btnMover";
            btnMover.Size = new Size(171, 46);
            btnMover.TabIndex = 0;
            btnMover.Text = "Avanzar Ciclo";
            btnMover.Click += btnMover_Click;
            // 
            // btnReiniciar
            // 
            btnReiniciar.Location = new Point(754, 683);
            btnReiniciar.Name = "btnReiniciar";
            btnReiniciar.Size = new Size(171, 46);
            btnReiniciar.TabIndex = 0;
            btnReiniciar.Text = "Reiniciar";
            btnReiniciar.Click += btnReiniciar_Click;
            // 
            // btnAuto
            // 
            btnAuto.Location = new Point(126, 683);
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
            btnDetener.Location = new Point(322, 683);
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
            // btnPredecir
            // 
            btnPredecir.Location = new Point(1041, 102);
            btnPredecir.Name = "btnPredecir";
            btnPredecir.Size = new Size(327, 46);
            btnPredecir.TabIndex = 1;
            btnPredecir.Text = "Predecir Conflicto";
            btnPredecir.Click += btnPredecir_Click;
            // 
            // btnDatos
            // 
            btnDatos.Location = new Point(1041, 31);
            btnDatos.Name = "btnDatos";
            btnDatos.Size = new Size(327, 46);
            btnDatos.TabIndex = 0;
            btnDatos.Text = "Mostrar Datos";
            btnDatos.Click += btnDatos_Click;
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
            // lblVuelo1
            // 
            lblVuelo1.AutoSize = true;
            lblVuelo1.Font = new Font("Segoe UI", 12F);
            lblVuelo1.Location = new Point(1041, 180);
            lblVuelo1.Name = "lblVuelo1";
            lblVuelo1.Size = new Size(134, 45);
            lblVuelo1.TabIndex = 4;
            lblVuelo1.Text = "Vuelo 1:";
            // 
            // lblVuelo2
            // 
            lblVuelo2.AutoSize = true;
            lblVuelo2.Font = new Font("Segoe UI", 12F);
            lblVuelo2.Location = new Point(1041, 240);
            lblVuelo2.Name = "lblVuelo2";
            lblVuelo2.Size = new Size(134, 45);
            lblVuelo2.TabIndex = 6;
            lblVuelo2.Text = "Vuelo 2:";
            // 
            // txtVel1
            // 
            txtVel1.Font = new Font("Segoe UI", 12F);
            txtVel1.Location = new Point(1206, 186);
            txtVel1.Name = "txtVel1";
            txtVel1.Size = new Size(100, 50);
            txtVel1.TabIndex = 5;
            // 
            // txtVel2
            // 
            txtVel2.Font = new Font("Segoe UI", 12F);
            txtVel2.Location = new Point(1206, 242);
            txtVel2.Name = "txtVel2";
            txtVel2.Size = new Size(100, 50);
            txtVel2.TabIndex = 7;
            // 
            // btnAplicarVel
            // 
            btnAplicarVel.Location = new Point(1041, 308);
            btnAplicarVel.Name = "btnAplicarVel";
            btnAplicarVel.Size = new Size(300, 86);
            btnAplicarVel.TabIndex = 8;
            btnAplicarVel.Text = "Cambiar velocidades y reiniciar";
            btnAplicarVel.UseVisualStyleBackColor = true;
            btnAplicarVel.Click += btnAplicarVel_Click;
            // 
            // FormSimulacion
            // 
            ClientSize = new Size(1599, 909);
            Controls.Add(btnDatos);
            Controls.Add(btnAplicarVel);
            Controls.Add(txtVel2);
            Controls.Add(lblVuelo2);
            Controls.Add(txtVel1);
            Controls.Add(lblVuelo1);
            Controls.Add(lblEstadoAlarma);
            Controls.Add(btnPredecir);
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
            PerformLayout();
        }

        #endregion

        private Panel panelEspacioAereo;
        private Button btnMover;
        private Button btnReiniciar;
        private Button btnPredecir;
        private Button btnDatos;
        private Button btnAuto;
        private Button btnDetener;
        private System.Windows.Forms.Timer timerSimulador;
        private Label lblEstadoAlarma;
        private Label lblVuelo1;
        private Label lblVuelo2;
        private TextBox txtVel1;
        private TextBox txtVel2;
        private Button btnAplicarVel;
    }
}