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
        public static Utilidades utilidades = new Utilidades(utilidades.SqlUserId,utilidades.SqlPassword);
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

        // Método Insertar con consulta SQL normal
        #region MetodoInsertar
        public string Insertar(DCliente Cliente)
        {
            string respuesta = "";
            var conexionSql = utilidades.Conexion();

            try
            {
                string consultaSql = "INSERT INTO Cliente (Nombre, Telefono, Dirección) VALUES (@nombre, @telefono, @direccion)";

                var comandoSql = new SqlCommand(consultaSql, conexionSql);

                comandoSql.Parameters.AddWithValue("@nombre", Cliente.Nombre);
                comandoSql.Parameters.AddWithValue("@telefono", Cliente.Telefono);
                comandoSql.Parameters.AddWithValue("@direccion", Cliente.Direccion);

                respuesta = comandoSql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo insertar el registro";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }

            return respuesta;
        }
        #endregion

        #region MetodoEditar
        public string Editar(DCliente Cliente)
        {
            string respuesta = "";
            var conexionSql = utilidades.Conexion();

            try
            {
                string consultaSql = "UPDATE Cliente SET Nombre = @nombre, Telefono = @telefono, Dirección = @direccion WHERE ID_Cliente = @idcliente";

                var comandoSql = new SqlCommand(consultaSql, conexionSql);

                comandoSql.Parameters.AddWithValue("@idcliente", Cliente.IdCliente);
                comandoSql.Parameters.AddWithValue("@nombre", Cliente.Nombre);
                comandoSql.Parameters.AddWithValue("@telefono", Cliente.Telefono);
                comandoSql.Parameters.AddWithValue("@direccion", Cliente.Direccion);

                respuesta = comandoSql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo editar el registro";
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
            var conexionSql = utilidades.Conexion();

            try
            {
                string consultaSql = "DELETE FROM Cliente WHERE ID_Cliente = @idcliente";

                var comandoSql = new SqlCommand(consultaSql, conexionSql);
                comandoSql.Parameters.AddWithValue("@idcliente", Cliente.IdCliente);

                respuesta = comandoSql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo eliminar el registro";
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
            var conexionSql = utilidades.Conexion();

            try
            {
                string consultaSql = "SELECT ID_Cliente, Nombre, Telefono, Dirección FROM Cliente";

                SqlCommand comandoSql = new SqlCommand(consultaSql, conexionSql);
                SqlDataAdapter sqlDat = new SqlDataAdapter(comandoSql);
                sqlDat.Fill(resultadoTabla);
            }
            catch (Exception)
            {
                resultadoTabla = null;
            }

            return resultadoTabla;
        }
        #endregion

        #region MetodoBuscarNombre
        public DataTable BuscarNombre(DCliente Cliente)
        {
            var resultadoTabla = new DataTable("cliente");
            var conexionSql = utilidades.Conexion();

            try
            {
                string consultaSql = "SELECT ID_Cliente, Nombre, Telefono, Dirección FROM Cliente WHERE Nombre LIKE @textobuscar + '%'";

                var comandoSql = new SqlCommand(consultaSql, conexionSql);
                comandoSql.Parameters.AddWithValue("@textobuscar", Cliente.Nombre);

                SqlDataAdapter sqlDat = new SqlDataAdapter(comandoSql);
                sqlDat.Fill(resultadoTabla);
            }
            catch (Exception)
            {
                resultadoTabla = null;
            }

            return resultadoTabla;
        }
        #endregion

    }
}
