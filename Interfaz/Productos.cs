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
        public void MostrarDatos()
        {
            try
            {
                var proveedores = NProductos.MostrarProductos();
                if (proveedores != null)
                {
                    dataListado.DataSource = NProductos.MostrarProductos();
                }
                else
                {
                    MessageBox.Show("No Se Encontraron datos de productos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sucedio un Error al Querer Mostrar los Datos" + ex);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int idproducto;

            // Primero intentamos hacer el TryParse
            if (int.TryParse(txtIdProducto.Text, out idproducto))
            {
                try
                {
                    // Llamamos a la función para insertar el cliente si el ID es válido
                    string resultado = NProveedores.InsertarProveedor(idproducto, txtNombre.Text, txtPrecio.Text, txtDescripcion.Text);

                    // Opcional: mostrar un mensaje si la inserción fue exitosa
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
                MessageBox.Show("El ID del Proveedor no es válido");
            }
        }
    }
}
