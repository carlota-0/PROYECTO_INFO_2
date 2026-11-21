using System;
using System.Collections.Generic;

namespace FlightLib
{
    public class FlightPlan
    {
       //ATRIBUTOS
        private string id;
        private string company;
        private Position initialPosition;
        private Position currentPosition;
        private Position finalPosition;
        private double velocidad;
        private Stack<Position> historialPosiciones;

        //CONSTRUCTORES
        public FlightPlan(string id, string company, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;
            this.company = company;
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidad = velocidad;
            this.historialPosiciones = new Stack<Position>();
        }

        //GETTERS Y SETTERS
        public string GetId() 
        { return id; }
        public void SetId(string id) 
        { this.id = id; }
        public string GetCompany()
        { return company; }
        public void SetCompany(string company)
        { this.company = company; }
        public Position GetCurrentPosition() 
        { return currentPosition; }
        public Position GetFinalPosition() 
        { return finalPosition; }
        public Position GetInitialPosition() 
        { return initialPosition; }
        public double GetVelocidad() 
        { return velocidad; }
        public void SetVelocidad(double velocidad) 
        { this.velocidad = velocidad; }


        //Prolonga la trayectoria del avión hasta el borde del sector
        public void ExtenderHastaBordeSector(double anchoSector, double altoSector)
        {
            double x0 = initialPosition.GetX();
            double y0 = initialPosition.GetY();
            double dx = finalPosition.GetX() - x0;
            double dy = finalPosition.GetY() - y0;

            // t = cuántas veces hay que repetir el vector (dx, dy) para llegar al borde
            double t = double.MaxValue;

            if (dx > 0)
            {
                t = Math.Min(t, (anchoSector - x0) / dx);
            }
            else if (dx < 0)
            {
                t = Math.Min(t, (0 - x0) / dx);
            }

            if (dy > 0)
            {
                t = Math.Min(t, (altoSector - y0) / dy);
            }
            else if (dy < 0)
            {
                t = Math.Min(t, (0 - y0) / dy);
            }

            // Si no hay dirección o el vuelo ya está fuera del sector, no cambiamos nada
            if (t == double.MaxValue || t <= 0)
                return;

            finalPosition = new Position(x0 + t * dx, y0 + t * dy);
        }

        //MOVIMIENTO 
        //----------

        // Mueve el avión según el tiempo introducido (tiempo de ciclo)
        public void Mover(double tiempo)
        {
            //Calculamos las razones trigonométricas
            double hipotenusa = Math.Sqrt((finalPosition.GetX() - currentPosition.GetX()) * (finalPosition.GetX() - currentPosition.GetX()) + (finalPosition.GetY() - currentPosition.GetY()) * (finalPosition.GetY() - currentPosition.GetY()));
            if (hipotenusa == 0)
                return;

            historialPosiciones.Push(new Position(currentPosition.GetX(), currentPosition.GetY()));

            //Calculamos la distancia recorrida en el tiempo dado
            double distancia = tiempo * this.velocidad / 60;

            double coseno = (finalPosition.GetX() - currentPosition.GetX()) / hipotenusa;
            double seno = (finalPosition.GetY() - currentPosition.GetY()) / hipotenusa;

            //Caculamos la nueva posición del vuelo
            double x = currentPosition.GetX() + distancia * coseno;
            double y = currentPosition.GetY() + distancia * seno;

            //Modificar Mover para que no se pase del destino
            Position nextPosition = new Position(x, y);
            if (currentPosition.Distancia(nextPosition) < hipotenusa)
                currentPosition = nextPosition;
            else
                currentPosition = finalPosition;
        }

        //Recupera la posición anterior del avión
        public bool DeshacerPaso()
        {
            if (historialPosiciones.Count > 0)
            {
                currentPosition = historialPosiciones.Pop();
                return true;
            }
            return false;
        }

        // Comprueba que el avión ha llegado a su destino; si ha legado devuelve true
        public bool EstaDestino()
        {
            return (currentPosition.GetX() == finalPosition.GetX() &&
                    currentPosition.GetY() == finalPosition.GetY());
        }

        //Reinicia el vuelo a su posición inicial y vacia el historial de posiciones
        public void Restart()
        {
            this.currentPosition = new Position(initialPosition.GetX(), initialPosition.GetY());
            this.historialPosiciones.Clear(); //vacia su historial de movimientos
        }

        //CÁLCULO DISTANCIA Y DETECCIÓN CONFLICTOS
        //-----------------------------------------

        // Devuelve la distancia con otro plan de vuelo pasado por parámetro
        public double Distancia(FlightPlan b)
        {
            return this.currentPosition.Distancia(b.GetCurrentPosition());
        }

