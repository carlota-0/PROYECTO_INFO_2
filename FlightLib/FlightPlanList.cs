
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



    }
    }

        
    

    
        

           
        

    


