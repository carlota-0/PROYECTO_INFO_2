using System;
using System.Windows.Forms;

namespace InterfazGrafica
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        // Clic en Iniciar Sesión
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Cogemos lo que el usuario ha escrito
            string user = txtUsuario.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (user == "" || pass == "")
            {
                MessageBox.Show("Por favor, rellena todos los campos.");
                return;
            }

            // Usamos la clase que creamos antes
            GestionUsuarios gestion = new GestionUsuarios();
            gestion.Iniciar();

            bool correcto = gestion.ComprobarLogin(user, pass);

            gestion.Cerrar();

            if (correcto)
            {
                MessageBox.Show("¡Bienvenido " + user + "!");

                // Abrimos tu menú principal de la Versión 0 (Form1)
                Form1 menuPrincipal = new Form1();
                this.Hide(); // Ocultamos la ventana de login
                menuPrincipal.ShowDialog(); // Esperamos a que cierre el menú principal
                this.Close(); // Cerramos el programa entero
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }

        // Clic en Registrarse
        private void btnRegistro_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (user == "" || pass == "")
            {
                MessageBox.Show("Por favor, rellena todos los campos para registrarte.");
                return;
            }

            GestionUsuarios gestion = new GestionUsuarios();
            gestion.Iniciar();

            int resultado = gestion.RegistrarUsuario(user, pass);

            gestion.Cerrar();

            if (resultado == 0)
            {
                MessageBox.Show("Usuario registrado correctamente. Ahora puedes iniciar sesión.");
            }
            else if (resultado == -1)
            {
                MessageBox.Show("Error: Ese nombre de usuario ya existe. Elige otro.");
            }
            else
            {
                MessageBox.Show("Error desconocido al registrar.");
            }
        }
    }
}