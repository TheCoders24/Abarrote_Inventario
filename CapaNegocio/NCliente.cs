using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class NCliente
    {
        #region InsertarCliente
        public static async Task<string> InsertarClienteAsync(int idCliente, string nombre, string direccion, string telefono)
        {
            DCliente cliente = new DCliente()
            {
                IdCliente = idCliente,
                Nombre = nombre,
                Direccion = direccion,
                Telefono = telefono,
            };
            return await cliente.InsertarAsync(cliente);
        }
        #endregion

        #region EditarCliente
        public static async Task<string> EditarClienteAsync(int idCliente, string nombre, string direccion, string telefono)
        {
            DCliente cliente = new DCliente()
            {
                IdCliente = idCliente,
                Nombre = nombre,
                Direccion = direccion,
                Telefono = telefono,
            };
            return await cliente.EditarAsync(cliente);
        }
        #endregion

        #region EliminarCliente
        public static async Task<string> EliminarClienteAsync(string nombre)
        {
            DCliente cliente = new DCliente()
            {
                Nombre = nombre
            };
            return await cliente.EliminarAsync(cliente);
        }
        #endregion

        #region MostrarCliente
        public static async Task<DataTable> MostrarClientesAsync()
        {
            return await new DCliente().MostrarAsync();
        }
        #endregion

        #region BuscarNombreCliente
        public static async Task<DataTable> BuscarNombreClienteAsync(string textobuscar)
        {
            DCliente cliente = new DCliente()
            {
                TextoBuscar = textobuscar
            };
            return await cliente.BuscarNombreAsync(cliente.TextoBuscar);
        }
        #endregion


    }
}
