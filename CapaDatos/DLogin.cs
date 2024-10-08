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

        public bool ValidarUsuarioSqlServer(string sqlUserId,string sqlPassword)
        {
            string connectionString = $"Server={_SqlServidor};Database={_SqlDataBase};User Id={sqlUserId};Password={sqlPassword};";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {

                    sqlConnection.Open();

                    return true;

                }
                catch (Exception ex) 
                {
                    Respuesta += ex.Message;
                    return false;
                }
            }
        }
    }
}
