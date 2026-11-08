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
            // FormSimulacion
            // 
            ClientSize = new Size(1599, 909);
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
    }
}