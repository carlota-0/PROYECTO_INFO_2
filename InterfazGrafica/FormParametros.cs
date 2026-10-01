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
            // TryParse devuelve false si el texto no es un número (o está vacío)
            if (!double.TryParse(distSeguridad.Text, out distancia) || distancia <= 0)
            {
                MessageBox.Show("Distancia de seguridad incorrecta, escribe un número mayor que 0");
                return;   // salimos sin cerrar el formulario
            }

            // --- Tiempo de ciclo ---
            if (!double.TryParse(tiempoC.Text, out tiempo) || tiempo <= 0)
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
    
    }
}
