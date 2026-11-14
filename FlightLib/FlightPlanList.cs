
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlanList
    {
        //VERSION 2
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
    

        
    

    
        

           
        

    


