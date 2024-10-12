using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NVenta
    {
        public NVenta() 
        {
            
        }
        // Método para insertar una venta
        public static string InsertarVenta(int idCliente, DateTime fecha, decimal importe, decimal iva, decimal total, string metodoPago)
        {
            // Llama al método de la clase DVenta para insertar la venta
            return DVenta.InsertarVenta(idCliente, fecha, importe, iva, total, metodoPago);
        }
    }
}
