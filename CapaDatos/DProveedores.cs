using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DProveedores
    {
        public static Utilidades utilidades = new Utilidades(utilidades.SqlUserId, utilidades.SqlPassword);
        public int IDProveedor { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public DProveedores() { }

        public DProveedores(int idproveedor, string nombre, string telefono, string direccion)
        {
            IDProveedor = idproveedor;
            Nombre = nombre;
            Telefono = telefono;
            Direccion = direccion;
        }


        public string Insertar(DProveedores proveedor)
        {
            string respuesta = "";
            var conexionSql = utilidades.Conexion();

            try
            {
                string consultaSql = "INSERT INTO Proveedores (Nombre, Telefono, Direccion) VALUES (@nombre, @telefono, @direccion)";
                var comandoSql = new SqlCommand(consultaSql, conexionSql);
                comandoSql.Parameters.Add("@nombre", SqlDbType.VarChar).Value = proveedor.Nombre;
                comandoSql.Parameters.Add("@telefono", SqlDbType.VarChar).Value = proveedor.Telefono;
                comandoSql.Parameters.Add("@direccion", SqlDbType.VarChar).Value = proveedor.Direccion;

                respuesta = comandoSql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo insertar el registro";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                conexionSql.Close(); // Asegúrate de cerrar la conexión
            }
            return respuesta;
        }

        public string Editar(DProveedores proveedor)
        {
            string respuesta = "";
            var conexionSql = utilidades.Conexion();

            try
            {
                string consultaSql = "UPDATE Proveedores SET Nombre = @nombre, Telefono = @telefono, Direccion = @direccion WHERE IDProveedor = @idproveedor";
                var comandoSql = new SqlCommand(consultaSql, conexionSql);
                comandoSql.Parameters.Add("@idproveedor", SqlDbType.Int).Value = proveedor.IDProveedor;
                comandoSql.Parameters.Add("@nombre", SqlDbType.VarChar).Value = proveedor.Nombre;
                comandoSql.Parameters.Add("@telefono", SqlDbType.VarChar).Value = proveedor.Telefono;
                comandoSql.Parameters.Add("@direccion", SqlDbType.VarChar).Value = proveedor.Direccion;

                respuesta = comandoSql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo editar el registro";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                conexionSql.Close(); // Asegúrate de cerrar la conexión
            }
            return respuesta;
        }

        public string Eliminar(DProveedores proveedor)
        {
            string respuesta = "";
            var conexionSql = utilidades.Conexion();

            try
            {
                string consultaSql = "DELETE FROM Proveedores WHERE IDProveedor = @idproveedor";
                var comandoSql = new SqlCommand(consultaSql, conexionSql);
                comandoSql.Parameters.AddWithValue("@idproveedor", proveedor.IDProveedor);

                respuesta = comandoSql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo eliminar el registro";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                conexionSql.Close(); // Asegúrate de cerrar la conexión
            }
            return respuesta;
        }

        public DataTable Mostrar()
        {
            var resultadoTabla = new DataTable("proveedores");
            var conexionSql = utilidades.Conexion();

            try
            {
                string consultaSql = "SELECT IDProveedor, Nombre, Telefono, Direccion FROM Proveedores";
                var comandoSql = new SqlCommand(consultaSql, conexionSql);
                var sqlDat = new SqlDataAdapter(comandoSql);
                sqlDat.Fill(resultadoTabla);
            }
            catch (Exception ex)
            {
                resultadoTabla = null; // Consider logging the error
            }
            finally
            {
                conexionSql.Close(); // Asegúrate de cerrar la conexión
            }
            return resultadoTabla;
        }

        public DataTable BuscarNombre(string nombre)
        {
            var resultadoTabla = new DataTable("proveedores");
            var conexionSql = utilidades.Conexion();

            try
            {
                string consultaSql = "SELECT IDProveedor, Nombre, Telefono, Direccion FROM Proveedores WHERE Nombre LIKE @textobuscar + '%'";
                var comandoSql = new SqlCommand(consultaSql, conexionSql);
                comandoSql.Parameters.Add("@textobuscar", SqlDbType.VarChar).Value = nombre;

                var sqlDat = new SqlDataAdapter(comandoSql);
                sqlDat.Fill(resultadoTabla);
            }
            catch (Exception ex)
            {
                resultadoTabla = null; // Consider logging the error
            }
            finally
            {
                conexionSql.Close(); // Asegúrate de cerrar la conexión
            }
            return resultadoTabla;
        }
    }
}