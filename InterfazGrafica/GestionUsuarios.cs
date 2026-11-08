using System;
using System.Data;
using System.Data.SQLite; 

namespace InterfazGrafica
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
}