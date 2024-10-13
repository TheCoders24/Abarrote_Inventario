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
            MostrarDatos();
        }


        public void MostrarDatos()
        {
            try
            {
                var proveedores = NProveedores.MostrarProveedoresAsync();
                if (proveedores != null)
                {
                    dataListado.DataSource = NProveedores.MostrarProveedoresAsync();
                    
                }
                else
                {
                    MessageBox.Show("No Se Encontraron datos de proveedores");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sucedio un Error al Querer Mostrar los Datos" + ex);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
           

        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            int idProveedor;

            // Primero intentamos hacer el TryParse
            if (int.TryParse(txtIdProveedor.Text, out idProveedor))
            {
                try
                {
                    // Llamamos a la función para insertar el cliente si el ID es válido
                    string resultado = await NProveedores.InsertarProveedorAsync(idProveedor, txtNombre.Text, txtDireccion.Text, mtxtTelefono.Text);

                    // Opcional: mostrar un mensaje si la inserción fue exitosa
                    MessageBox.Show(resultado == "Ok" ? "Proveedor insertado correctamente" : "No se pudo insertar el Proveedor");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al insertar el Proveedor: " + ex.Message);
                }
            }
            else
            {
                // Mostramos un mensaje si el ID no es válido
                MessageBox.Show("El ID del Proveedor no es válido");
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idProveedor; // Suponiendo que ya tienes el ID del cliente a actualizar

            if (int.TryParse(txtIdProveedor.Text, out idProveedor))
            {
                try
                {
                    string resultado = await NProveedores.EditarProveedorAsync(idProveedor, txtNombre.Text, txtDireccion.Text, mtxtTelefono.Text);
                    MessageBox.Show(resultado == "Ok" ? "Proveedor actualizado correctamente" : "No se pudo actualizar el Proveedor");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al actualizar el Proveedor: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("El ID del Proveedor no es válido");
            }
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            int idProveedor; // Suponiendo que ya tienes el ID del cliente a eliminar

            if (int.TryParse(txtIdProveedor.Text, out idProveedor))
            {
                try
                {
                    string resultado = await NProveedores.EliminarProveedorAsync (idProveedor);
                    MessageBox.Show(resultado == "Ok" ? "Proveedor eliminado correctamente" : "No se pudo eliminar el Proveedor");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al eliminar el Proveedor: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("El ID del Proveedor no es válido");
            }

        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
