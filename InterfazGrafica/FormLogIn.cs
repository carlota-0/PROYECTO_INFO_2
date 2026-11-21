using FlightLib;
using System;
using System.Windows.Forms;

namespace InterfazGrafica
{
    public partial class FormLogin : Form
    {
        private GestionUsuarios gestion = new GestionUsuarios();

        //Inicializa los componentes del formulario de acceso
        public FormLogin()
        {
            InitializeComponent();
        }

        //Comprueba al cargar el formulario que la base de datos de usuarios existe y es accesible
        private void FormLogin_Load(object sender, EventArgs e)
        {

            try
            {
                if (!gestion.Iniciar())
                {
                    MessageBox.Show("Error: No se encuentra el fichero de la base de datos (usuarios.db).");
                    Application.Exit();
                    return;
                }
                gestion.Cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la base de datos: " + ex.Message);
                Application.Exit();
            } 

        }

        //Valida si son correctas los datos introducidos y abre el menú principal 
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (user == "" || pass == "")
            {
                MessageBox.Show("Por favor, rellena todos los campos.");
                return;
            }

            try
            {
                if (!gestion.Iniciar())
                {
                    MessageBox.Show("Error: No se encuentra el fichero de la base de datos (usuarios.db).");
                    return;
                }

                bool correcto = gestion.ComprobarLogin(user, pass);
                gestion.Cerrar();

                if (correcto)
                {
                    MessageBox.Show("¡Bienvenid@ " + user + "!");
                    Form1 menuPrincipal = new Form1();
                    this.Hide();
                    menuPrincipal.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                gestion.Cerrar();
                MessageBox.Show("Error al acceder a la base de datos: " + ex.Message);
            }

        }

        //Registra un nuevo usuario en la base de datos comprobando que no esté duplicado
        private void btnRegistro_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (user == "" || pass == "")
            {
                MessageBox.Show("Por favor, rellena todos los campos para registrarte.");
                return;
            }

            try
            {
                if (!gestion.Iniciar())
                {
                    MessageBox.Show("Error: No se encuentra el fichero de la base de datos (usuarios.db).");
                    return;
                }

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
            catch (Exception ex)
            {
                gestion.Cerrar();
                MessageBox.Show("Error al registrar en la base de datos: " + ex.Message);
            }
        }
    }
}