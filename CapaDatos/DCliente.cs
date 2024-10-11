using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DCliente
    {
        Utilidades utilidades = new Utilidades();
        public DCliente() 
        {
          
        }
        public DCliente(int idCliente, string nombre, string telefono, string direccion,string textoBuscar)
        {
           
            IdCliente = idCliente;
            Nombre = nombre;
            Telefono = telefono;
            Direccion = direccion;
            TextoBuscar = textoBuscar;

        }

        #region Propiedades
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string TextoBuscar { get; set; }
        #endregion

        #region MetodoInsertar
        public string Insertar(DCliente Cliente)
        {
            string respuesta = "";

            try
            {
                // Validación de datos de entrada
                if (string.IsNullOrWhiteSpace(Cliente.Nombre) ||
                    string.IsNullOrWhiteSpace(Cliente.Telefono) ||
                    string.IsNullOrWhiteSpace(Cliente.Direccion))
                {
                    return "Todos los campos son obligatorios.";
                }

                using (var conexionSql = Utilidades.Conexion())
                {
                    if (conexionSql == null)
                    {
                        throw new InvalidOperationException("No se pudo establecer la conexión a la base de datos.");
                    }

                    // Asegúrate de que el orden de los parámetros aquí coincide con el orden de las columnas en la tabla.
                    string consultaSql = "INSERT INTO Cliente (Nombre, Telefono, Dirección) VALUES (@nombre, @telefono, @direccion)";

                    using (var comandoSql = new SqlCommand(consultaSql, conexionSql))
                    {
                        // Asignación de parámetros en el mismo orden que en la consulta SQL
                        comandoSql.Parameters.AddWithValue("@nombre", Cliente.Nombre);
                        comandoSql.Parameters.AddWithValue("@telefono", Cliente.Telefono);
                        comandoSql.Parameters.AddWithValue("@direccion", Cliente.Direccion);

                        // Ejecutar la consulta y almacenar el resultado
                        respuesta = comandoSql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo insertar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "Ocurrió un error: " + ex.Message; // Manejo de excepciones
            }

            return respuesta;

        }
        #endregion

        #region MetodoEditar
        public string Editar(DCliente Cliente)
        {
            string respuesta = "";

            try
            {
                using (var conexionSql = Utilidades.Conexion())
                {
                    if (conexionSql == null)
                    {
                        throw new InvalidOperationException("No se pudo establecer la conexión a la base de datos.");
                    }

                    string consultaSql = "UPDATE Cliente SET Nombre = @nombre, Telefono = @telefono, Dirección = @direccion WHERE ID_Cliente = @idcliente";

                    using (var comandoSql = new SqlCommand(consultaSql, conexionSql))
                    {
                        comandoSql.Parameters.AddWithValue("@idcliente", Cliente.IdCliente);
                        comandoSql.Parameters.AddWithValue("@nombre", Cliente.Nombre);
                        comandoSql.Parameters.AddWithValue("@telefono", Cliente.Telefono);
                        comandoSql.Parameters.AddWithValue("@direccion", Cliente.Direccion);

                        respuesta = comandoSql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo editar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }

            return respuesta;
        }
        #endregion

        #region MetodoEliminar
        public string Eliminar(DCliente Cliente)
        {
            string respuesta = "";

            try
            {
                using (var conexionSql = Utilidades.Conexion())
                {
                    if (conexionSql == null)
                    {
                        throw new InvalidOperationException("No se pudo establecer la conexión a la base de datos.");
                    }

                    string consultaSql = "DELETE FROM Cliente WHERE ID_Cliente = @idcliente";

                    using (var comandoSql = new SqlCommand(consultaSql, conexionSql))
                    {
                        comandoSql.Parameters.AddWithValue("@idcliente", Cliente.IdCliente);
                        respuesta = comandoSql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo eliminar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }

            return respuesta;
        }
        #endregion

        #region MetodoMostrar
        public DataTable Mostrar()
        {
            var resultadoTabla = new DataTable("cliente");

            try
            {
                using (var conexionSql = Utilidades.Conexion())
                {
                    if (conexionSql == null)
                    {
                        throw new InvalidOperationException("No se pudo establecer la conexión a la base de datos.");
                    }

                    string consultaSql = "SELECT ID_Cliente, Nombre, Telefono, Dirección FROM Cliente";

                    using (var comandoSql = new SqlCommand(consultaSql, conexionSql))
                    {
                        using (var sqlDat = new SqlDataAdapter(comandoSql))
                        {
                            sqlDat.Fill(resultadoTabla);
                        }
                    }
                }
            }
            catch (Exception)
            {
                resultadoTabla = null; // Retorna null en caso de error
            }

            return resultadoTabla;
        }
        #endregion

        #region MetodoBuscarNombre
        public DataTable BuscarNombre(DCliente Cliente)
        {
            var resultadoTabla = new DataTable("cliente");

            try
            {
                using (var conexionSql = Utilidades.Conexion())
                {
                    if (conexionSql == null)
                    {
                        throw new InvalidOperationException("No se pudo establecer la conexión a la base de datos.");
                    }

                    string consultaSql = "SELECT ID_Cliente, Nombre, Telefono, Dirección FROM Cliente WHERE Nombre LIKE @textobuscar + '%'";

                    using (var comandoSql = new SqlCommand(consultaSql, conexionSql))
                    {
                        comandoSql.Parameters.AddWithValue("@textobuscar", Cliente.Nombre);

                        using (var sqlDat = new SqlDataAdapter(comandoSql))
                        {
                            sqlDat.Fill(resultadoTabla);
                        }
                    }
                }
            }
            catch (Exception)
            {
                resultadoTabla = null; // Retorna null en caso de error
            }

            return resultadoTabla;
        }
        #endregion


    }
}
