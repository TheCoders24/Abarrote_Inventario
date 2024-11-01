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
    public partial class Productos : Form
    {
        public Productos()
        {
            InitializeComponent();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            MostrarDatos();
        }
        public async void MostrarDatos()
        {
            try
            {
                var proveedores = await NProductos.MostrarProductosAsync();
                if (proveedores != null) // Verifica que haya datos
                {
                    dataListado.DataSource = proveedores; // Asigna la lista al DataGridView
                }
                else
                {
                    MessageBox.Show("No se encontraron datos de productos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sucedió un error al querer mostrar los datos: " + ex.Message);
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {

            int idproducto;
            int idprovedores;

            // Intentamos hacer el TryParse para el ID del producto y del proveedor
            if (int.TryParse(textBox1.Text, out idprovedores)) // Asegúrate de que `textBox1` es el nombre correcto de tu TextBox para el ID del proveedor
            {
                try
                {
                    // Validamos que los campos de texto no estén vacíos
                    if (string.IsNullOrWhiteSpace(txtNombre.Text))
                    {
                        MessageBox.Show("El nombre del producto no puede estar vacío.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                    {
                        MessageBox.Show("La descripción del producto no puede estar vacía.");
                        return;
                    }

                    // Asegúrate de que txtPrecio.Text puede ser convertido a decimal
                    decimal precio;
                    if (string.IsNullOrWhiteSpace(txtPrecio.Text) || !decimal.TryParse(txtPrecio.Text, out precio))
                    {
                        MessageBox.Show("El precio ingresado no es válido.");
                        return; // Salimos del método si el precio no es válido
                    }

                    // Llamamos a la función para insertar el producto si el ID es válido
                    string resultado = await NProductos.InsertarProductoAsync(txtNombre.Text, precio, txtDescripcion.Text, idprovedores);

                    // Mostrar un mensaje si la inserción fue exitosa
                    MessageBox.Show(resultado == "Ok" ? "Producto insertado correctamente" : "No se pudo insertar el Producto");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al insertar el Producto: " + ex.Message);
                }
            }
            else
            {
                // Mostramos un mensaje si el ID no es válido
                MessageBox.Show("El ID del Producto o del Proveedor no es válido");
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
