using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InterfazGrafica
{
    public partial class FormParametros : Form
    {
        public double DistanciaSeguridad;
        public double TiempoCiclo;
        double distancia = 0, tiempo = 0;
        public FormParametros()
        {
            InitializeComponent();
            btnCancelar.DialogResult = DialogResult.Cancel;

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // --- Distancia de seguridad ---
          
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

            // --- Tiempo de ciclo ---
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
        private void btnDefecto_Click(object sender, EventArgs e)
        {
            distSeguridad.Text = "50";
            tiempoC.Text = "10";
        }
    }
}
