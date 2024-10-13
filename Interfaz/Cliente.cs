using CapaDatos;
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

        public async void MostrarDatos()
        {
            try
            {
                    dataListado.DataSource = await NCliente.MostrarClientesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sucedio un Error al Querer Mostrar los Datos" + ex);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            int idCliente;

            // Primero intentamos hacer el TryParse
            if (int.TryParse(txtIdCliente.Text, out idCliente))
            {
                try
                {
                    NLogger.LogInfo("iniciando operaciones en la base de datos");
                    // Llamamos a la función para insertar el cliente si el ID es válido
                    string resultado = await NCliente.InsertarClienteAsync(idCliente, txtNombre.Text, txtDireccion.Text, mtxtTelefono.Text);

                    // Opcional: mostrar un mensaje si la inserción fue exitosa
                    MessageBox.Show(resultado == "Ok" ? "Cliente insertado correctamente" : "No se pudo insertar el cliente");
                    NLogger.LogInfo("Operación en la base de datos completada con éxito.");
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

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            int idCliente; // Suponiendo que ya tienes el ID del cliente a actualizar

            if (int.TryParse(txtIdCliente.Text, out idCliente))
            {
                try
                {
                    NLogger.LogInfo("iniciando operaciones en la base de datos");
                    string resultado = await NCliente.EditarClienteAsync(idCliente, txtNombre.Text, txtDireccion.Text, mtxtTelefono.Text);
                    MessageBox.Show(resultado == "Ok" ? "Cliente actualizado correctamente" : "No se pudo actualizar el cliente");
                    NLogger.LogInfo("Operación en la base de datos completada con éxito.");
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

        private async void btnEliminar_Click(object sender, EventArgs e)
        {

            // Obtiene el nombre del cliente desde el TextBox
            string nombre = txtNombre.Text;

            // Llama al método EliminarClienteAsync de forma asíncrona
            string resultado = await NCliente.EliminarClienteAsync(nombre);

            // Muestra el resultado en un MessageBox
            MessageBox.Show(resultado);

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
