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
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public string MetodoPago { get; set; }
        public int IdCliente { get; set; }

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

        #region MetodoInsertar
        public string Insertar(DVenta venta)
        {
            string respuesta = "";

            try
            {
                // Validación de datos de entrada
                if (venta.Importe <= 0 ||
                    venta.Iva < 0 ||
                    venta.Total <= 0 ||
                    string.IsNullOrWhiteSpace(venta.MetodoPago) ||
                    venta.IdCliente <= 0)
                {
                    return "Todos los campos son obligatorios y deben ser válidos.";
                }

                using (var conexionSql = Utilidades.Conexion())
                {
                    if (conexionSql == null)
                    {
                        throw new InvalidOperationException("No se pudo establecer la conexión a la base de datos.");
                    }

                    // Asegúrate de que el orden de los parámetros aquí coincide con el orden de las columnas en la tabla.
                    string consultaSql = "INSERT INTO Venta (Fecha, Importe, Iva, Total, Método_Pago, ID_Cliente) VALUES (@fecha, @importe, @iva, @total, @metodoPago, @idCliente)";

                    using (var comandoSql = new SqlCommand(consultaSql, conexionSql))
                    {
                        // Asignación de parámetros en el mismo orden que en la consulta SQL
                        comandoSql.Parameters.AddWithValue("@fecha", venta.Fecha);
                        comandoSql.Parameters.AddWithValue("@importe", venta.Importe);
                        comandoSql.Parameters.AddWithValue("@iva", venta.Iva);
                        comandoSql.Parameters.AddWithValue("@total", venta.Total);
                        comandoSql.Parameters.AddWithValue("@metodoPago", venta.MetodoPago);
                        comandoSql.Parameters.AddWithValue("@idCliente", venta.IdCliente);

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

    }
}

