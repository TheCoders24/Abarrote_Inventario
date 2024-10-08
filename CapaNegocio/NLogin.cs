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
        public NLogin() 
        {
            _login = new DLogin(Utilidades.SqlServer,Utilidades.SqlDataBase);            
        }

        public bool IniciarSesion(string userid, string password)
        {
            return _login.ValidarUsuarioSqlServer(userid, password);
        }
    }
}
