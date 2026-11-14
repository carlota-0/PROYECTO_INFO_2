namespace InterfazGrafica
{
    partial class FormPlanesVuelo
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
            Vuelo1 = new Label();
            Vuelo2 = new Label();
            label3 = new Label();
            XYinicial = new Label();
            XYfinal = new Label();
            label6 = new Label();
            textID1 = new TextBox();
            textID2 = new TextBox();
            textVelocidad2 = new TextBox();
            textVelocidad1 = new TextBox();
            Fin2 = new TextBox();
            Fin1 = new TextBox();
            Ini2 = new TextBox();
            Ini1 = new TextBox();
            btnAceptar = new Button();
            btnCancelar = new Button();
            btnDefecto = new Button();
            label1 = new Label();
            textCompania1 = new TextBox();
            textCompania2 = new TextBox();
            SuspendLayout();
            // 
            // Vuelo1
            // 
            Vuelo1.AutoSize = true;
            Vuelo1.Location = new Point(57, 109);
            Vuelo1.Name = "Vuelo1";
            Vuelo1.Size = new Size(72, 25);
            Vuelo1.TabIndex = 0;
            Vuelo1.Text = "Vuelo 1";
            // 
            // Vuelo2
            // 
            Vuelo2.AutoSize = true;
            Vuelo2.Location = new Point(57, 164);
            Vuelo2.Name = "Vuelo2";
            Vuelo2.Size = new Size(72, 25);
            Vuelo2.TabIndex = 1;
            Vuelo2.Text = "Vuelo 2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(189, 68);
            label3.Name = "label3";
            label3.Size = new Size(30, 25);
            label3.TabIndex = 2;
            label3.Text = "ID";
            // 
            // XYinicial
            // 
            XYinicial.AutoSize = true;
            XYinicial.Location = new Point(446, 68);
            XYinicial.Name = "XYinicial";
            XYinicial.Size = new Size(91, 25);
            XYinicial.TabIndex = 3;
            XYinicial.Text = "(x,y) inicial";
            // 
            // XYfinal
            // 
            XYfinal.AutoSize = true;
            XYfinal.Location = new Point(578, 68);
            XYfinal.Name = "XYfinal";
            XYfinal.Size = new Size(81, 25);
            XYfinal.TabIndex = 4;
            XYfinal.Text = "(x,y) final";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(709, 68);
            label6.Name = "label6";
            label6.Size = new Size(89, 25);
            label6.TabIndex = 5;
            label6.Text = "Velocidad";
            // 
            // textID1
            // 
            textID1.Location = new Point(155, 103);
            textID1.Name = "textID1";
            textID1.Size = new Size(104, 31);
            textID1.TabIndex = 6;
            // 
            // textID2
            // 
            textID2.Location = new Point(155, 158);
            textID2.Name = "textID2";
            textID2.Size = new Size(104, 31);
            textID2.TabIndex = 7;
            // 
            // textVelocidad2
            // 
            textVelocidad2.Location = new Point(709, 158);
            textVelocidad2.Name = "textVelocidad2";
            textVelocidad2.Size = new Size(104, 31);
            textVelocidad2.TabIndex = 8;
            // 
            // textVelocidad1
            // 
            textVelocidad1.Location = new Point(709, 103);
            textVelocidad1.Name = "textVelocidad1";
            textVelocidad1.Size = new Size(104, 31);
            textVelocidad1.TabIndex = 9;
            // 
            // Fin2
            // 
            Fin2.Location = new Point(578, 158);
            Fin2.Name = "Fin2";
            Fin2.Size = new Size(104, 31);
            Fin2.TabIndex = 10;
            // 
            // Fin1
            // 
            Fin1.Location = new Point(578, 103);
            Fin1.Name = "Fin1";
            Fin1.Size = new Size(104, 31);
            Fin1.TabIndex = 11;
            // 
            // Ini2
            // 
            Ini2.Location = new Point(446, 158);
            Ini2.Name = "Ini2";
            Ini2.Size = new Size(104, 31);
            Ini2.TabIndex = 12;
            // 
            // Ini1
            // 
            Ini1.Location = new Point(446, 103);
            Ini1.Name = "Ini1";
            Ini1.Size = new Size(104, 31);
            Ini1.TabIndex = 13;
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(297, 247);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(112, 34);
            btnAceptar.TabIndex = 14;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(444, 247);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(112, 34);
            btnCancelar.TabIndex = 15;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnDefecto
            // 
            btnDefecto.Location = new Point(147, 247);
            btnDefecto.Name = "btnDefecto";
            btnDefecto.Size = new Size(112, 34);
            btnDefecto.TabIndex = 0;
            btnDefecto.Text = "Por defecto";
            btnDefecto.Click += btnDefecto_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(298, 68);
            label1.Name = "label1";
            label1.Size = new Size(93, 25);
            label1.TabIndex = 16;
            label1.Text = "Compañía";
            // 
            // textCompania1
            // 
            textCompania1.Location = new Point(282, 103);
            textCompania1.Name = "textCompania1";
            textCompania1.Size = new Size(137, 31);
            textCompania1.TabIndex = 17;
            // 
            // textCompania2
            // 
            textCompania2.Location = new Point(282, 161);
            textCompania2.Name = "textCompania2";
            textCompania2.Size = new Size(137, 31);
            textCompania2.TabIndex = 18;
            // 
            // FormPlanesVuelo
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(857, 334);
            Controls.Add(textCompania2);
            Controls.Add(textCompania1);
            Controls.Add(label1);
            Controls.Add(btnDefecto);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(Ini1);
            Controls.Add(Ini2);
            Controls.Add(Fin1);
            Controls.Add(Fin2);
            Controls.Add(textVelocidad1);
            Controls.Add(textVelocidad2);
            Controls.Add(textID2);
            Controls.Add(textID1);
            Controls.Add(label6);
            Controls.Add(XYfinal);
            Controls.Add(XYinicial);
            Controls.Add(label3);
            Controls.Add(Vuelo2);
            Controls.Add(Vuelo1);
            Name = "FormPlanesVuelo";
            Text = "FormPlanesVuelo";
            Load += FormPlanesVuelo_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Vuelo1;
        private Label Vuelo2;
        private Label label3;
        private Label XYinicial;
        private Label XYfinal;
        private Label label6;
        private TextBox textID1;
        private TextBox textID2;
        private TextBox textVelocidad2;
        private TextBox textVelocidad1;
        private TextBox Fin2;
        private TextBox Fin1;
        private TextBox Ini2;
        private TextBox Ini1;
        private Button btnAceptar;
        private Button btnCancelar;
        private Button btnDefecto;
        private Label label1;
        private TextBox textCompania1;
        private TextBox textCompania2;
    }
}