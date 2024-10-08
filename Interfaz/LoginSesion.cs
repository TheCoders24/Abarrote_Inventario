using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace Interfaz
{
    public partial class LoginSesion : Form
    {
        public LoginSesion()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                NLogin nLogin = new NLogin();
                string _user = tbUsuario.Text;
                string _password = tbContrasena.Text;
                if (string.IsNullOrEmpty(_user) || string.IsNullOrEmpty(_password))
                {
                    MessageBox.Show("Usuario y contraseña esta vacios compruebe las credenciales");
                }

                bool loginExitoso = nLogin.IniciarSesion(_user, _password);

                if (loginExitoso)
                {
                    MessageBox.Show("Login Exitoso");
                }
                else
                {
                    MessageBox.Show("Login Fallido. Revise sus Credenciales");
                }
            }
            catch (Exception ex)
            {
                string Respuesta = "";
                Respuesta += ex.Message;
                MessageBox.Show(Respuesta);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            Hide();
        }
    }
}
