using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz
{
    public partial class Cliente : Form
    {
        public Cliente()
        {
            InitializeComponent();
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Cliente_Load(object sender, EventArgs e)
        {
           MostrarDatos();
        }

        public void MostrarDatos()
        {
            try
            {
                    dataListado.DataSource = NCliente.Mostrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sucedio un Error al Querer Mostrar los Datos" + ex);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int idCliente;

            // Primero intentamos hacer el TryParse
            if (int.TryParse(txtIdCliente.Text, out idCliente))
            {
                try
                {
                    // Llamamos a la función para insertar el cliente si el ID es válido
                    string resultado = NCliente.InsertarCliente(idCliente, txtNombre.Text, txtDireccion.Text, mtxtTelefono.Text);

                    // Opcional: mostrar un mensaje si la inserción fue exitosa
                    MessageBox.Show(resultado == "Ok" ? "Cliente insertado correctamente" : "No se pudo insertar el cliente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al insertar el cliente: " + ex.Message);
                }
            }
            else
            {
                // Mostramos un mensaje si el ID no es válido
                MessageBox.Show("El ID del cliente no es válido");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int idCliente; // Suponiendo que ya tienes el ID del cliente a actualizar

            if (int.TryParse(txtIdCliente.Text, out idCliente))
            {
                try
                {
                    string resultado = NCliente.EditarCliente(idCliente, txtNombre.Text, txtDireccion.Text, mtxtTelefono.Text);
                    MessageBox.Show(resultado == "Ok" ? "Cliente actualizado correctamente" : "No se pudo actualizar el cliente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al actualizar el cliente: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("El ID del cliente no es válido");
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int idCliente; // Suponiendo que ya tienes el ID del cliente a eliminar

            if (int.TryParse(txtIdCliente.Text, out idCliente))
            {
                try
                {
                    string resultado = NCliente.Eliminar(idCliente);
                    MessageBox.Show(resultado == "Ok" ? "Cliente eliminado correctamente" : "No se pudo eliminar el cliente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al eliminar el cliente: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("El ID del cliente no es válido");
            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
