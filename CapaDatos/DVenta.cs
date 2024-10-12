using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DVenta
    {
        // Propiedades de la clase
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public string MetodoPago { get; set; }
        public int IdCliente { get; set; }

        // Constructor de la clase
        public DVenta(int idVenta, DateTime fecha, decimal importe, decimal iva, decimal total, string metodoPago, int idCliente)
        {
            IdVenta = idVenta;
            Fecha = fecha;
            Importe = importe;
            Iva = iva;
            Total = total;
            MetodoPago = metodoPago;
            IdCliente = idCliente;
        }

        // Método para insertar una venta
        public static string InsertarVenta(int idCliente, DateTime fecha, decimal importe, decimal iva, decimal total, string metodoPago)
        {
            string respuesta;

            // Validación de datos de entrada
            if (idCliente <= 0 || importe < 0 || iva < 0 || total < 0)
            {
                return "Los valores de entrada no son válidos.";
            }

            try
            {
                using (var conexionSql = Utilidades.Conexion())
                {
                    if (conexionSql == null)
                    {
                        throw new InvalidOperationException("No se pudo establecer la conexión a la base de datos.");
                    }

                    // Consulta SQL para insertar una nueva venta
                    string consultaSql = @"
                INSERT INTO Venta (Fecha, Importe, Iva, Total, Método_Pago, ID_Cliente) 
                VALUES (@fecha, @importe, @iva, @total, @metodoPago, @idCliente)";

                    using (var comandoSql = new SqlCommand(consultaSql, conexionSql))
                    {
                        // Asignación de parámetros
                        comandoSql.Parameters.AddWithValue("@fecha", fecha);
                        comandoSql.Parameters.AddWithValue("@importe", importe);
                        comandoSql.Parameters.AddWithValue("@iva", iva);
                        comandoSql.Parameters.AddWithValue("@total", total);
                        comandoSql.Parameters.AddWithValue("@metodoPago", metodoPago ?? (object)DBNull.Value); // Manejo de null
                        comandoSql.Parameters.AddWithValue("@idCliente", idCliente);

                        // Ejecutar la consulta y almacenar el resultado
                        respuesta = comandoSql.ExecuteNonQuery() == 1 ? "Ok" : "No se pudo insertar el registro";
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                respuesta = "Error de base de datos: " + sqlEx.Message; // Manejo de excepciones de SQL
            }
            catch (Exception ex)
            {
                respuesta = "Ocurrió un error: " + ex.Message; // Manejo de excepciones generales
            }

            return respuesta;
        }
    }
}

