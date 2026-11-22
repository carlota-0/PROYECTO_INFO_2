
using System;
using System.Collections.Generic;
using System.IO;

namespace FlightLib
{
    public class FlightPlanList
    {
        private List<FlightPlan> lista = new List<FlightPlan>();

        // Añade un plan de vuelo a la lista y devuelve el índice asignado
        public int AddFlightPlan(FlightPlan p)
        {
            lista.Add(p);
            return lista.Count - 1;
        }

        // Devuelve el número total de vuelos almacenados en la lista
        public int GetCount()
        {
            return lista.Count;
        }

        //Devuelve el plan de vuelo ubicado en el índice indicado
        public FlightPlan GetFlightPlan(int i)
        {
            if (i < 0 || i >= lista.Count)
            {
                return null;
            }
            else
            {
                return lista[i];
            }
        }

        //GESTIÓN DE ARCHIVOS
        //-------------------

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

                    if (ix == fx && iy == fy)
                    {
                        throw new FormatException("La posición inicial y final de un vuelo no pueden ser iguales.");
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

        // Guarda el estado actual de todos los vuelos en un archivo de texto
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

        //CONTROL SIMULACIÓN
        //------------------

        public void Mover(double tiempo)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                lista[i].Mover(tiempo);
            }
        }

        // Deshace el último paso de todos los vuelos de la lista; devuelve true si había pasos para deshacer
        public bool DeshacerPaso()
        {
            bool deshecho = false;
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].DeshacerPaso())
                {
                    deshecho = true;
                }
            }
            return deshecho;
        }

        // Reinicia todos los vuelos de la lista a su posición inicial
        public void Restart()
        {
            for (int i = 0; i < lista.Count; i++)
            {
                lista[i].Restart();
            }
        }

        //DETECCIÓN CONFLICTOS Y PROPUESTA
        //--------------------------------

        //Detecta parejas de vuelos con conflicto futuro
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

        // Comprueba si el vuelo situado en la posición indicada entra en conflicto futuro con algún otro vuelo de la lista (parámetros: índice y distancia de seguridad; devuelve true si hay conflicto)
        private bool TieneConflictoConAlguno(int indice, double distanciaSeguridad)
        {
            FlightPlan vuelo = lista[indice];

            for (int k = 0; k < lista.Count; k++)
            {
                if (k != indice)
                {
                    if (vuelo.HabraConflicto(lista[k], distanciaSeguridad))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //Calcula velocidades propuestas para resolver conflictos
        public double[] ProponerVelocidades(double distanciaSeguridad, out bool resuelto)
        {
            int cantidad = lista.Count;

            double[] velocidadesOriginales = new double[cantidad];
            for (int i = 0; i < cantidad; i++)
            {
                velocidadesOriginales[i] = lista[i].GetVelocidad();
            }

            for (int pasada = 0; pasada < 3; pasada++)
            {
                for (int i = 0; i < cantidad; i++)
                {
                    if (TieneConflictoConAlguno(i, distanciaSeguridad))
                    {
                        FlightPlan vuelo = lista[i];
                        double vActual = vuelo.GetVelocidad();
                        bool encontrado = false;

                        // Primero probamos a reducir la velocidad del 95 % al 10 % en pasos del 5 %
                        for (int porcentaje = 95; porcentaje >= 10; porcentaje = porcentaje - 5)
                        {
                            vuelo.SetVelocidad(vActual * porcentaje / 100.0);
                            if (!TieneConflictoConAlguno(i, distanciaSeguridad))
                            {
                                encontrado = true;
                                break;
                            }
                        }

                        // Si igualmente no se evita el conflicto, probamos a aumentarla del 105 % al 200 %
                        if (!encontrado)
                        {
                            for (int porcentaje = 105; porcentaje <= 200; porcentaje = porcentaje + 5)
                            {
                                vuelo.SetVelocidad(vActual * porcentaje / 100.0);
                                if (!TieneConflictoConAlguno(i, distanciaSeguridad))
                                {
                                    encontrado = true;
                                    break;
                                }
                            }
                        }


                        if (!encontrado)
                        {
                            vuelo.SetVelocidad(vActual);
                        }
                    }
                }
            }

            List<string> conflictosRestantes = DetectarConflictos(distanciaSeguridad);

            if (conflictosRestantes.Count == 0)
            {
                resuelto = true;
            }
            else
            {
                resuelto = false;
            }

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

    

        
    

    
        

           
        

    


