namespace InterfazGrafica
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            menuOpciones = new ToolStripMenuItem();
            menuPlanesVuelo = new ToolStripMenuItem();
            menuParametros = new ToolStripMenuItem();
            menuSimulacion = new ToolStripMenuItem();
            cargarPlanesDeVueloDesdeArchivoToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuOpciones });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(4, 1, 0, 1);
            menuStrip1.Size = new Size(683, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuOpciones
            // 
            menuOpciones.DropDownItems.AddRange(new ToolStripItem[] { menuPlanesVuelo, menuParametros, menuSimulacion, cargarPlanesDeVueloDesdeArchivoToolStripMenuItem });
            menuOpciones.Name = "menuOpciones";
            menuOpciones.Size = new Size(69, 22);
            menuOpciones.Text = "Opciones";
            menuOpciones.Click += menuOpciones_Click;
            // 
            // menuPlanesVuelo
            // 
            menuPlanesVuelo.Name = "menuPlanesVuelo";
            menuPlanesVuelo.Size = new Size(270, 22);
            menuPlanesVuelo.Text = "Introducir planes de vuelo";
            menuPlanesVuelo.Click += menuPlanesVuelo_Click;
            // 
            // menuParametros
            // 
            menuParametros.Name = "menuParametros";
            menuParametros.Size = new Size(270, 22);
            menuParametros.Text = "Parámetros de vuelo";
            menuParametros.Click += menuParametros_Click;
            // 
            // menuSimulacion
            // 
            menuSimulacion.Name = "menuSimulacion";
            menuSimulacion.Size = new Size(270, 22);
            menuSimulacion.Text = "Iniciar simulación";
            menuSimulacion.Click += menuSimulacion_Click;
            // 
            // cargarPlanesDeVueloDesdeArchivoToolStripMenuItem
            // 
            cargarPlanesDeVueloDesdeArchivoToolStripMenuItem.Name = "cargarPlanesDeVueloDesdeArchivoToolStripMenuItem";
            cargarPlanesDeVueloDesdeArchivoToolStripMenuItem.Size = new Size(270, 22);
            cargarPlanesDeVueloDesdeArchivoToolStripMenuItem.Text = "Cargar planes de vuelo desde archivo";
            cargarPlanesDeVueloDesdeArchivoToolStripMenuItem.Click += cargarPlanesDeVueloDesdeArchivoToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(683, 324);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2, 2, 2, 2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuOpciones;
        private ToolStripMenuItem menuPlanesVuelo;
        private ToolStripMenuItem menuParametros;
        private ToolStripMenuItem menuSimulacion;
        private ToolStripMenuItem cargarPlanesDeVueloDesdeArchivoToolStripMenuItem;
    }
}
