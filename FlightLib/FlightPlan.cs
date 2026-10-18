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
        public bool HabraConflicto(FlightPlan b, double distanciaSeguridad, double paso)
        {
            FlightPlan a1 = this.Copia();
            FlightPlan b1 = b.Copia();

            // Si ya están en conflicto ahora mismo
            if (a1.Conflicto(b1, distanciaSeguridad))
            {
                return true;
            }

            // Seguimos mientras algún vuelo se pueda seguir moviendo
            while ((a1.velocidad > 0 && !a1.EstaDestino()) || (b1.velocidad > 0 && !b1.EstaDestino()))
            {
                a1.Mover(paso);
                b1.Mover(paso);

                if (a1.Conflicto(b1, distanciaSeguridad))
                {
                    return true;
                }
            }

            return false;
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