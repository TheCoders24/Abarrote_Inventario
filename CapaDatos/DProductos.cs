using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DProductos
    {

        

        public DProductos(int idProducto, string nombre, decimal precio, string descripcion, int idProveedor)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Precio = precio;
            Descripcion = descripcion;
            IdProveedor = idProveedor;
        }

        #region Propiedades
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public int IdProveedor { get; set; }
        #endregion

        #region MetodoInsertar
        public string Insertar(DProductos producto)
        {
            string respuesta = "";
            var conexionSql = Utilidades.Conexion();

            try
            {
                string consultaSql = "INSERT INTO Producto (Nombre, Precio, Descripcion, ID_Proveedor) VALUES (@nombre, @precio, @descripcion, @idProveedor)";
                var comandoSql = new SqlCommand(consultaSql, conexionSql);

                comandoSql.Parameters.AddWithValue("@nombre", producto.Nombre);
                comandoSql.Parameters.AddWithValue("@precio", producto.Precio);
                comandoSql.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                comandoSql.Parameters.AddWithValue("@idProveedor", producto.IdProveedor);

                respuesta = comandoSql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo insertar el registro";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message; // Consider logging the exception here.
            }
            finally
            {
                conexionSql.Close(); // Cerrar la conexión
            }

            return respuesta;
        }
        #endregion

        #region MetodoEditar
        public string Editar(DProductos producto)
        {
            string respuesta = "";
            var conexionSql = Utilidades.Conexion();

            try
            {
                string consultaSql = "UPDATE Producto SET Nombre = @nombre, Precio = @precio, Descripcion = @descripcion, ID_Proveedor = @idProveedor WHERE ID_Producto = @idProducto";
                var comandoSql = new SqlCommand(consultaSql, conexionSql);

                comandoSql.Parameters.AddWithValue("@idProducto", producto.IdProducto);
                comandoSql.Parameters.AddWithValue("@nombre", producto.Nombre);
                comandoSql.Parameters.AddWithValue("@precio", producto.Precio);
                comandoSql.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                comandoSql.Parameters.AddWithValue("@idProveedor", producto.IdProveedor);

                respuesta = comandoSql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo editar el registro";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message; // Consider logging the exception here.
            }
            finally
            {
                conexionSql.Close(); // Cerrar la conexión
            }

            return respuesta;
        }
        #endregion

        #region MetodoEliminar
        public string Eliminar(DProductos producto)
        {
            string respuesta = "";
            var conexionSql = Utilidades.Conexion();

            try
            {
                string consultaSql = "DELETE FROM Producto WHERE ID_Producto = @idProducto";
                var comandoSql = new SqlCommand(consultaSql, conexionSql);

                comandoSql.Parameters.AddWithValue("@idProducto", producto.IdProducto);

                respuesta = comandoSql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo eliminar el registro";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message; // Consider logging the exception here.
            }
            finally
            {
                conexionSql.Close(); // Cerrar la conexión
            }

            return respuesta;
        }
        #endregion

        #region MetodoMostrar
        public DataTable Mostrar()
        {
            var resultadoTabla = new DataTable("producto");
            var conexionSql = Utilidades.Conexion();

            try
            {
                string consultaSql = "SELECT TOP (1000) [ID_Producto], [Nombre], [Precio], [Descripcion], [ID_Proveedor] FROM [AbarroteDB].[dbo].[Producto]";
                var comandoSql = new SqlCommand(consultaSql, conexionSql);

                SqlDataAdapter sqlDat = new SqlDataAdapter(comandoSql);
                sqlDat.Fill(resultadoTabla);
            }
            catch (Exception)
            {
                resultadoTabla = null;
            }
            finally
            {
                conexionSql.Close(); // Cerrar la conexión
            }

            return resultadoTabla;
        }
        #endregion

        #region MetodoBuscarNombre
        public DataTable BuscarNombre(string nombre)
        {
            var resultadoTabla = new DataTable("producto");
            var conexionSql = Utilidades.Conexion();

            try
            {
                string consultaSql = "SELECT TOP (1000) [ID_Producto], [Nombre], [Precio], [Descripcion], [ID_Proveedor] FROM [AbarroteDB].[dbo].[Producto] WHERE Nombre LIKE @nombre + '%'";
                var comandoSql = new SqlCommand(consultaSql, conexionSql);

                comandoSql.Parameters.AddWithValue("@nombre", nombre);
                SqlDataAdapter sqlDat = new SqlDataAdapter(comandoSql);
                sqlDat.Fill(resultadoTabla);
            }
            catch (Exception)
            {
                resultadoTabla = null;
            }
            finally
            {
                conexionSql.Close(); // Cerrar la conexión
            }

            return resultadoTabla;
        }
        #endregion
    }
}