namespace InterfazGrafica
{
    partial class FormParametros
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            distanciaSeglbl = new Label();
            tiempoCiclolbl = new Label();
            distSeguridad = new TextBox();
            tiempoC = new TextBox();
            btnAceptar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // distanciaSeglbl
            // 
            distanciaSeglbl.AutoSize = true;
            distanciaSeglbl.Location = new Point(49, 68);
            distanciaSeglbl.Name = "distanciaSeglbl";
            distanciaSeglbl.Size = new Size(192, 25);
            distanciaSeglbl.TabIndex = 0;
            distanciaSeglbl.Text = "Distancia de seguridad";
            // 
            // tiempoCiclolbl
            // 
            tiempoCiclolbl.AutoSize = true;
            tiempoCiclolbl.Location = new Point(49, 124);
            tiempoCiclolbl.Name = "tiempoCiclolbl";
            tiempoCiclolbl.Size = new Size(137, 25);
            tiempoCiclolbl.TabIndex = 1;
            tiempoCiclolbl.Text = "Tiempo de ciclo";
            // 
            // distSeguridad
            // 
            distSeguridad.Location = new Point(303, 62);
            distSeguridad.Name = "distSeguridad";
            distSeguridad.Size = new Size(150, 31);
            distSeguridad.TabIndex = 2;
            // 
            // tiempoC
            // 
            tiempoC.Location = new Point(303, 118);
            tiempoC.Name = "tiempoC";
            tiempoC.Size = new Size(150, 31);
            tiempoC.TabIndex = 3;
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(106, 229);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(112, 34);
            btnAceptar.TabIndex = 4;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(290, 229);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(112, 34);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FormParametros
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(512, 353);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(tiempoC);
            Controls.Add(distSeguridad);
            Controls.Add(tiempoCiclolbl);
            Controls.Add(distanciaSeglbl);
            Name = "FormParametros";
            Text = "FormParametros";
            Load += FormParametros_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label distanciaSeglbl;
        private Label tiempoCiclolbl;
        private TextBox distSeguridad;
        private TextBox tiempoC;
        private Button btnAceptar;
        private Button btnCancelar;
    }
}