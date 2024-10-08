using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Utilidades
    {
        private static SqlConnection connection;

        public Utilidades() 
        {
            
        }

        public Utilidades(string userid, string password)
        {
            SqlServer = "127.0.0.1";  // O reemplaza con el valor adecuado
            SqlDataBase = "AbarroteDB";  // O reemplaza con el valor adecuado
            SqlUserId = userid;
            SqlPassword = password;
        }

        public static string SqlServer { get; set; }
        public static string SqlDataBase { get; set; }
        public string SqlUserId { get; set; }
        public string SqlPassword { get; set; }

        public string ConnectionString
        {
            get
            {
                return $"Server={SqlServer};Database={SqlDataBase};User Id={SqlUserId};Password={SqlPassword};";
            }
        }

        public SqlConnection Conexion()
        {
            if (connection == null)
            {
                connection = new SqlConnection(ConnectionString);
            }
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                return connection;
            }
            catch (Exception ex)
            {
                string respuesta = ex.Message;
                return null;
            }
        }

        public static void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

    }
}
