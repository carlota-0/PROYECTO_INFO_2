using FlightLib;
using System;
using System.Windows.Forms;

namespace InterfazGrafica
{
    public partial class FormPlanesVuelo : Form
    {
        private FlightPlan Plan1;
        private FlightPlan Plan2;

        public FormPlanesVuelo()
        {
            InitializeComponent();

            //Boton cancelar cierra el form sin guardar nada
            btnCancelar.DialogResult = DialogResult.Cancel;
        }
        public FlightPlan GetPlan1()
        {
            return Plan1;
        }

        public FlightPlan GetPlan2()
        {
            return Plan2;
        }

        //Rellena los campos de ambos vuelos con valores por defecto
        private void btnDefecto_Click(object sender, EventArgs e)
        {
            textID1.Text = "IBE123";
            textCompania1.Text = "Iberia";
            Ini1.Text = "50,50";
            Fin1.Text = "400,300";
            textVelocidad1.Text = "150";

            textID2.Text = "RYR456";
            textCompania2.Text = "Ryanair";
            Ini2.Text = "400,50";
            Fin2.Text = "50,300";
            textVelocidad2.Text = "150";
        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            double x1 = 0, y1 = 0, xf1 = 0, yf1 = 0, vel1 = 0;
            double x2 = 0, y2 = 0, xf2 = 0, yf2 = 0, vel2 = 0;

            //VUELO 1

            string id1 = textID1.Text.Trim(); 
            if (id1 == "")
            {
                MessageBox.Show("Vuelo 1: ID vacía, escribe un identificador");
                return;
            }

            string comp1 = textCompania1.Text.Trim();
            if (comp1 == "")
            {
                MessageBox.Show("Vuelo 1: Compañía vacía, escribe una compañía.");
                return;
            }

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

            if (x1 == xf1 && y1 == yf1)
            {
                MessageBox.Show("Vuelo 1: Posición inicial y final no pueden ser iguales");
                return;
            }

            //VUELO2

            string id2 = textID2.Text.Trim();
            if (id2 == "")
            {
                MessageBox.Show("Vuelo 2: ID vacía, escribe un identificador");
                return;

            }
            string comp2 = textCompania2.Text.Trim();
            if (comp2 == "")
            {
                MessageBox.Show("Vuelo 2: Compañía vacía, escribe una compañía.");
                return;
            }

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

            if (x2 == xf2 && y2 == yf2)
            {
                MessageBox.Show("Vuelo 2: Posición inicial y final no pueden ser iguales");
                return;
            }

            if (id1.ToUpper() == id2.ToUpper())
            {
                MessageBox.Show("Los identificadores de los vuelos no pueden ser iguales");
                return;
            }

            Plan1 = new FlightPlan(id1, comp1, x1, y1, xf1, yf1, vel1);
            Plan2 = new FlightPlan(id2, comp2, x2, y2, xf2, yf2, vel2);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void FormPlanesVuelo_Load(object sender, EventArgs e)
        {
        }
    }
}
