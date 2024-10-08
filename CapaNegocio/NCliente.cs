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
        public static string InsertarCliente(string nombre, string telefono, string direccion)
        {
            DCliente cliente = new DCliente()
            {
                Nombre = nombre,
                Telefono = telefono,
                Direccion = direccion
            };
            return cliente.Insertar(cliente);
        }
        #endregion

        #region EditarCliente
        public static string EditarCliente(int idCliente, string nombre, string telefono, string direccion)
        {
            DCliente cliente = new DCliente()
            {
                IdCliente = idCliente,
                Nombre = nombre,
                Telefono = telefono,
                Direccion = direccion
            };
            return cliente.Editar(cliente);
        }
        #endregion

        #region EliminarCliente
        public static string Eliminar(int idCliente)
        {
            DCliente cliente = new DCliente();
            cliente.IdCliente = idCliente;
            return cliente.Eliminar(cliente);
        }
        #endregion


        #region MostrarCliente
        public static DataTable Mostrar()
        {
            return new DCliente().Mostrar();
        }
        #endregion

        #region BuscarNombreCliente
        public static DataTable BuscarNombre(string textobuscar)
        {
            DCliente cliente = new DCliente()
            {
                TextoBuscar = textobuscar
            };
            return cliente.BuscarNombre(cliente);
        }
        #endregion

    }
}
