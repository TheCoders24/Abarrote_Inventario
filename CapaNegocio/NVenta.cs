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

        #region InsertarVenta
        public static string InsertarVenta(DateTime fecha, decimal importe, decimal iva, decimal total, string metodoPago, int idCliente)
        {
            DVenta venta = new DVenta(0, fecha, importe, iva, total, metodoPago, idCliente);
            return venta.Insertar(venta);
        }
        #endregion
    }
}
