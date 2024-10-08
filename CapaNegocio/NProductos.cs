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
        public static string InsertarProducto(string nombre, decimal precio, string descripcion, int idProveedor)
        {
            DProductos producto = new DProductos(0, nombre, precio, descripcion, idProveedor);
            return producto.Insertar(producto);
        }
        #endregion

        #region EditarProducto
        public static string EditarProducto(int idProducto, string nombre, decimal precio, string descripcion, int idProveedor)
        {
            DProductos producto = new DProductos(idProducto, nombre, precio, descripcion, idProveedor);
            return producto.Editar(producto);
        }
        #endregion

        #region EliminarProducto
        public static string EliminarProducto(int idProducto)
        {
            DProductos producto = new DProductos(idProducto, string.Empty, 0, string.Empty, 0);
            return producto.Eliminar(producto);
        }
        #endregion

        #region MostrarProductos
        public static DataTable MostrarProductos()
        {
            DProductos repo = new DProductos(0, string.Empty, 0, string.Empty, 0);
            return repo.Mostrar();
        }
        #endregion

        #region BuscarNombreProducto
        public static DataTable BuscarNombreProducto(string textoBuscar)
        {
            DProductos repo = new DProductos(0, textoBuscar, 0, string.Empty, 0);
            return repo.BuscarNombre(textoBuscar);
        }
        #endregion
    }
}
