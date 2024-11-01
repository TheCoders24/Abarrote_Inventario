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
using CapaNegocio;

namespace Interfaz
{
    public partial class Inventario : Form
    {
        public Inventario()
        {
            InitializeComponent();
            CargarProveedoresAsync(comboBox1);
            Fecha();
        }

        private async void Inventario_Load(object sender, EventArgs e)
        {
            await CargarDatosInventarioAsync();
        }
        public async Task CargarDatosInventarioAsync()
        {
            string query = "SELECT [ID_Inventario], [Fecha_Registro], [Observaciones], [Importe], [IVA], [Total], [ID_Proveedor] FROM [AbarroteDB].[dbo].[Inventario]";

            try
            {
                using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable); // Llenar el DataTable con los datos de la consulta
                            dataListado.DataSource = dataTable; // Asignar el DataTable al DataGridView
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sucedió un error al cargar los datos: " + ex.Message);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
        private async Task CargarProveedoresAsync(ComboBox comboBoxProveedores)
        {
            string query = "SELECT ID_Proveedor, Nombre FROM Proveedor"; // Asegúrate de que el nombre de la tabla sea correcto

            try
            {
                using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
                {
                    if (connection == null)
                    {
                        MessageBox.Show("Error al establecer conexión con la base de datos.");
                        return;
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int idProveedor = reader.GetInt32(0);
                                string nombreProveedor = reader.GetString(1);

                                // Agregar los proveedores al ComboBox
                                comboBoxProveedores.Items.Add(new { Id = idProveedor, Nombre = nombreProveedor });
                            }
                        }
                    }
                }

                // Configurar el ComboBox para mostrar solo el nombre
                comboBoxProveedores.DisplayMember = "Nombre";
                comboBoxProveedores.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message);
            }
        }
        private int? ObtenerIdProveedorSeleccionado(ComboBox comboBoxProveedores)
        {
            // Verificar si hay un proveedor seleccionado
            if (comboBoxProveedores.SelectedItem != null)
            {
                // Obtener el objeto del proveedor seleccionado
                var proveedorSeleccionado = comboBoxProveedores.SelectedItem;

                // Usar Reflection para obtener el ID
                var idProveedor = (int)proveedorSeleccionado.GetType().GetProperty("Id").GetValue(proveedorSeleccionado);
                return idProveedor;
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un proveedor.");
                return null;
            }
        }
        private void Fecha()
        {
            var Fecha = DateTime.Now;
            txtfecha.Text = Fecha.ToShortDateString();
        }

        

        private async void btnalta_Click(object sender, EventArgs e)
        {
            DateTime fechaRegistro = DateTime.Now;
            string observaciones = txtObservacion.Text; // Observaciones que desees
            decimal importe = decimal.Parse(txtImporte.Text); // Importe que desees
            decimal iva = decimal.Parse(txtIVA.Text); // Porcentaje de IVA
            decimal total = importe + (importe * iva); // Cálculo del total
            txtTotal.Text = total.ToString();
            string nombreproveedor = comboBox1.Text;
            int? idProveedor = await ObtenerIdProveedorPorNombre(nombreproveedor);

            // Consulta SQL para insertar en Inventario
            string queryInsertInventario = @"
            INSERT INTO [AbarroteDB].[dbo].[Inventario] 
                  ([Fecha_Registro], [Observaciones], [Importe], [IVA], [Total], [ID_Proveedor])
            VALUES 
                  (@FechaRegistro, @Observaciones, @Importe, @IVA, @Total, @ID_Proveedor);";

            try
            {
                using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
                {
                    if (connection == null)
                    {
                        MessageBox.Show("Error al establecer conexión con la base de datos.");
                        return;
                    }

                    using (SqlCommand commandInsertInventario = new SqlCommand(queryInsertInventario, connection))
                    {
                        // Parámetros para la inserción
                        commandInsertInventario.Parameters.AddWithValue("@FechaRegistro", fechaRegistro);
                        commandInsertInventario.Parameters.AddWithValue("@Observaciones", observaciones);
                        commandInsertInventario.Parameters.AddWithValue("@Importe", importe);
                        commandInsertInventario.Parameters.AddWithValue("@IVA", iva * importe); // Calcular el IVA
                        commandInsertInventario.Parameters.AddWithValue("@Total", total);
                        commandInsertInventario.Parameters.AddWithValue("@ID_Proveedor", idProveedor);

                        // Ejecutar la inserción
                        int  filasAfectadas = await commandInsertInventario.ExecuteNonQueryAsync();
                        MessageBox.Show($"Filas afectadas por la inserción en Inventario: {filasAfectadas}");
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
        public async Task<int?> ObtenerIdProveedorPorNombre(string nombreProveedor)
        {
            string query = "SELECT ID_Proveedor FROM Proveedor WHERE Nombre = @NombreProveedor";

            using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
            {
                if (connection == null)
                {
                    throw new Exception("Error al establecer conexión con la base de datos.");
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreProveedor", nombreProveedor);

                    object resultado = await command.ExecuteScalarAsync();
                    return resultado != null ? (int?)Convert.ToInt32(resultado) : null; // Retorna el ID o null si no se encuentra
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
