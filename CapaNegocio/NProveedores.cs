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
        public static async Task<string> InsertarProveedorAsync(int idProveedor, string nombre, string direccion, string telefono)
        {
            DProveedores proveedor = new DProveedores()
            {
                IDProveedor = idProveedor,
                Nombre = nombre,
                Telefono = telefono,
                Direccion = direccion
            };

            return await proveedor.InsertarAsync(proveedor);
        }
        #endregion

        #region EditarProveedor
        public static async Task<string> EditarProveedorAsync(int idProveedor, string nombre, string telefono, string direccion)
        {
            DProveedores proveedor = new DProveedores()
            {
                IDProveedor = idProveedor,
                Nombre = nombre,
                Telefono = telefono,
                Direccion = direccion
            };

            return await proveedor.EditarAsync(proveedor);
        }
        #endregion

        #region EliminarProveedor
        public static async Task<string> EliminarProveedorAsync(int idProveedor)
        {
            DProveedores proveedor = new DProveedores()
            {
                IDProveedor = idProveedor
            };

            return await proveedor.EliminarAsync(proveedor);
        }
        #endregion

        #region MostrarProveedores
        public static async Task<DataTable> MostrarProveedoresAsync()
        {
            DProveedores repo = new DProveedores();
            return await repo.MostrarAsync();
        }
        #endregion

        #region BuscarNombreProveedor
        public static async Task<DataTable> BuscarNombreProveedorAsync(string textoBuscar)
        {
            DProveedores repo = new DProveedores();
            return await repo.BuscarNombreAsync(textoBuscar);
        }
        #endregion


    }
}
