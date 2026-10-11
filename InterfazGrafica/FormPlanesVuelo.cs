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
        private FlightPlan Plan1;
        private FlightPlan Plan2;

        public FlightPlan GetPlan1()
        {
            return Plan1;
        }

        public FlightPlan GetPlan2()
        {
            return Plan2;
        }
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
        private void btnDefecto_Click(object sender, EventArgs e)
        {
            textID1.Text = "IBE123";
            Ini1.Text = "50,50";
            Fin1.Text = "400,300";
            textVelocidad1.Text = "150";

            textID2.Text = "RYR456";
            Ini2.Text = "400,50";
            Fin2.Text = "50,300";
            textVelocidad2.Text = "150";
        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Desclaramos variables numéricas
            double x1 = 0, y1 = 0, xf1 = 0, yf1 = 0, vel1 = 0;
            double x2 = 0, y2 = 0, xf2 = 0, yf2 = 0, vel2 = 0;

            //1r leer y comprobar vuelo1
            string id1 = textID1.Text.Trim();
            //Trim() elimina espacios en blanco!!
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
            try
            {

                x1 = Convert.ToDouble(ini1[0]);
                y1 = Convert.ToDouble(ini1[1]);
            }
            catch 
            {
                MessageBox.Show("Vuelo 1: Posición inicial incorrecta, escribe x,y");
                return;
            }
              
    

            //POSICION FINAL(x,y)
            string[] fin1 = Fin1.Text.Split(",");
            if (fin1.Length != 2)
            {
                MessageBox.Show("Vuelo 1: Posición final incorrecta, escribe x,y");
                return;
            }
            
            try
            {
                xf1 = Convert.ToDouble(fin1[0]);
                yf1 = Convert.ToDouble(fin1[1]);
            }
            catch
            {
                MessageBox.Show("Vuelo 1: Posición final incorrecta, escribe x,y");
                return;
            }

            //VELOCIDAD

            try
            {
                vel1 = Convert.ToDouble(textVelocidad1.Text);
            }
            catch
            {
                MessageBox.Show("Vuelo 1: Velocidad incorrecta, escribe un número");
                return;
            }

            if (vel1 <= 0)
            {
                MessageBox.Show("Vuelo 1: Velocidad incorrecta, escribe un número mayor que 0");
                return;
    
            }

            //POSICION INICIAL Y FINAL 1 NO IGUALES
            if (x1 == xf1 && y1 == yf1)
            {
                MessageBox.Show("Vuelo 1: Posición inicial y final no pueden ser iguales");
                return;
            }

            //_____________________
            //VUELO2
            //_____________________
            string id2 = textID2.Text.Trim();
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
            try
            {

                x2 = Convert.ToDouble(ini2[0]);
                y2 = Convert.ToDouble(ini2[1]);
            }
            catch
            {
                MessageBox.Show("Vuelo 2: Posición inicial incorrecta, escribe x,y");
                return;
            }

            //POSICION FINAL(x,y)
            string[] fin2 = Fin2.Text.Split(",");
            if (fin2.Length != 2)
            {
                MessageBox.Show("Vuelo 2: Posición final incorrecta, escribe x,y");
                return;
            }
            try
            {
                xf2 = Convert.ToDouble(fin2[0]);
                yf2 = Convert.ToDouble(fin2[1]);
            }
            catch
            {
                MessageBox.Show("Vuelo 2: Posición final incorrecta, escribe x,y");
                return;
            }

            //VELOCIDAD

            try
            {
                vel2 = Convert.ToDouble(textVelocidad2.Text);
            }
            catch
            {
                MessageBox.Show("Vuelo 2: Velocidad incorrecta, escribe un número");
                return;
            }

            if (vel2 <= 0)
            {
                MessageBox.Show("Vuelo 2: Velocidad incorrecta, escribe un número mayor que 0");
                return;

            }


            //POSICION INICIAL Y FINAL 2 NO IGUALES
            if (x2 == xf2 && y2 == yf2)
            {
                MessageBox.Show("Vuelo 2: Posición inicial y final no pueden ser iguales");
                return;
            }

            //COMPROBACION ID NO IGUALES
            if (id1.ToUpper() == id2.ToUpper())
            //Añadimos ToUpper()para que si lo escribimos en minúsculas también se acepte
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
