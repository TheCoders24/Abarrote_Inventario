using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NProductos
    {

        #region InsertarProducto
        public static async Task<string> InsertarProductoAsync(string nombre, decimal precio, string descripcion, int idProveedor)
        {
            DProductos producto = new DProductos(0, nombre, precio, descripcion, idProveedor);
            return await producto.InsertarAsync(producto);
        }
        #endregion

        #region EditarProducto
        public static async Task<string> EditarProductoAsync(int idProducto, string nombre, decimal precio, string descripcion, int idProveedor)
        {
            DProductos producto = new DProductos(idProducto, nombre, precio, descripcion, idProveedor);
            return await producto.EditarAsync(producto);
        }
        #endregion

        #region EliminarProducto
        public static async Task<string> EliminarProductoAsync(int idProducto)
        {
            DProductos producto = new DProductos(idProducto, string.Empty, 0, string.Empty, 0);
            return await producto.EliminarAsync(producto);
        }
        #endregion

        #region MostrarProductos
        public static async Task<DataTable> MostrarProductosAsync()
        {
            DProductos repo = new DProductos(0, string.Empty, 0, string.Empty, 0);
            return await repo.MostrarAsync();
        }
        #endregion

        #region BuscarNombreProducto
        public static async Task<DataTable> BuscarNombreProductoAsync(string textoBuscar)
        {
            DProductos repo = new DProductos(0, textoBuscar, 0, string.Empty, 0);
            return await repo.BuscarNombreAsync(textoBuscar);
        }
        #endregion

    }
}
