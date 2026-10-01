using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace InterfazGrafica
{
    public partial class FormPlanesVuelo : Form
    {
        public FlightPlan Plan1;
        public FlightPlan Plan2;
        public FormPlanesVuelo()
        {
            InitializeComponent();

            //Boton cancelar cierra el form sin guardar nada
            btnCancelar.DialogResult = DialogResult.Cancel;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormPlanesVuelo_Load(object sender, EventArgs e)
        {

        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Desclaramos variables numéricas
            double x1 = 0, y1 = 0, xf1 = 0, yf1 = 0, vel1 = 0;
            double x2 = 0, y2 = 0, xf2 = 0, yf2 = 0, vel2 = 0;

            //1r leer y comprobar vuelo1
            string id1 = textID1.Text;
            if (id1 == "")
            {
                MessageBox.Show("Vuelo 1: ID vacía, escribe un identificador");
                return;

            }

            //POSICION INICIAL (x,y)
            string[] ini1 = Ini1.Text.Split(",");
            if (ini1.Length != 2)
            {
                MessageBox.Show("Vuelo 1: Posición inicial incorrecta, escribe x,y");
                return;
            }
            else
            {
           
                if (!double.TryParse(ini1[0], out x1) || !double.TryParse(ini1[1], out y1))
                {
                    MessageBox.Show("Vuelo 1: Posición inicial incorrecta, escribe x,y");
                    return;
                }
            }

            //POSICION FINAL(x,y)
            string[] fin1 = Fin1.Text.Split(",");
            if (fin1.Length != 2)
            {
                MessageBox.Show("Vuelo 1: Posición final incorrecta, escribe x,y");
                return;
            }
            else
            {
                
                if (!double.TryParse(fin1[0], out xf1) || !double.TryParse(fin1[1], out yf1))
                {
                    MessageBox.Show("Vuelo 1: Posición final incorrecta, escribe x,y");
                    return;
                }
            }

            //VELOCIDAD
           
            if (!double.TryParse(textVelocidad1.Text, out vel1) || vel1 <= 0)
            {
                MessageBox.Show("Vuelo 1: Velocidad incorrecta, escribe un número mayor que 0");
                return;
            }

            //VUELO2
            string id2 = textID2.Text;
            if (id2 == "")
            {
                MessageBox.Show("Vuelo 2: ID vacía, escribe un identificador");
                return;

            }

            //POSICION INICIAL (x,y)
            string[] ini2 = Ini2.Text.Split(",");
            if (ini2.Length != 2)
            {
                MessageBox.Show("Vuelo 2: Posición inicial incorrecta, escribe x,y");
                return;
            }
            else
            {
               
                if (!double.TryParse(ini2[0], out x2) || !double.TryParse(ini2[1], out y2))
                {
                    MessageBox.Show("Vuelo 2: Posición inicial incorrecta, escribe x,y");
                    return;
                }
            }

            //POSICION FINAL(x,y)
            string[] fin2 = Fin2.Text.Split(",");
            if (fin2.Length != 2)
            {
                MessageBox.Show("Vuelo 2: Posición final incorrecta, escribe x,y");
                return;
            }
            else
            {
                
                if (!double.TryParse(fin2[0], out xf2) || !double.TryParse(fin2[1], out yf2))
                {
                    MessageBox.Show("Vuelo 2: Posición final incorrecta, escribe x,y");
                    return;
                }
            }

            //VELOCIDAD
           
            if (!double.TryParse(textVelocidad2.Text, out vel2) || vel2 <= 0)
            {
                MessageBox.Show("Vuelo 2: Velocidad incorrecta, escribe un número mayor que 0");
                return;
            }

            //COMPROBACION ID NO IGUALES
            if (id1 == id2)
            {
                MessageBox.Show("Los identificadores de los vuelos no pueden ser iguales");
                return;
            }

            Plan1 = new FlightPlan(id1, x1, y1, xf1, yf1, vel1);
            Plan2 = new FlightPlan(id2, x2, y2, xf2, yf2, vel2);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
