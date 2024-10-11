using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DLogin
    {
        private readonly string _SqlServidor;
        private readonly string _SqlDataBase;
        public string Respuesta = "";

        public DLogin(string sqlServidor, string sqlDataBase)
        {
            _SqlServidor = sqlServidor;
            _SqlDataBase = sqlDataBase;
        }

        public bool ValidarUsuarioSqlServer(string sqlUserId, string sqlPassword)
        {
            string connectionString = $"Server={_SqlServidor};Database={_SqlDataBase};User Id={sqlUserId};Password={sqlPassword};";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    return true; // Conexión exitosa
                }
                catch (Exception ex)
                {
                    Respuesta += ex.Message; // Captura el error
                    return false; // Fallo en la conexión
                }
            }
        }

        public bool AutenticarUsuario(string sqlUserId, string sqlPassword)
        {
            // Aquí puedes agregar la lógica para verificar el usuario en la base de datos.
            // Esto puede incluir la consulta de un hash de contraseña almacenada.
            using (var conexion = new SqlConnection($"Server={_SqlServidor};Database={_SqlDataBase};User Id={sqlUserId};Password={sqlPassword};"))
            {
                string consulta = "SELECT COUNT(*) FROM Usuarios WHERE UserId = @userid AND Password = @password"; // Reemplaza con tu tabla real

                using (var comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@userid", sqlUserId);
                    comando.Parameters.AddWithValue("@password", sqlPassword); // Asegúrate de que estás validando correctamente

                    try
                    {
                        conexion.Open();
                        int count = (int)comando.ExecuteScalar();
                        return count > 0; // Retorna verdadero si el usuario existe
                    }
                    catch (Exception ex)
                    {
                        Respuesta += ex.Message;
                        return false; // Fallo en la consulta
                    }
                }
            }
        }
    }
}
