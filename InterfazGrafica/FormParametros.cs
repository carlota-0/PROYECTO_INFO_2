using System;
using System.Windows.Forms;

namespace InterfazGrafica
{
    public partial class FormParametros : Form
    {
        public double DistanciaSeguridad;
        public double TiempoCiclo;

        //Inicializa el formulario y configura el botón cancelar
        public FormParametros()
        {
            InitializeComponent();
            btnCancelar.DialogResult = DialogResult.Cancel;

        }

        //Rellena los campos de parámetros con valores por defecto
        private void btnDefecto_Click(object sender, EventArgs e)
        {
            distSeguridad.Text = "50";
            tiempoC.Text = "10";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            double distancia = 0;
            double tiempo = 0;

            try
            {
                distancia = Convert.ToDouble(distSeguridad.Text);
            }
            catch
            {
                MessageBox.Show("Distancia de seguridad incorrecta, escribe un número");
                return;  
            }
            if (distancia<= 0)
            {
                MessageBox.Show("Distancia de seguridad incorrecta, escribe un número mayor que 0");
                return;
            }

            try
            {
                tiempo = Convert.ToDouble(tiempoC.Text);
            }
            catch
            {
                MessageBox.Show("Tiempo de ciclo incorrecto, escribe un número");
                return;
            }
            if (tiempo<= 0)
            {
                MessageBox.Show("Tiempo de ciclo incorrecto, escribe un número mayor que 0");
                return;
            }

            // Todo correcto: guardamos los valores y cerramos
            DistanciaSeguridad = distancia;
            TiempoCiclo = tiempo;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void FormParametros_Load(object sender, EventArgs e)
        {

        }
    }
}
