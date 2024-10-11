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
        public static string InsertarCliente(int idCliente,string nombre, string direccion, string telefono)
        {
            DCliente cliente = new DCliente()
            {
                IdCliente = idCliente,  
                Nombre = nombre,
                Direccion = direccion,
                Telefono = telefono,
                
            };
            return cliente.Insertar(cliente);
        }
        #endregion

        #region EditarCliente
        public static string EditarCliente(int idCliente, string nombre, string direccion, string telefono)
        {
            DCliente cliente = new DCliente()
            {
                IdCliente = idCliente,
                Nombre = nombre,
                Direccion = direccion,
                Telefono = telefono,
                
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
