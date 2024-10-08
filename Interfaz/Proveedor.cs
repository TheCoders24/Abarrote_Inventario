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
    public partial class Proveedor : Form
    {
        public Proveedor()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Proveedor_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int IdProveedor = int.Parse(txtIDProveedores.Text);
            string Nombre = txtNombre.Text;
            int Telefono = int.Parse(txtTelefono.Text); // Assuming txtTelefono is a TextBox for phone number
            string Direccion = txtDireccion.Text;

            string ResultadoInsertar = CapaNegocio.NProveedores.InsertarProveedor(Nombre, Telefono, Direccion);

        }
    }
}
