using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class NLogin
    {
        private DLogin _login;

        // Constructor
        public NLogin()
        {
            // Inicializa la instancia de DLogin con los valores de configuración
            _login = new DLogin(Utilidades.SqlServer, Utilidades.SqlDataBase);
        }

        // Método para iniciar sesión
        public bool IniciarSesion(string userid, string password)
        {
            // Aquí podrías agregar lógica adicional, como validar que userid y password no estén vacíos
            if (string.IsNullOrWhiteSpace(userid) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("El usuario y la contraseña no pueden estar vacíos.");
            }

            // Llama al método ValidarUsuarioSqlServer de DLogin
            return _login.ValidarUsuarioSqlServer(userid, password);
        }
    }
}
