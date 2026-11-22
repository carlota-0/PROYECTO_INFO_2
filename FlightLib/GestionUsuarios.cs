using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace FlightLib
{
    public class GestionUsuarios
    {
        private SQLiteConnection cnx;
        private string rutaBaseDatos = "usuarios.db";

        // Comprueba si existe la base de datos y abre la conexión.
        // Parámetros: ninguno. Devuelve: true si se abre correctamente, false si no existe el archivo.
        public bool Iniciar()
        {
            if (!File.Exists(rutaBaseDatos))
            {
                return false;
            }
            cnx = new SQLiteConnection("Data Source=" + rutaBaseDatos);
            cnx.Open();
            return true;
        }

        // Cierra la conexión con la base de datos si está abierta.
        // Parámetros: ninguno. Devuelve: nada (void).
        public void Cerrar()
        {
            if (cnx != null && cnx.State == ConnectionState.Open)
            {
                cnx.Close();
            }
        }

        // Comprueba si el usuario y la contraseña coinciden en la base de datos.
        // Parámetros: user (nombre de usuario), pass (contraseña). Devuelve: true si existe, false si no.
        public bool ComprobarLogin(string user, string pass)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM usuarios WHERE username='" + user + "' AND password='" + pass + "'";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, cnx);
            adp.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Registra un nuevo usuario en la base de datos si no existe previamente.
        // Parámetros: user (nombre de usuario), pass (contraseña). Devuelve: 0 si se registra, -1 si ya existe, -2 si hay error.
        public int RegistrarUsuario(string user, string pass)
        {
            // Comprobamos si el nombre de usuario ya existe
            DataTable dt = new DataTable();
            string sqlCheck = "SELECT * FROM usuarios WHERE username='" + user + "'";
            SQLiteDataAdapter adp = new SQLiteDataAdapter(sqlCheck, cnx);
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return -1;
            }

            // Si no existe, lo insertamos
            string sql = "INSERT INTO usuarios VALUES ('" + user + "', '" + pass + "')";
            SQLiteCommand command = new SQLiteCommand(sql, cnx);
            int affectedRows = command.ExecuteNonQuery();

            if (affectedRows == 1)
            {
                return 0;
            }
            else
            {
                return -2;
            }
        }
    }
}