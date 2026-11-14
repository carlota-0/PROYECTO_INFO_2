
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlanList
    {
        private List<FlightPlan> lista = new List<FlightPlan>();

        // Añade el vuelo y devuelve el índice donde lo ha guardado
        public int AddFlightPlan(FlightPlan p)
        {
            lista.Add(p); // Add hace todo el trabajo de meterlo al final
            return lista.Count - 1; // Count nos dice cuántos hay
        }
        public int GetCount()
        {
            return lista.Count;
        }

        public FlightPlan GetFlightPlan(int i)
        {
            // Count sustituye a tu antigua variable "number"
            if (i < 0 || i >= lista.Count)
            {
                return null;
            }
            else
            {
                // En C#, las List se leen con corchetes igual que los vectores
                return lista[i];
            }
        }

        public void Mover(double tiempo)
        {
            int i = 0;
            while (i < lista.Count)
            {
                lista[i].Mover(tiempo);
                i++;
            }
        }
        public void Restart()
        {
            for (int i = 0; i < lista.Count; i++)
            {
                lista[i].Restart();
            }
        }

        // Deshace el último paso de todos los vuelos de la lista; devuelve true si había pasos para deshacer
        public bool DeshacerPaso()
        {
            bool deshecho = false;
            int i = 0;
            while (i < lista.Count)
            {
                if (lista[i].DeshacerPaso())
                {
                    deshecho = true;
                }
                i++;
            }
            return deshecho;
        }

        // Lee los planes de vuelo desde un archivo de texto (parámetro: ruta del archivo)
        public void CargarDesdeArchivo(string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
            {
                throw new FileNotFoundException("No se encuentra el archivo especificado.");
            }

            List<FlightPlan> nuevaLista = new List<FlightPlan>();
            StreamReader r = new StreamReader(rutaArchivo);
            string linea = r.ReadLine();

            try
            {
                while (linea != null)
                {
                    if (linea.StartsWith("//") || linea.Trim() == "")
                    {
                        linea = r.ReadLine();
                        continue;
                    }

                    string[] trozos = linea.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (trozos.Length != 7)
                    {
                        throw new FormatException("Cada línea debe contener 7 campos: ID Compañía X0 Y0 Xf Yf Velocidad.");
                    }

                    string id = trozos[0];
                    string company = trozos[1];
                    double ix = Convert.ToDouble(trozos[2]);
                    double iy = Convert.ToDouble(trozos[3]);
                    double fx = Convert.ToDouble(trozos[4]);
                    double fy = Convert.ToDouble(trozos[5]);
                    double vel = Convert.ToDouble(trozos[6]);

                    if (vel <= 0)
                    {
                        throw new FormatException("La velocidad de todos los vuelos debe ser mayor que 0.");
                    }

                    FlightPlan avion = new FlightPlan(id, company, ix, iy, fx, fy, vel);
                    nuevaLista.Add(avion);
                    linea = r.ReadLine();
                }
            }
            finally
            {
                r.Close();
            }

            this.lista = nuevaLista;
        }

        // Guarda el estado actual de todos los vuelos en un archivo de texto (parámetro: ruta del archivo)
        public void GuardarEnArchivo(string rutaArchivo)
        {
            StreamWriter w = new StreamWriter(rutaArchivo);
            try
            {
                int i = 0;
                while (i < lista.Count)
                {
                    FlightPlan p = lista[i];
                    // Se guarda CurrentPosition como posición de partida para poder reanudar después
                    string linea = p.GetId() + " " +
                                   p.GetCompany() + " " +
                                   p.GetCurrentPosition().GetX() + " " +
                                   p.GetCurrentPosition().GetY() + " " +
                                   p.GetFinalPosition().GetX() + " " +
                                   p.GetFinalPosition().GetY() + " " +
                                   p.GetVelocidad();
                    w.WriteLine(linea);
                    i++;
                }
            }
            finally
            {
                w.Close();
            }
        }
        public List<string> DetectarConflictos(double distanciaSeguridad)
        {
            List<string> conflictos = new List<string>();

            for (int i = 0; i < lista.Count; i++)
            {
                for (int j = i + 1; j < lista.Count; j++)
                {
                    if (lista[i].HabraConflicto(lista[j], distanciaSeguridad))
                    {
                        conflictos.Add(lista[i].GetId() + " y " + lista[j].GetId());
                    }
                }
            }

            return conflictos;
        }
        public double[] ProponerVelocidades(double distanciaSeguridad, out bool resuelto)
        {
            int cantidad = lista.Count;

            // PASO 1: guardamos las velocidades de verdad (copia de seguridad)
            double[] velocidadesOriginales = new double[cantidad];
            for (int i = 0; i < cantidad; i++)
            {
                velocidadesOriginales[i] = lista[i].GetVelocidad();
            }

            // PASO 2: miramos todas las parejas de vuelos
            for (int i = 0; i < cantidad; i++)
            {
                for (int j = i + 1; j < cantidad; j++)
                {
                    FlightPlan vueloA = lista[i];
                    FlightPlan vueloB = lista[j];

                    // Si esta pareja va a chocar, intentamos arreglarlo cambiando la velocidad de B
                    if (vueloA.HabraConflicto(vueloB, distanciaSeguridad) == true)
                    {
                        vueloA.ResolverConflicto(vueloB, distanciaSeguridad);
                    }
                }
            }

            // PASO 3: comprobamos si después de los cambios todavía queda algún conflicto
            List<string> conflictosRestantes = DetectarConflictos(distanciaSeguridad);

            if (conflictosRestantes.Count == 0)
            {
                resuelto = true;    // no queda ninguno: todo arreglado
            }
            else
            {
                resuelto = false;   // todavía queda alguno sin arreglar
            }

            // PASO 4: guardamos las velocidades de prueba y devolvemos las originales a los vuelos
            double[] velocidadesPropuestas = new double[cantidad];
            for (int i = 0; i < cantidad; i++)
            {
                velocidadesPropuestas[i] = lista[i].GetVelocidad();
                lista[i].SetVelocidad(velocidadesOriginales[i]);
            }

            return velocidadesPropuestas;
        }


    }
}
    

        
    

    
        

           
        

    


