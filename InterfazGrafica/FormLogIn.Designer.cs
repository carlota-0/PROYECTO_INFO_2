namespace InterfazGrafica
{
    partial class FormLogin
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
            label1 = new Label();
            label2 = new Label();
            txtPassword = new TextBox();
            txtUsuario = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(334, 48);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(98, 25);
            label1.TabIndex = 0;
            label1.Text = "contraseña";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(86, 53);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(70, 25);
            label2.TabIndex = 1;
            label2.Text = "usuario";
            // 
            // txtPassword
            // 
            txtPassword.AccessibleName = "txtpassword";
            txtPassword.Location = new Point(310, 83);
            txtPassword.Margin = new Padding(4, 5, 4, 5);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(141, 31);
            txtPassword.TabIndex = 2;
            // 
            // txtUsuario
            // 
            txtUsuario.AccessibleName = "txtusuario";
            txtUsuario.Location = new Point(46, 83);
            txtUsuario.Margin = new Padding(4, 5, 4, 5);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(141, 31);
            txtUsuario.TabIndex = 3;
            // 
            // button1
            // 
            button1.AccessibleName = "btnLogin";
            button1.Location = new Point(46, 132);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(143, 38);
            button1.TabIndex = 4;
            button1.Text = "Iniciar Sesion";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnLogin_Click;
            // 
            // button2
            // 
            button2.AccessibleName = "btnRegistro";
            button2.Location = new Point(310, 132);
            button2.Margin = new Padding(4, 5, 4, 5);
            button2.Name = "button2";
            button2.Size = new Size(143, 38);
            button2.TabIndex = 5;
            button2.Text = "Registrarse";
            button2.UseVisualStyleBackColor = true;
            button2.Click += btnRegistro_Click;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(504, 242);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txtUsuario);
            Controls.Add(txtPassword);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormLogin";
            Text = "FormLogIn";
            Load += FormLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtPassword;
        private TextBox txtUsuario;
        private Button button1;
        private Button button2;
    }
}