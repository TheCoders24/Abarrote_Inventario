using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NProveedores
    {
        #region InsertarProveedor
        public static string InsertarProveedor(int idproveedor,string nombre, string direccion, string telefono)
        {
            DProveedores proveedor = new DProveedores()
            {
                IDProveedor = idproveedor,  
                Nombre = nombre,
                Telefono = telefono,
                Direccion = direccion
            };
            
            return proveedor.Insertar(proveedor);
        }
        #endregion

        #region EditarProveedor
        public static string EditarProveedor(int idProveedor, string nombre, string telefono, string direccion)
        {
            DProveedores proveedor = new DProveedores()
            {
                IDProveedor = idProveedor,
                Nombre = nombre,
                Telefono = telefono,
                Direccion = direccion
            };
            
            return proveedor.Editar(proveedor);
        }
        #endregion

        #region EliminarProveedor
        public static string EliminarProveedor(int idProveedor)
        {
            DProveedores proveedor = new DProveedores()
            {
                IDProveedor = idProveedor
            };
            
            return proveedor.Eliminar(proveedor);
        }
        #endregion

        #region MostrarProveedores
        public static DataTable MostrarProveedores()
        {
            DProveedores repo = new DProveedores();
            return repo.Mostrar();
        }
        #endregion

        #region BuscarNombreProveedor
        public static DataTable BuscarNombreProveedor(string textoBuscar)
        {
            DProveedores repo = new DProveedores();
            return repo.BuscarNombre(textoBuscar);
        }
        #endregion

    }
}
