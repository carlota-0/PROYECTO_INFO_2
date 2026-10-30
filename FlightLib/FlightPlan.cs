using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlan
    {
        // =========================================================================
        // ATRIBUTOS
        // =========================================================================
        private string id;
        private Position initialPosition;
        private Position currentPosition;
        private Position finalPosition;
        private double velocidad;

        // =========================================================================
        // CONSTRUCTOR
        // =========================================================================
        public FlightPlan(string id, double cpx, double cpy, double fpx, double fpy, double velocidad)
        {
            this.id = id;
            this.initialPosition = new Position(cpx, cpy);
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.velocidad = velocidad;
        }

        // =========================================================================
        // GETTERS Y SETTERS
        // =========================================================================
        public string GetId() 
        { return id; }
        public void SetId(string id) 
        { this.id = id; }
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

        // =========================================================================
        // MÉTODOS PRINCIPALES DE LA SIMULACIÓN (Fase 1)
        // =========================================================================

        // Devuelve true si el avión ha llegado a su destino
        public bool EstaDestino()
        {
            return (currentPosition.GetX() == finalPosition.GetX() &&
                    currentPosition.GetY() == finalPosition.GetY());
        }

        // Mueve el avión según el tiempo introducido
        public void Mover(double tiempo)
        {
            // Si ya ha llegado, no se mueve más
            if ((currentPosition.GetX() == finalPosition.GetX() &&
                    currentPosition.GetY() == finalPosition.GetY()))
            {
                return;
            }

            //Calculamos la distancia recorrida en el tiempo dado
            double distancia = tiempo * this.velocidad / 60;

            //Calculamos las razones trigonométricas
            double hipotenusa = Math.Sqrt((finalPosition.GetX() - currentPosition.GetX()) * (finalPosition.GetX() - currentPosition.GetX()) + (finalPosition.GetY() - currentPosition.GetY()) * (finalPosition.GetY() - currentPosition.GetY()));
            double coseno = (finalPosition.GetX() - currentPosition.GetX()) / hipotenusa;
            double seno = (finalPosition.GetY() - currentPosition.GetY()) / hipotenusa;

            //Caculamos la nueva posición del vuelo
            double x = currentPosition.GetX() + distancia * coseno;
            double y = currentPosition.GetY() + distancia * seno;

            //Modificar MoverVuelo para que no se pase del destino
            Position nextPosition = new Position(x, y);
            if (currentPosition.Distancia(nextPosition) < hipotenusa)
                currentPosition = nextPosition;
            else
                currentPosition = finalPosition;
        }

        // Vuelve el avión a la posición inicial
        public void Restart()
        {
            this.currentPosition = new Position(initialPosition.GetX(), initialPosition.GetY());
        }

        // Devuelve la distancia con otro plan de vuelo pasado por parámetro
        public double Distancia(FlightPlan b)
        {
            return this.currentPosition.Distancia(b.GetCurrentPosition());
        }

        // Comprueba si hay peligro de colisión con otro avión según la distancia de seguridad
        public bool Conflicto(FlightPlan b, double distanciaSeguridad)
        {
            return (this.Distancia(b) < distanciaSeguridad);
        }

        // =========================================================================
        // FASE 11: PREDICCIÓN Y RESOLUCIÓN DE CONFLICTOS
        // =========================================================================

        // Crea una copia del vuelo (para poder simular sin tocar el vuelo real)
        private FlightPlan Copia()
        {
            FlightPlan copia = new FlightPlan(id,
                currentPosition.GetX(), currentPosition.GetY(),
                finalPosition.GetX(), finalPosition.GetY(),
                velocidad);
            return copia;
        }

        // Simula el futuro con copias y devuelve true si en algún momento
        // los dos vuelos están a menos de la distancia de seguridad
        // Simula el futuro matemáticamente (método de la derivada)
        public bool HabraConflicto(FlightPlan b, double distanciaSeguridad, double paso)
        {
            // Si ya están en conflicto ahora mismo, devolvemos true
            if (this.Conflicto(b, distanciaSeguridad))
                return true;

            // 1. Calculamos las componentes vectoriales (Vx, Vy) del Avión A (this)
            double dxA = finalPosition.GetX() - currentPosition.GetX();
            double dyA = finalPosition.GetY() - currentPosition.GetY();
            double hipA = Math.Sqrt(dxA * dxA + dyA * dyA);
            double vxA = (hipA > 0) ? (dxA / hipA) * velocidad : 0;
            double vyA = (hipA > 0) ? (dyA / hipA) * velocidad : 0;

            // 2. Calculamos las componentes vectoriales (Vx, Vy) del Avión B
            double dxB = b.GetFinalPosition().GetX() - b.GetCurrentPosition().GetX();
            double dyB = b.GetFinalPosition().GetY() - b.GetCurrentPosition().GetY();
            double hipB = Math.Sqrt(dxB * dxB + dyB * dyB);
            double vxB = (hipB > 0) ? (dxB / hipB) * b.GetVelocidad() : 0;
            double vyB = (hipB > 0) ? (dyB / hipB) * b.GetVelocidad() : 0;

            // 3. Diferencias de posición y velocidad (A - B)
            double dX0 = currentPosition.GetX() - b.GetCurrentPosition().GetX();
            double dY0 = currentPosition.GetY() - b.GetCurrentPosition().GetY();
            double dVx = vxA - vxB;
            double dVy = vyA - vyB;

            // 4. Derivada de la distancia igualada a cero (para hallar el tiempo mínimo)
            // Fórmula: t_min = -(dX0*dVx + dY0*dVy) / (dVx^2 + dVy^2)
            double denom = (dVx * dVx) + (dVy * dVy);

            // Si el denominador es 0, van a la misma velocidad y dirección (nunca se cruzan)
            if (denom == 0) return false;

            double t_min = -(dX0 * dVx + dY0 * dVy) / denom;

            // Si el tiempo es negativo, el punto de máximo acercamiento ocurrió en el pasado
            if (t_min < 0) return false;

            // 5. Calculamos la distancia mínima que habrá en ese instante t_min
            double distX_min = dX0 + dVx * t_min;
            double distY_min = dY0 + dVy * t_min;
            
            // Calculamos la distancia al cuadrado para ahorrar un Math.Sqrt()
            double distanciaMinimaAlCuadrado = (distX_min * distX_min) + (distY_min * distY_min);

            // Nota sobre el punto 3: Si el profesor considera "distancia de seguridad" 
            // como el radio de un solo avión, aquí deberías compararlo con (2 * distanciaSeguridad) al cuadrado.
            // Si es la separación total requerida, lo dejamos así:
            return distanciaMinimaAlCuadrado < (distanciaSeguridad * distanciaSeguridad);
        }

        // Cambia la velocidad del vuelo b hasta que no haya conflicto.
        // Devuelve true si lo consigue y false si no hay solución.
        public bool ResolverConflicto(FlightPlan b, double distanciaSeguridad, double paso)
        {
            double velocidadOriginal = b.GetVelocidad();
            int porcentaje;

            // Primero probamos a bajar la velocidad: 95%, 90%, 85% ... 10%
            porcentaje = 95;
            while (porcentaje >= 10)
            {
                b.SetVelocidad(velocidadOriginal * porcentaje / 100);
                if (this.HabraConflicto(b, distanciaSeguridad, paso) == false)
                {
                    return true;
                }
                porcentaje = porcentaje - 5;
            }

            // Si no funciona, probamos a subirla: 105%, 110% ... 200%
            porcentaje = 105;
            while (porcentaje <= 200)
            {
                b.SetVelocidad(velocidadOriginal * porcentaje / 100);
                if (this.HabraConflicto(b, distanciaSeguridad, paso) == false)
                {
                    return true;
                }
                porcentaje = porcentaje + 5;
            }

            // No ha habido suerte: dejamos la velocidad como estaba
            b.SetVelocidad(velocidadOriginal);
            return false;
        }

        // =========================================================================
        // MÉTODOS AUXILIARES
        // =========================================================================
        public void EscribeConsola()
        {
            Console.WriteLine("******************************");
            Console.WriteLine("Datos del vuelo: ");
            Console.WriteLine("Identificador: {0}", id);
            Console.WriteLine("Velocidad: {0:F2}", velocidad);
            Console.WriteLine("Posición actual: ({0:F2},{1:F2})", currentPosition.GetX(), currentPosition.GetY());

            if (this.EstaDestino())
                Console.WriteLine("Ha llegado al destino");

            Console.WriteLine("******************************");
        }
    }
}