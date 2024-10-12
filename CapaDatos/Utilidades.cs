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

        private static SqlConnection connection; // Conexión estática

        // Propiedades estáticas
        public static string SqlServer { get; set; } = "127.0.0.1";  // Puedes establecer un valor predeterminado
        public static string SqlDataBase { get; set; } = "AbarroteDB";  // Puedes establecer un valor predeterminado

        // Propiedades para el usuario y la contraseña
        public static string SqlUserId { get; set; }
        public static string SqlPassword { get; set; }

        // Propiedad para obtener la cadena de conexión
        private static string ConnectionString
        {
            get
            {

                // Validar que el usuario y la contraseña no sean nulos o vacíos
                if (string.IsNullOrEmpty(SqlUserId) || string.IsNullOrEmpty(SqlPassword))
                {
                    throw new InvalidOperationException("El usuario y la contraseña no pueden estar vacíos.");
                }

                return $"Server={SqlServer};Database={SqlDataBase};User Id={SqlUserId};Password={SqlPassword};";
            }
        }

        // Método para obtener la conexión
        public static SqlConnection Conexion()
        {
            if (string.IsNullOrEmpty(SqlUserId) || string.IsNullOrEmpty(SqlPassword))
            {
                throw new InvalidOperationException("El usuario y la contraseña no pueden estar vacíos.");
            }

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
                throw new InvalidOperationException("Error al abrir la conexión: " + ex.Message);
            }
        }

        // Método para cerrar la conexión
        public static void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection = null; // Establecer a null para permitir una nueva conexión en el futuro
            }
        }

    }
}
