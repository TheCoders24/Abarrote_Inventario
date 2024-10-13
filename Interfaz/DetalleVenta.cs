﻿using CapaDatos;
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
            Fecha();
        }

        private void Fecha()
        {
            var Fecha = DateTime.Now;
            txtFecha.Text = Fecha.ToShortDateString();
        }

        private async void CargarProductosDesdeBD()
        {
            SqlConnection connection = null; // Declarar la conexión fuera del bloque using
            try
            {
                connection = await Utilidades.ObtenerConexionAsync(); // Inicializar la conexión
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

        private async void CargarClientesDB()
        {
            SqlConnection connection = null; // Declarar la conexión fuera del bloque using
            try
            {
                connection = await Utilidades.ObtenerConexionAsync(); // Inicializar la conexión
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



        // Función para obtener el ID del cliente por nombre
        public async Task<int?> ObtenerIdClientePorNombre(string nombreCliente)
        {
            string query = "SELECT ID_Cliente FROM Cliente WHERE Nombre = @NombreCliente";

            using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
            {
                if (connection == null)
                {
                    throw new Exception("Error al establecer conexión con la base de datos.");
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreCliente", nombreCliente);

                    object resultado = await command.ExecuteScalarAsync();
                    return resultado != null ? (int?)Convert.ToInt32(resultado) : null; // Retorna el ID o null si no se encuentra
                }
            }
        }

        // Función para obtener el ID del producto por nombre
        public async Task<int?> ObtenerIdProductoPorNombre(string nombreProducto)
        {
            string query = "SELECT ID_Producto FROM Producto WHERE Nombre = @NombreProducto";

            using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
            {
                if (connection == null)
                {
                    throw new Exception("Error al establecer conexión con la base de datos.");
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreProducto", nombreProducto);

                    object resultado = await command.ExecuteScalarAsync();
                    return resultado != null ? (int?)Convert.ToInt32(resultado) : null; // Retorna el ID o null si no se encuentra
                }
            }
        }

        private async Task<int> ObtenerSiguienteIdVentaAsync()
        {
            string queryObtenerUltimoId = "SELECT ISNULL(MAX(ID_Venta), 0) + 1 FROM DetalleVenta;";

            using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
            {
                if (connection == null)
                {
                    throw new InvalidOperationException("Error al establecer conexión con la base de datos.");
                }

                using (SqlCommand command = new SqlCommand(queryObtenerUltimoId, connection))
                {
                    return (int)await command.ExecuteScalarAsync();
                }
            }
        }

        private async Task<int> InsertarVentaAsync(int clienteId)
        {
            string query = @"
            INSERT INTO DetalleVenta (ID_Cliente, Fecha_Venta) 
            OUTPUT INSERTED.ID_Venta 
            VALUES (@ID_Cliente, GETDATE());"; // Asumiendo que la fecha de venta es la fecha actual

            using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Cliente", clienteId);
                    return (int)await command.ExecuteScalarAsync(); // Devuelve el ID de la venta insertada
                }
            }
        }


        public async Task CargarDetallesVenta(string nombreProducto, string nombreCliente, int cantidad, decimal preciounitario)
        {

            // Validaciones de los datos
            if (string.IsNullOrWhiteSpace(nombreProducto))
            {
                MessageBox.Show("El nombre del producto no puede estar vacío.");
                return;
            }

            if (string.IsNullOrWhiteSpace(nombreCliente))
            {
                MessageBox.Show("El nombre del cliente no puede estar vacío.");
                return;
            }

            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor que cero.");
                return;
            }

            if (preciounitario <= 0)
            {
                MessageBox.Show("El precio unitario debe ser mayor que cero.");
                return;
            }

            // Cálculo del subtotal
            decimal subtotal = cantidad * preciounitario;
            txtsubtotal.Text = subtotal.ToString("F2"); // Formato a dos decimales

            // Consulta para insertar en DetalleVenta
            string queryInsertDetalle = @"
            INSERT INTO DetalleVenta (ID_Producto, ID_Venta, Cantidad, Precio_Unitario, Subtotal)
            VALUES (@ID_Producto, @ID_Venta, @Cantidad, @PrecioUnitario, @Subtotal);";

            // Consulta para obtener IDs
            string queryObtenerIDs = @"
            DECLARE @ClienteID INT, @ProductoID INT;

            SELECT @ClienteID = ID_Cliente FROM Cliente WHERE Nombre = @NombreCliente;
            SELECT @ProductoID = ID_Producto FROM Producto WHERE Nombre = @NombreProducto;

            IF @ClienteID IS NULL
            BEGIN
                RAISERROR('El cliente no existe en la base de datos.', 16, 1);
            END

            IF @ProductoID IS NULL
            BEGIN
                RAISERROR('El producto no existe en la base de datos.', 16, 1);
            END

            SELECT @ProductoID AS ID_Producto;"; // Eliminamos la selección de ID_Venta aquí

            try
            {
                using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
                {
                    if (connection == null)
                    {
                        MessageBox.Show("Error al establecer conexión con la base de datos.");
                        return;
                    }

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Obtener ID del cliente
                            int? clienteId = await ObtenerIdClientePorNombre(nombreCliente);
                            if (clienteId == null)
                            {
                                MessageBox.Show("El cliente no se encontró en la base de datos.");
                                return;
                            }

                            // Obtener ID de producto
                            int idProducto;
                            using (SqlCommand commandObtenerIDs = new SqlCommand(queryObtenerIDs, connection, transaction))
                            {
                                commandObtenerIDs.Parameters.AddWithValue("@NombreCliente", nombreCliente);
                                commandObtenerIDs.Parameters.AddWithValue("@NombreProducto", nombreProducto);

                                using (SqlDataReader reader = await commandObtenerIDs.ExecuteReaderAsync())
                                {
                                    if (reader.Read())
                                    {
                                        idProducto = reader.GetInt32(reader.GetOrdinal("ID_Producto"));
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encontraron resultados para el cliente o el producto.");
                                        return;
                                    }
                                }
                            }

                            // Obtener ID de venta, si no hay, se deja como null
                            int? idVenta = await ObtenerSiguienteIdVentaAsync(); // Debes implementar este método

                            // Si no hay venta activa, crear una nueva venta
                            if (idVenta == null)
                            {
                                idVenta = await InsertarVentaAsync(clienteId.Value); // Asegúrate de que clienteId no sea null aquí
                            }

                            // Inserción de los detalles de la venta
                            using (SqlCommand commandInsert = new SqlCommand(queryInsertDetalle, connection, transaction))
                            {
                                commandInsert.Parameters.AddWithValue("@ID_Venta", idVenta.Value);
                                commandInsert.Parameters.AddWithValue("@ID_Producto", idProducto);
                                commandInsert.Parameters.AddWithValue("@Cantidad", cantidad);
                                commandInsert.Parameters.AddWithValue("@PrecioUnitario", preciounitario);
                                commandInsert.Parameters.AddWithValue("@Subtotal", subtotal);

                                int filasAfectadas = await commandInsert.ExecuteNonQueryAsync();
                                MessageBox.Show($"Filas afectadas por la inserción: {filasAfectadas}");

                                if (filasAfectadas == 0)
                                {
                                    MessageBox.Show("No se pudo insertar el detalle de la venta. Verifica si el producto y el cliente existen.");
                                    transaction.Rollback();
                                    return;
                                }
                            }

                            // Si llegamos aquí, se realizó todo correctamente
                            transaction.Commit();
                            MessageBox.Show("Detalle de venta insertado correctamente.");
                        }
                        catch (SqlException sqlEx)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error de base de datos: " + sqlEx.Message);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ocurrió un error: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general: " + ex.Message);
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
            CargarDetallesVenta(nombreProducto,nombreCliente,cantidad,precioUnitario);
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