        // Comprueba si hay peligro de colisión con otro avión según la distancia de seguridad
        public bool Conflicto(FlightPlan b, double distanciaSeguridad)
        {
            return (this.Distancia(b) < distanciaSeguridad*2);
        }


        // Utilizamos método de la derivada para simular lo que pasará
        public bool HabraConflicto(FlightPlan b, double distanciaSeguridad)
        {
            if (this.Conflicto(b, distanciaSeguridad))
                return true;

            double dxA = finalPosition.GetX() - currentPosition.GetX();
            double dyA = finalPosition.GetY() - currentPosition.GetY();
            double hipA = Math.Sqrt(dxA * dxA + dyA * dyA);

            double vxA = 0;
            double vyA = 0;
            if (hipA > 0)
            {
                vxA = (dxA / hipA) * (velocidad / 60.0);
                vyA = (dyA / hipA) * (velocidad / 60.0);
            }

            double dxB = b.GetFinalPosition().GetX() - b.GetCurrentPosition().GetX();
            double dyB = b.GetFinalPosition().GetY() - b.GetCurrentPosition().GetY();
            double hipB = Math.Sqrt(dxB * dxB + dyB * dyB);

            double vxB = 0;
            double vyB = 0;
            if (hipB > 0)
            {
                vxB = (dxB / hipB) * (b.GetVelocidad() / 60.0);
                vyB = (dyB / hipB) * (b.GetVelocidad() / 60.0);
            }

            double dX0 = currentPosition.GetX() - b.GetCurrentPosition().GetX();
            double dY0 = currentPosition.GetY() - b.GetCurrentPosition().GetY();
            double dVx = vxA - vxB;
            double dVy = vyA - vyB;

            // Derivada de la distancia igualada a cero (para buscar el tiempo mínimo)
            // Fórmula: t_min = -(dX0*dVx + dY0*dVy) / (dVx^2 + dVy^2)
            double denom = (dVx * dVx) + (dVy * dVy);

            // Si el denominador es 0, van a la misma velocidad y dirección (nunca se cruzan)
            if (denom == 0) return false;

            double t_min = -(dX0 * dVx + dY0 * dVy) / denom;

            // Si el tiempo es negativo, el punto de máximo acercamiento ocurrió en el pasado
            if (t_min < 0) return false;

            //Calculamos la distancia mínima que habrá en ese instante t_min
            double distX_min = dX0 + dVx * t_min;
            double distY_min = dY0 + dVy * t_min;
            
            // Calculamos la distancia al cuadrado para ahorrar un Math.Sqrt()
            double distanciaMinimaAlCuadrado = (distX_min * distX_min) + (distY_min * distY_min);

            //Separación total requerida:
            double separacionRequerida = distanciaSeguridad * 2;
            return distanciaMinimaAlCuadrado < (separacionRequerida*separacionRequerida);
        }

        //RESOLUCIÓN DE CONFLICTOS
        //------------------------

        //Busca velocidad alternativa para evitar conflict
        public double ObtenerVelocidadPropuesta(FlightPlan b, double distanciaSeguridad)
        {
            double velocidadOriginal = b.GetVelocidad();
            int porcentaje = 95;

            // Probar a bajar velocidad
            while (porcentaje >= 10)
            {
                b.SetVelocidad(velocidadOriginal * porcentaje / 100.0);
                if (this.HabraConflicto(b, distanciaSeguridad) == false)
                {
                    b.SetVelocidad(velocidadOriginal); 
                    return velocidadOriginal * porcentaje / 100.0;
                }
                porcentaje = porcentaje - 5;
            }

            // Probar a subir velocidad
            porcentaje = 105;
            while (porcentaje <= 200)
            {
                b.SetVelocidad(velocidadOriginal * porcentaje / 100.0);
                if (this.HabraConflicto(b, distanciaSeguridad) == false)
                {
                    b.SetVelocidad(velocidadOriginal); 
                    return velocidadOriginal * porcentaje / 100.0;
                }
                porcentaje = porcentaje + 5;
            }

            b.SetVelocidad(velocidadOriginal);
            return -1;
        }
        //Modifica la velocidad del vuelo b para resolver un futuro conflicto
        public bool ResolverConflicto(FlightPlan b, double distanciaSeguridad)
        {
            double vPropuesta = ObtenerVelocidadPropuesta(b, distanciaSeguridad);
            if (vPropuesta > 0)
            {
                b.SetVelocidad(vPropuesta);
                return true;
            }
            return false;
        }
    }
}