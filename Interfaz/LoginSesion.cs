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
using CapaDatos;

namespace Interfaz
{
    public partial class LoginSesion : Form
    {
        
        public LoginSesion()
        {
            InitializeComponent();
            // Asignamos valores depues de la validacion
          
        }
        public static string _user {  get; set; }
        public static string _password { get; set; }

        Utilidades Utilidades = new Utilidades();   
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                NLogin nLogin = new NLogin();
                 _user = tbUsuario.Text.Trim();
                 _password = tbContrasena.Text.Trim();
               
                
                if (string.IsNullOrEmpty(_user) || string.IsNullOrEmpty(_password))
                {
                    MessageBox.Show("Usuario y contraseña esta vacios compruebe las credenciales");
                }


                Utilidades.SqlUserId = _user;
                Utilidades.SqlPassword = _password;

                //MessageBox.Show(Utilidades.SqlUserId);
                //MessageBox.Show(Utilidades.SqlPassword);


                bool loginExitoso = nLogin.IniciarSesion(_user, _password);

                if (loginExitoso)
                {
                    MessageBox.Show("Login Exitoso");
                    MenuPrincipal frm = new MenuPrincipal();
                    frm.Show();
                    this.Hide();
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

        private void LoginSesion_Load(object sender, EventArgs e)
        {

        }
    }
}
