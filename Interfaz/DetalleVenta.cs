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

                string query = "SELECT ID_Producto, Nombre, Precio, Descripcion FROM Producto";
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


        //public async Task CargarDetallesVenta(string nombreProducto, string nombreCliente, int cantidad, decimal preciounitario)
        //{
        //    // Validaciones de los datos
        //    if (string.IsNullOrWhiteSpace(nombreProducto))
        //    {
        //        MessageBox.Show("El nombre del producto no puede estar vacío.");
        //        return;
        //    }

        //    if (string.IsNullOrWhiteSpace(nombreCliente))
        //    {
        //        MessageBox.Show("El nombre del cliente no puede estar vacío.");
        //        return;
        //    }

        //    if (cantidad <= 0)
        //    {
        //        MessageBox.Show("La cantidad debe ser mayor que cero.");
        //        return;
        //    }

        //    if (preciounitario <= 0)
        //    {
        //        MessageBox.Show("El precio unitario debe ser mayor que cero.");
        //        return;
        //    }

        //    // Cálculo del subtotal
        //    decimal subtotal = cantidad * preciounitario;
        //    txtsubtotal.Text = subtotal.ToString();
        //    txtTotal.Text = subtotal.ToString();


        //    // Consulta SQL para insertar en Venta
        //    string queryInsertVenta = @"
        //    INSERT INTO Venta (Fecha, Importe, Iva, Total, Método_Pago, ID_Cliente)
        //    OUTPUT INSERTED.ID_Venta
        //    VALUES (@Fecha, @Importe, @Iva, @Total, @MetodoPago, @ID_Cliente);";

        //    // Consulta SQL para insertar en DetalleVenta
        //    string queryInsertDetalle = @"
        //    INSERT INTO DetalleVenta (ID_Producto, ID_Venta, Cantidad, Precio_Unitario, Subtotal)
        //    VALUES (@ID_Producto, @ID_Venta, @Cantidad, @PrecioUnitario, @Subtotal);";

        //    // Consulta SQL para obtener los IDs
        //    string queryObtenerIDs = @"
        //    SELECT ID_Cliente FROM Cliente WHERE Nombre = @NombreCliente;
        //    SELECT ID_Producto FROM Producto WHERE Nombre = @NombreProducto;";

        //    try
        //    {
        //        using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
        //        {
        //            if (connection == null)
        //            {
        //                MessageBox.Show("Error al establecer conexión con la base de datos.");
        //                return;
        //            }

        //            using (SqlTransaction transaction = connection.BeginTransaction())
        //            {
        //                try
        //                {
        //                    // Obtener ID del cliente
        //                    int? clienteId = await ObtenerIdClientePorNombre(nombreCliente);
        //                    if (clienteId == null)
        //                    {
        //                        MessageBox.Show("El cliente no se encontró en la base de datos.");
        //                        return;
        //                    }

        //                    // Obtener ID del producto
        //                    int idProducto;
        //                    using (SqlCommand commandObtenerIDs = new SqlCommand(queryObtenerIDs, connection, transaction))
        //                    {
        //                        commandObtenerIDs.Parameters.AddWithValue("@NombreCliente", nombreCliente);
        //                        commandObtenerIDs.Parameters.AddWithValue("@NombreProducto", nombreProducto);

        //                        using (SqlDataReader reader = await commandObtenerIDs.ExecuteReaderAsync())
        //                        {
        //                            if (reader.Read())
        //                            {
        //                                // Leer el ID del producto
        //                                if (reader.NextResult() && reader.Read())
        //                                {
        //                                    idProducto = reader.GetInt32(0); // Obtener el ID del producto
        //                                }
        //                                else
        //                                {
        //                                    MessageBox.Show("No se encontró el producto.");
        //                                    return;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                MessageBox.Show("No se encontraron resultados para el cliente o el producto.");
        //                                return;
        //                            }
        //                        }
        //                    }

        //                    // Insertar en la tabla Venta
        //                    int idVenta;
        //                    decimal iva = 0.16m;
        //                    using (SqlCommand commandInsertVenta = new SqlCommand(queryInsertVenta, connection, transaction))
        //                    {
        //                        // Parámetros para la tabla Venta
        //                        commandInsertVenta.Parameters.AddWithValue("@Fecha", DateTime.Now);
        //                        commandInsertVenta.Parameters.AddWithValue("@Importe", subtotal);
        //                        commandInsertVenta.Parameters.AddWithValue("@Iva", iva * subtotal); // Asumiendo un IVA del 16%
        //                        commandInsertVenta.Parameters.AddWithValue("@Total", 1.16m * subtotal);
        //                        commandInsertVenta.Parameters.AddWithValue("@MetodoPago", "Efectivo"); // Puedes cambiar el método de pago según la selección
        //                        commandInsertVenta.Parameters.AddWithValue("@ID_Cliente", clienteId.Value);

        //                        // Obtener el ID de la venta recién insertada
        //                        idVenta = (int)await commandInsertVenta.ExecuteScalarAsync();
        //                    }

        //                    // Insertar en la tabla DetalleVenta
        //                    using (SqlCommand commandInsertDetalle = new SqlCommand(queryInsertDetalle, connection, transaction))
        //                    {
        //                        commandInsertDetalle.Parameters.AddWithValue("@ID_Venta", idVenta);
        //                        commandInsertDetalle.Parameters.AddWithValue("@ID_Producto", idProducto);
        //                        commandInsertDetalle.Parameters.AddWithValue("@Cantidad", cantidad);
        //                        commandInsertDetalle.Parameters.AddWithValue("@PrecioUnitario", preciounitario);
        //                        commandInsertDetalle.Parameters.AddWithValue("@Subtotal", subtotal);

        //                        int filasAfectadas = await commandInsertDetalle.ExecuteNonQueryAsync();
        //                        MessageBox.Show($"Filas afectadas por la inserción: {filasAfectadas}");

        //                        if (filasAfectadas == 0)
        //                        {
        //                            MessageBox.Show("No se pudo insertar el detalle de la venta.");
        //                            transaction.Rollback();
        //                            return;
        //                        }
        //                    }

        //                    // Si todo va bien, hacer commit
        //                    transaction.Commit();
        //                    MessageBox.Show("Venta y detalle insertados correctamente.");
        //                }
        //                catch (SqlException sqlEx)
        //                {
        //                    transaction.Rollback();
        //                    MessageBox.Show("Error de base de datos: " + sqlEx.Message);
        //                }
        //                catch (Exception ex)
        //                {
        //                    transaction.Rollback();
        //                    MessageBox.Show("Ocurrió un error: " + ex.Message);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error general: " + ex.Message);
        //    }

        //}
        public async Task CargarDetallesVenta(string nombreProducto, string nombreCliente, int cantidad, decimal precioUnitario)
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

            if (precioUnitario <= 0)
            {
                MessageBox.Show("El precio unitario debe ser mayor que cero.");
                return;
            }

            // Cálculo del subtotal
            decimal subtotal = cantidad * precioUnitario;
            string metodopago = comboBoxmetodopago.Text;
            txtsubtotal.Text = subtotal.ToString();
            txtTotal.Text = subtotal.ToString();

            try
            {
                using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
                {
                    if (connection == null)
                    {
                        MessageBox.Show("Error al establecer conexión con la base de datos.");
                        return;
                    }

                    using (SqlCommand command = new SqlCommand("usp_RegistrarVenta", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros para el procedimiento almacenado
                        command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                        command.Parameters.AddWithValue("@Importe", subtotal);
                        command.Parameters.AddWithValue("@Iva", 0.16m * subtotal); // IVA del 16%
                        command.Parameters.AddWithValue("@Total", 1.16m * subtotal);
                        command.Parameters.AddWithValue("@Metodo_Pago", metodopago); // Cambia según el método de pago
                        command.Parameters.Add("@ID_Cliente", SqlDbType.Int).Value = await ObtenerIdClientePorNombre(nombreCliente) ?? (object)DBNull.Value;
                        command.Parameters.Add("@ID_Producto", SqlDbType.Int).Value = await ObtenerIdProductoPorNombre(nombreProducto) ?? (object)DBNull.Value;
                        command.Parameters.AddWithValue("@Cantidad", cantidad);
                        command.Parameters.AddWithValue("@Precio_Unitario", precioUnitario);
                        // Parámetro de salida
                        SqlParameter resultadoParam = new SqlParameter("@Resultado", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(resultadoParam);

                        // Ejecutar el procedimiento
                        await command.ExecuteNonQueryAsync();
                        int resultado = (int)command.Parameters["@Resultado"].Value;
                        if (resultado == -1)
                        {
                            MessageBox.Show("No hay suficiente inventario para completar la venta.");
                        }
                        else if (resultado == 0)
                        {
                            MessageBox.Show("Ocurrió un error al registrar la venta.");
                        }
                        else
                        {
                            MessageBox.Show($"Venta registrada exitosamente con ID: {resultado}");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Error de base de datos: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        private async void DetalleVenta_Load(object sender, EventArgs e)
        {
           
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            //if (comboboxProducto.SelectedItem == null || comboBoxCliente.SelectedItem == null)
            //{
            //    MessageBox.Show("Seleccione un Producto y un Cliente");
            //    return;
            //}
            ////Verificamos que la cantidad sea un numero valido
            //if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            //{
            //    MessageBox.Show("Ingresa una Cantidad Valida");
            //    return;
            //}

            //if (!decimal.TryParse(txtPrecio.Text, out decimal precioUnitario) || precioUnitario <= 0 )
            //{
            //    MessageBox.Show("Ingrese un Precio Valido");
            //    return;
            //}

            //decimal importe = cantidad * precioUnitario;

            //     dataGridView1.Rows.Add(
            //            comboboxProducto.Text, // Producto
            //            cantidad,              // Cantidad
            //            precioUnitario,       // Importe
            //            importe                // Total
            //    );
            //string nombreProducto = comboboxProducto.Text;
            //string nombreCliente = comboBoxCliente.Text;

            //CargarDetallesVenta(nombreProducto,nombreCliente,cantidad,precioUnitario);

            // Asumiendo que tienes todos los valores capturados desde el formulario.
            int ID_Venta = 0; // Generado automáticamente en la base de datos, o puedes capturarlo si ya lo tienes.
            DateTime Fecha = DateTime.Now; // O puedes tomarlo desde un control de fecha.
            decimal Importe = Convert.ToDecimal(txtimporte.Text);
            decimal IVA = 10.5m;
            IVA = decimal.Parse(txtimporte.Text);
            // Intentar convertir el texto a decimal
            // Obtener el texto del TextBox

            //string IVA = txtiva.Text;
            decimal Total = Importe + IVA;
            string Metodo_Pago = comboBoxmetodopago.SelectedItem.ToString();
            string ID_Cliente = comboBoxCliente.Text;

            // Lista de detalles de la venta (ID_Producto, Cantidad, PrecioUnitario)
            List<Tuple<int, int, decimal>> detallesVenta = new List<Tuple<int, int, decimal>>();

            // Supongamos que tienes un DataGridView con las columnas ID_Producto, Cantidad, PrecioUnitario para los detalles
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["ID_Producto"].Value != null && row.Cells["Cantidad"].Value != null && row.Cells["PrecioUnitario"].Value != null)
                {
                    int ID_Producto = Convert.ToInt32(row.Cells["ID_Producto"].Value);
                    int cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                    decimal precioUnitario = Convert.ToDecimal(row.Cells["PrecioUnitario"].Value);
                    detallesVenta.Add(new Tuple<int, int, decimal>(ID_Producto, cantidad, precioUnitario));
                }
            }

            // Llamada a RegistrarVenta
            bool resultado = await RegistrarVenta(ID_Venta, Fecha, Importe, IVA, Total, Metodo_Pago, ID_Cliente, detallesVenta);

            // Verificar si la venta fue registrada correctamente
            if (resultado)
            {
                MessageBox.Show("Venta registrada exitosamente.");
            }
            else
            {
                MessageBox.Show("Error al registrar la venta. Verifica los datos y el stock.");
            }
        }


        public async Task<bool> RegistrarVenta(int ID_Venta, DateTime Fecha, decimal Importe, decimal IVA, decimal Total, string Metodo_Pago, String ID_Cliente, List<Tuple<int, int, decimal>> detallesVenta)
        {
            SqlTransaction transaction = null;
            SqlConnection conexion = await Utilidades.ObtenerConexionAsync();

            try
            {
                // Obtener la conexión de forma asíncrona
                SqlConnection connection = await Utilidades.ObtenerConexionAsync();

                // Abrir la conexión
                await connection.OpenAsync();

                // Iniciar la transacción
                transaction = conexion.BeginTransaction();

                // Verificar la existencia del producto y su stock para cada detalle
                foreach (var detalle in detallesVenta)
                {
                    int ID_Producto = detalle.Item1;
                    int cantidadVendida = detalle.Item2;

                    string queryStock = "SELECT Cantidad_Disponible FROM Saldos WHERE ID_Producto = @ID_Producto";
                    using (SqlCommand cmdStock = new SqlCommand(queryStock, conexion, transaction))
                    {
                        cmdStock.Parameters.AddWithValue("@ID_Producto", ID_Producto);
                        object result = await cmdStock.ExecuteScalarAsync();

                        if (result == null)
                        {
                            Console.WriteLine($"No se encontró información de stock para el producto con ID {ID_Producto}");
                            transaction.Rollback();
                            return false;
                        }

                        decimal stockActual = Convert.ToDecimal(result);
                        if (stockActual < cantidadVendida)
                        {
                            Console.WriteLine($"Stock insuficiente para el producto con ID {ID_Producto}. Stock actual: {stockActual}, Cantidad solicitada: {cantidadVendida}");
                            transaction.Rollback();
                            return false;
                        }
                    }
                }

                // Insertar la venta y obtener el ID generado
                string queryVenta = "INSERT INTO Venta (Fecha, Importe, IVA, Total, Metodo_Pago, ID_Cliente) " +
                                    "VALUES (@Fecha, @Importe, @IVA, @Total, @Metodo_Pago, @ID_Cliente); " +
                                    "SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmdVenta = new SqlCommand(queryVenta, conexion, transaction))
                {
                    cmdVenta.Parameters.AddWithValue("@Fecha", Fecha);
                    cmdVenta.Parameters.AddWithValue("@Importe", Importe);
                    cmdVenta.Parameters.AddWithValue("@IVA", IVA);
                    cmdVenta.Parameters.AddWithValue("@Total", Total);
                    cmdVenta.Parameters.AddWithValue("@Metodo_Pago", Metodo_Pago);
                    cmdVenta.Parameters.AddWithValue("@ID_Cliente", ID_Cliente);

                    object result = await cmdVenta.ExecuteScalarAsync();
                    int idVentaGenerado = Convert.ToInt32(result);

                    // Insertar los detalles de la venta y actualizar el stock
                    foreach (var detalle in detallesVenta)
                    {
                        int idProducto = detalle.Item1;
                        int cantidadVendida = detalle.Item2;
                        decimal precioUnitario = detalle.Item3;
                        decimal subtotal = cantidadVendida * precioUnitario;

                        string queryDetalleVenta = "INSERT INTO Detalle_Venta (ID_Venta, ID_Producto, Cantidad, Precio_Unitario, Subtotal) " +
                                                   "VALUES (@ID_Venta, @ID_Producto, @Cantidad, @Precio_Unitario, @Subtotal)";
                        using (SqlCommand cmdDetalleVenta = new SqlCommand(queryDetalleVenta, conexion, transaction))
                        {
                            cmdDetalleVenta.Parameters.AddWithValue("@ID_Venta", idVentaGenerado);
                            cmdDetalleVenta.Parameters.AddWithValue("@ID_Producto", idProducto);
                            cmdDetalleVenta.Parameters.AddWithValue("@Cantidad", cantidadVendida);
                            cmdDetalleVenta.Parameters.AddWithValue("@Precio_Unitario", precioUnitario);
                            cmdDetalleVenta.Parameters.AddWithValue("@Subtotal", subtotal);
                            await cmdDetalleVenta.ExecuteNonQueryAsync();
                        }

                        string queryUpdateStock = "UPDATE Saldos SET Cantidad_Salida = Cantidad_Salida + @Cantidad WHERE ID_Producto = @ID_Producto";
                        using (SqlCommand cmdUpdateStock = new SqlCommand(queryUpdateStock, conexion, transaction))
                        {
                            cmdUpdateStock.Parameters.AddWithValue("@Cantidad", cantidadVendida);
                            cmdUpdateStock.Parameters.AddWithValue("@ID_Producto", idProducto);
                            await cmdUpdateStock.ExecuteNonQueryAsync();
                        }
                    }
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                Console.WriteLine($"Error al registrar la venta: {ex.Message}");
                return false;
            }
        }


        private void ConfigurarDataGridView()
        {
            dataGridView1.Columns.Add("ID_Producto", "ID_Producto");
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
