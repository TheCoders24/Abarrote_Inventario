using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DProveedores
    {
        public DProveedores()
        {

        }
        public DProveedores(int idproveedor, string nombre, string telefono,string direccion)
        {
            IDProveedor = idproveedor;
            Nombre = nombre;
            Telefono = telefono;
            Direccion = direccion;
        }

        public static int IDProveedor {  get; set; }
        public static string Nombre { get; set; }
        public static string Telefono { get; set; }
        public static string Direccion { get; set; }
    }
}
