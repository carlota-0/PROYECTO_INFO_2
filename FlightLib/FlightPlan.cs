using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class GestionUsuarios
    {
        // Atributo privado para mantener la conexión
        private SQLiteConnection cnx;

        // Abre la conexión y se asegura de que la tabla exista
        public void Iniciar()
        {
            string dataSource = "Data Source=usuarios.db";
            cnx = new SQLiteConnection(dataSource);
            cnx.Open();

            // Magia: Si el archivo está vacío, crea la tabla automáticamente
            string sql = "CREATE TABLE IF NOT EXISTS usuarios (username varchar primarykey, password varchar)";
            SQLiteCommand cmd = new SQLiteCommand(sql, cnx);
            cmd.ExecuteNonQuery();
        }

        // Cierra la conexión
        public void Cerrar()
        {
            cnx.Close();
        }

        // Comprueba si el usuario y la contraseña coinciden en la base de datos
        public bool ComprobarLogin(string user, string pass)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM usuarios WHERE username='" + user + "' AND password='" + pass + "'";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, cnx);
            adp.Fill(dt);

            // Si encuentra exactamente 1 fila, las credenciales son correctas
            if (dt.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Registra un nuevo usuario en la base de datos
        public int RegistrarUsuario(string user, string pass)
        {
            // 1. Primero comprobamos si el nombre de usuario ya existe
            DataTable dt = new DataTable();
            string sqlCheck = "SELECT * FROM usuarios WHERE username='" + user + "'";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(sqlCheck, cnx);
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return -1; // Código de error: el usuario ya existe
            }

            // 2. Si no existe, lo insertamos
            string sql = "INSERT INTO usuarios VALUES ('" + user + "', '" + pass + "')";
            SQLiteCommand command = new SQLiteCommand(sql, cnx);
            int affectedRows = command.ExecuteNonQuery();

            if (affectedRows == 1)
            {
                return 0; // Código de éxito
            }
            else
            {
                return -2; // Código de error desconocido
            }
        }
    }
    public class FlightPlan
    {
        // =========================================================================
        // ATRIBUTOS
        // =========================================================================
        private string id;
        private string company;
        private Position initialPosition;
        private Position currentPosition;
        private Position finalPosition;
        private double velocidad;
        private Stack<Position> historialPosiciones;

        //CONSTRUCTORES
        //----------------------------------------------
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
        //----------------------------------------
        public string GetId() 
        { return id; }
        public void SetId(string id) 
        { this.id = id; }
        public string GetCompany()
        {
            return company;
        }
        public void SetCompany(string company)
        {
            this.company = company;
        }
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

        //MÉTODOS
        //--------------------------------------------------

        // Comprueba que el avión ha llegado a su destino
        public bool EstaDestino()
        {
            return (currentPosition.GetX() == finalPosition.GetX() &&
                    currentPosition.GetY() == finalPosition.GetY());
        }

        // Mueve el avión según el tiempo introducido (tiempo de ciclo)
        public void Mover(double tiempo)
        {

            historialPosiciones.Push(new Position(currentPosition.GetX(), currentPosition.GetY()));

            //Calculamos la distancia recorrida en el tiempo dado
            double distancia = tiempo * this.velocidad / 60;

            //Calculamos las razones trigonométricas
            double hipotenusa = Math.Sqrt((finalPosition.GetX() - currentPosition.GetX()) * (finalPosition.GetX() - currentPosition.GetX()) + (finalPosition.GetY() - currentPosition.GetY()) * (finalPosition.GetY() - currentPosition.GetY()));
            if (hipotenusa == 0)
                return;

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

        // Vuelve el avión a la posición inicial
        public void Restart()
        {
            this.currentPosition = new Position(initialPosition.GetX(), initialPosition.GetY());
            this.historialPosiciones.Clear(); //vacia su historial de movimientos
        }
        //Deshace el último paso del avión
        public bool DeshacerPaso()
        {
            if (historialPosiciones.Count > 0)
            {
                currentPosition = historialPosiciones.Pop();
                return true;
            }
            return false;
        }

        // Prolonga el destino hasta el borde del sector: el vuelo cruza todo el sector
        // y "termina" cuando sale por el otro lado
        public void ExtenderHastaBordeSector(double anchoSector, double altoSector)
        {
            double x0 = initialPosition.GetX();
            double y0 = initialPosition.GetY();
            double dx = finalPosition.GetX() - x0;
            double dy = finalPosition.GetY() - y0;

            // t = cuántas veces hay que repetir el vector (dx, dy) para llegar al borde
            double t = double.MaxValue;

            if (dx > 0) t = Math.Min(t, (anchoSector - x0) / dx);
            else if (dx < 0) t = Math.Min(t, (0 - x0) / dx);

            if (dy > 0) t = Math.Min(t, (altoSector - y0) / dy);
            else if (dy < 0) t = Math.Min(t, (0 - y0) / dy);

            // Si no hay dirección o el vuelo ya está fuera del sector, no cambiamos nada
            if (t == double.MaxValue || t <= 0)
                return;

            finalPosition = new Position(x0 + t * dx, y0 + t * dy);
        }

        // Devuelve la distancia con otro plan de vuelo pasado por parámetro
        public double Distancia(FlightPlan b)
        {
            return this.currentPosition.Distancia(b.GetCurrentPosition());
        }

        //FASE 10
        // Comprueba si hay peligro de colisión con otro avión según la distancia de seguridad
        public bool Conflicto(FlightPlan b, double distanciaSeguridad)
        {
            return (this.Distancia(b) < distanciaSeguridad*2);
        }

    
        // Simula el futuro con copias y devuelve true si en algún momento
        // los dos vuelos están a menos de la distancia de seguridad
        // Utilizamos método de la derivada para simular lo que pasará
        public bool HabraConflicto(FlightPlan b, double distanciaSeguridad)
        {
            
            // Si ya están en conflicto ahora mismo, devolvemos true
            if (this.Conflicto(b, distanciaSeguridad))
                return true;

            // 1. Calculamos las componentes vectoriales (Vx, Vy) del Avión A 
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

            // 2. Calculamos las componentes vectoriales (Vx, Vy) del Avión B
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

            // 3. Diferencias de posición y velocidad (A - B)
            double dX0 = currentPosition.GetX() - b.GetCurrentPosition().GetX();
            double dY0 = currentPosition.GetY() - b.GetCurrentPosition().GetY();
            double dVx = vxA - vxB;
            double dVy = vyA - vyB;

            // 4. Derivada de la distancia igualada a cero (para buscar el tiempo mínimo)
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

            //Separación total requerida:
            double separacionRequerida = distanciaSeguridad * 2;
            return distanciaMinimaAlCuadrado < (separacionRequerida*separacionRequerida);
        }

        //antes de establecer velocidad, pregunta si queremos 
        public double ObtenerVelocidadPropuesta(FlightPlan b, double distanciaSeguridad)
        {
            double velocidadOriginal = b.GetVelocidad();
            int porcentaje;

            // Probar a bajar velocidad
            porcentaje = 95;
            while (porcentaje >= 10)
            {
                b.SetVelocidad(velocidadOriginal * porcentaje / 100.0);
                if (this.HabraConflicto(b, distanciaSeguridad) == false)
                {
                    b.SetVelocidad(velocidadOriginal); // Restaurar
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
                    b.SetVelocidad(velocidadOriginal); // Restaurar
                    return velocidadOriginal * porcentaje / 100.0;
                }
                porcentaje = porcentaje + 5;
            }

            b.SetVelocidad(velocidadOriginal); // Restaurar
            return -1; // No se encontró solución
        }
        //Cambia la velocidad del vuelo b hasta que no haya conflicto.
        //Devuelve true si lo consigue y false si no hay solución:
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