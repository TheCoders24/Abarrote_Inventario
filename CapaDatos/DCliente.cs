using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

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
        public async Task<string> InsertarAsync(DCliente cliente)
        {
            // Validación de datos de entrada
            if (string.IsNullOrWhiteSpace(cliente.Nombre) ||
                string.IsNullOrWhiteSpace(cliente.Telefono) ||
                string.IsNullOrWhiteSpace(cliente.Direccion))
            {
                return "Todos los campos son obligatorios.";
            }

            try
            {
                using (var conexionSql = await Utilidades.ObtenerConexionAsync())
                {
                    if (conexionSql == null)
                    {
                        throw new InvalidOperationException("No se pudo establecer la conexión a la base de datos.");
                    }

                    string consultaSql = "INSERT INTO Cliente (Nombre, Telefono, Dirección) VALUES (@nombre, @telefono, @direccion)";

                    using (var comandoSql = new SqlCommand(consultaSql, conexionSql))
                    {
                        comandoSql.Parameters.AddWithValue("@nombre", cliente.Nombre);
                        comandoSql.Parameters.AddWithValue("@telefono", cliente.Telefono);
                        comandoSql.Parameters.AddWithValue("@direccion", cliente.Direccion);

                        // Ejecutar la consulta y almacenar el resultado
                        var filasAfectadas = await comandoSql.ExecuteNonQueryAsync();
                        return filasAfectadas == 1 ? "Ok" : "No se pudo insertar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Ocurrió un error: " + ex.Message; // Manejo de excepciones
            }
        }
        #endregion

        #region MetodoEditar
        public async Task<string> EditarAsync(DCliente cliente)
        {
            try
            {
                using (var conexionSql = await Utilidades.ObtenerConexionAsync())
                {
                    if (conexionSql == null)
                    {
                        throw new InvalidOperationException("No se pudo establecer la conexión a la base de datos.");
                    }

                    string consultaSql = "UPDATE Cliente SET Nombre = @nombre, Telefono = @telefono, Dirección = @direccion WHERE ID_Cliente = @idcliente";

                    using (var comandoSql = new SqlCommand(consultaSql, conexionSql))
                    {
                        comandoSql.Parameters.AddWithValue("@idcliente", cliente.IdCliente);
                        comandoSql.Parameters.AddWithValue("@nombre", cliente.Nombre);
                        comandoSql.Parameters.AddWithValue("@telefono", cliente.Telefono);
                        comandoSql.Parameters.AddWithValue("@direccion", cliente.Direccion);

                        var filasAfectadas = await comandoSql.ExecuteNonQueryAsync();
                        return filasAfectadas == 1 ? "Ok" : "No se pudo editar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Ocurrió un error: " + ex.Message; // Manejo de excepciones
            }
        }
        #endregion

        #region MetodoEliminar
        public async Task<string> EliminarAsync(DCliente cliente)
        {

            try
            {
                using (var conexionSql = await Utilidades.ObtenerConexionAsync())
                {
                    if (conexionSql == null)
                    {
                        throw new InvalidOperationException("No se pudo establecer la conexión a la base de datos.");
                    }

                    // Primero eliminamos las ventas relacionadas
                    string eliminarVentasSql = "DELETE FROM Venta WHERE ID_Cliente IN (SELECT ID_Cliente FROM Cliente WHERE Nombre = @nombre)";
                    using (var comandoSql = new SqlCommand(eliminarVentasSql, conexionSql))
                    {
                        comandoSql.Parameters.AddWithValue("@nombre", cliente.Nombre);
                        await comandoSql.ExecuteNonQueryAsync();
                    }

                    // Luego eliminamos el cliente
                    string consultaSql = "DELETE FROM Cliente WHERE Nombre = @nombre";
                    using (var comandoSql = new SqlCommand(consultaSql, conexionSql))
                    {
                        comandoSql.Parameters.AddWithValue("@nombre", cliente.Nombre);
                        var filasAfectadas = await comandoSql.ExecuteNonQueryAsync();
                        return filasAfectadas > 0 ? "Cliente y ventas eliminados correctamente" : "No se pudo eliminar el cliente";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Ocurrió un error: " + ex.Message; // Manejo de excepciones
            }
        }
        #endregion

        #region MetodoMostrar
        public async Task<DataTable> MostrarAsync()
        {
            var resultadoTabla = new DataTable("Cliente");

            try
            {
                using (var conexionSql = await Utilidades.ObtenerConexionAsync())
                {
                    if (conexionSql == null || conexionSql.State != ConnectionState.Open)
                    {
                        throw new InvalidOperationException("No se pudo establecer la conexión a la base de datos.");
                    }

                    string consultaSql = "SELECT ID_Cliente, Nombre, Telefono, Dirección FROM Cliente";

                    using (var comandoSql = new SqlCommand(consultaSql, conexionSql))
                    {
                        using (var sqlDat = new SqlDataAdapter(comandoSql))
                        {
                            await Task.Run(() => sqlDat.Fill(resultadoTabla)); // Llena la tabla con los datos obtenidos de la consulta
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultadoTabla = null; // Retorna null si ocurre un error
                                       // Puedes considerar registrar el error en un log aquí.
            }

            return resultadoTabla; // Devuelve la tabla con los resultados
        }
        #endregion

        #region MetodoBuscarNombre
        public async Task<DataTable> BuscarNombreAsync(string nombre)
        {
            var resultadoTabla = new DataTable("Cliente");

            try
            {
                using (var conexionSql = await Utilidades.ObtenerConexionAsync())
                {
                    if (conexionSql == null)
                    {
                        throw new InvalidOperationException("No se pudo establecer la conexión a la base de datos.");
                    }

                    string consultaSql = "SELECT ID_Cliente, Nombre, Telefono, Dirección FROM Cliente WHERE Nombre LIKE @textobuscar + '%'";

                    using (var comandoSql = new SqlCommand(consultaSql, conexionSql))
                    {
                        comandoSql.Parameters.AddWithValue("@textobuscar", nombre);

                        using (var sqlDat = new SqlDataAdapter(comandoSql))
                        {
                            await Task.Run(() => sqlDat.Fill(resultadoTabla)); // Llena la tabla con los datos obtenidos de la consulta
                        }
                    }
                }
            }
            catch (Exception)
            {
                resultadoTabla = null; // Retorna null en caso de error
                                       // Puedes considerar registrar el error en un log aquí.
            }

            return resultadoTabla; // Devuelve la tabla con los resultados
        }
        #endregion


    }
}
