using CapaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz
{
    public partial class DetalleVenta : Form
    {
        public DetalleVenta()
        {
            InitializeComponent();
            CargarProductosDesdeBD();
            CargarClientesDB();
            ConfigurarDataGridView();
        }

        private void CargarProductosDesdeBD()
        {
            SqlConnection connection = null; // Declarar la conexión fuera del bloque using
            try
            {
                connection = Utilidades.Conexion(); // Inicializar la conexión
                                                    // connection.Open(); // No es necesario abrirla explícitamente, se maneja en Utilidades.Conexion()

                string query = "SELECT ID_Producto, Nombre, Precio, Descripción FROM Producto";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable productosTable = new DataTable();
                adapter.Fill(productosTable);

                if (productosTable.Rows.Count > 0)
                {
                    comboboxProducto.DataSource = productosTable;
                    comboboxProducto.DisplayMember = "Nombre";  // Mostrar nombre del producto
                    comboboxProducto.ValueMember = "ID_Producto"; // Guardar ID del producto
                }
                else
                {
                    MessageBox.Show("No se encontraron productos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close(); // Cerrar la conexión explícitamente
                }
            }
        }

        private void CargarClientesDB()
        {
            SqlConnection connection = null; // Declarar la conexión fuera del bloque using
            try
            {
                connection = Utilidades.Conexion(); // Inicializar la conexión
                                                    // connection.Open(); // No es necesario abrirla explícitamente, se maneja en Utilidades.Conexion()

                string query = "SELECT [ID_Cliente], [Nombre] FROM [AbarroteDB].[dbo].[Cliente]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable clientesTable = new DataTable();
                adapter.Fill(clientesTable);

                if (clientesTable.Rows.Count > 0)
                {
                    comboBoxCliente.DataSource = clientesTable; // Asegúrate de tener comboboxCliente definido
                    comboBoxCliente.DisplayMember = "Nombre";  // Mostrar nombre del cliente
                    comboBoxCliente.ValueMember = "ID_Cliente"; // Guardar ID del cliente
                }
                else
                {
                    MessageBox.Show("No se encontraron clientes.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close(); // Cerrar la conexión explícitamente
                }
            }
        }

        private void CargarDetallesVenta(string nombreProducto, string nombreCliente)
        {
            string query = @"
            SELECT 
                dv.ID_Venta AS ID_DetalleVenta,  -- Asumimos que ID_Venta es un identificador para el detalle de la venta
                p.Nombre AS Producto,
                dv.Cantidad,
                dv.Precio_Unitario AS Importe,
                (dv.Cantidad * dv.Precio_Unitario) AS Total
            FROM 
                DetalleVenta dv
            JOIN 
                Producto p ON dv.ID_Producto = p.ID_Producto
            JOIN 
                Cliente c ON dv.ID_Cliente = c.ID_Cliente  -- Asumimos que hay una relación entre DetalleVenta y Cliente
            WHERE 
                p.Nombre = @NombreProducto AND c.Nombre = @NombreCliente;";

            try
            {
                using (SqlConnection connection = Utilidades.Conexion())
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NombreProducto", nombreProducto);
                    command.Parameters.AddWithValue("@NombreCliente", nombreCliente);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable detallesTable = new DataTable();
                    adapter.Fill(detallesTable);

                    // Limpiar DataGridView previo
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();

                    // Asignar el DataTable al DataGridView
                    dataGridView1.DataSource = detallesTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalles de venta: " + ex.Message);
            }
        }

        private void DetalleVenta_Load(object sender, EventArgs e)
        {
           
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (comboboxProducto.SelectedItem == null || comboBoxCliente.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un Producto y un Cliente");
                return;
            }
            //Verificamos que la cantidad sea un numero valido
            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingresa una Cantidad Valida");
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precioUnitario) || precioUnitario <= 0 )
            {
                MessageBox.Show("Ingrese un Precio Valido");
                return;
            }

            decimal importe = cantidad * precioUnitario;

                 dataGridView1.Rows.Add(
                        comboboxProducto.Text, // Producto
                        cantidad,              // Cantidad
                        precioUnitario,       // Importe
                        importe                // Total
                );
            string nombreProducto = comboboxProducto.Text;
            string nombreCliente = comboBoxCliente.Text;
            CargarDetallesVenta(nombreProducto,nombreCliente);
        }


        private void ConfigurarDataGridView()
        {
            dataGridView1.Columns.Add("Producto", "Producto");
            dataGridView1.Columns.Add("Cantidad", "Cantidad");
            dataGridView1.Columns.Add("PrecioUnitario", "Precio Unitario");
            dataGridView1.Columns.Add("Total", "Total");
        }
        // limpiamos los controles del windows forms 
        public void LimpiarControles()
        {
            comboBoxCliente.SelectedIndex = -1;
            comboboxProducto.SelectedIndex = -1;    
            txtCantidad.Clear();
            txtsubtotal.Clear();    
            txtTotal.Clear();
            txtiva.Clear(); 
        }

        private void comboboxProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboboxProducto.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)comboboxProducto.SelectedItem;
                decimal precio = Convert.ToDecimal(selectedRow["Precio"]);
                txtPrecio.Text = precio.ToString("F2"); // Muestra el precio con dos decimales

                // Reinicia el campo de cantidad
                txtCantidad.Text = "1"; // Valor predeterminado
               
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
         
        }
    }
}
