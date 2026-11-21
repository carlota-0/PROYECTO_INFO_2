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

        // Comprueba si existe la base de datos y abre la conexión
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

        // Cierra la conexión con la base de datos 
        public void Cerrar()
        {
            if (cnx != null && cnx.State == ConnectionState.Open)
            {
                cnx.Close();
            }
        }

        // Comprueba si el usuario y la contraseña coinciden en la base de datos
        public bool ComprobarLogin(string user, string pass)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM usuarios WHERE username=@user AND password=@pass";
            SQLiteCommand cmd = new SQLiteCommand(sql, cnx);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", pass);

            SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
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

        // Registra un nuevo usuario en la base de datos
        public int RegistrarUsuario(string user, string pass)
        {
            // 1. Primero comprobamos si el nombre de usuario ya existe
            DataTable dt = new DataTable();
            string sqlCheck = "SELECT * FROM usuarios WHERE username=@user";
            SQLiteCommand cmdCheck = new SQLiteCommand(sqlCheck, cnx);
            cmdCheck.Parameters.AddWithValue("@user", user);

            SQLiteDataAdapter adp = new SQLiteDataAdapter(cmdCheck);
            adp.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return -1; 
            }

            // 2. Si no existe, lo insertamos
            string sql = "INSERT INTO usuarios VALUES (@user, @pass)";
            SQLiteCommand command = new SQLiteCommand(sql, cnx);
            command.Parameters.AddWithValue("@user", user);
            command.Parameters.AddWithValue("@pass", pass);

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
