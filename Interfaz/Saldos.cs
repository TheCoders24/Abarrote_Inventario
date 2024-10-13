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
    public partial class Saldos : Form
    {
        public Saldos()
        {
            InitializeComponent();
            CargarProductos();
            CargarSaldos(); // Cargar los saldos al iniciar
        }

        private void Saldos_Load(object sender, EventArgs e)
        {

        }
        private async void CargarSaldos()
        {
            string query = "SELECT ID_Saldo, ID_Producto, Cantidad_Entrante, Cantidad_Salida, Cantidad_Disponible FROM Saldos";

            using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            agregar();
        }

        private async void CargarProductos()
        {
             string query = "SELECT ID_Producto, Nombre AS Nombre_Producto FROM [AbarroteDB].[dbo].[Producto]"; // Asegúrate de que esta consulta sea correcta según tu esquema

    using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
    {
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            comboBoxidproducto.DisplayMember = "Nombre_Producto"; // Cambia según el nombre de tu columna que quieres mostrar
            comboBoxidproducto.ValueMember = "ID_Producto"; // Asegúrate de que esto es el ID del producto
            comboBoxidproducto.DataSource = dt;
        }
    }
        }

        private async void agregar()
        {
            string nombreProductoSeleccionado = comboBoxidproducto.Text; // Obtener el nombre del producto seleccionado
            int? idProducto;

            try
            {
                idProducto = await ObtenerIDProductoPorNombre(nombreProductoSeleccionado); // Obtener el ID del producto
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Mostrar mensaje de error si no se encuentra el producto
                return; // Salir si hay un error
            }

            string queryInsert = @"
            INSERT INTO Saldos (ID_Producto, Cantidad_Entrante, Cantidad_Salida) 
            VALUES (@ID_Producto, @CantidadEntrante, @CantidadSalida)"; // Eliminar Cantidad_Disponible

            using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
            {
                using (SqlCommand command = new SqlCommand(queryInsert, connection))
                {
                    command.Parameters.AddWithValue("@ID_Producto", comboBoxidproducto.SelectedValue); // Obtener el ID del producto
                    command.Parameters.AddWithValue("@CantidadEntrante", txtcantidadentrada.Text);
                    command.Parameters.AddWithValue("@CantidadSalida", txcantidadsalida.Text);
                    // No es necesario agregar Cantidad_Disponible aquí

                    await command.ExecuteNonQueryAsync();
                }
            }

            CargarSaldos(); // Recargar la lista de saldosos
        }
        private async Task<int?> ObtenerIDProductoPorNombre(string nombreProducto)
        {
            string query = "SELECT ID_Producto FROM [AbarroteDB].[dbo].[Producto] WHERE Nombre = @Nombre";

            using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", nombreProducto);

                    // Ejecuta la consulta y obtiene el resultado
                    object result = await command.ExecuteScalarAsync();
                    return result != null ? Convert.ToInt32(result) : (int?)null; // Devuelve el ID o null si no se encontró
                }
            }
        }
        private async void Modificar()
        {
            if (string.IsNullOrWhiteSpace(txtIDSaldo.Text)) return; // Asegúrate de que se haya seleccionado un saldo

            string nombreProductoSeleccionado = comboBoxidproducto.Text; // Obtener el nombre del producto seleccionado
            int? idProducto;

            try
            {
                idProducto = await ObtenerIDProductoPorNombre(nombreProductoSeleccionado); // Obtener el ID del producto
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Mostrar mensaje de error si no se encuentra el producto
                return; // Salir si hay un error
            }

            string queryUpdate = @"
            UPDATE Saldos 
            SET ID_Producto = @ID_Producto,
                Cantidad_Entrante = @CantidadEntrante,
                Cantidad_Salida = @CantidadSalida,
                Cantidad_Disponible = @CantidadDisponible 
            WHERE ID_Saldo = @ID_Saldo";

            using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
            {
                using (SqlCommand command = new SqlCommand(queryUpdate, connection))
                {
                    command.Parameters.AddWithValue("@ID_Saldo", txtIDSaldo.Text);
                    command.Parameters.AddWithValue("@ID_Producto", idProducto); // Usar el ID obtenido
                    command.Parameters.AddWithValue("@CantidadEntrante", txtcantidadentrada.Text);
                    command.Parameters.AddWithValue("@CantidadSalida", txcantidadsalida.Text);
                    command.Parameters.AddWithValue("@CantidadDisponible", CalculateAvailableQuantity().ToString());

                    await command.ExecuteNonQueryAsync();
                }
            }

            CargarSaldos(); // Recargar la lista de saldos
        }


        private async void Elimianr()
        {
            if (string.IsNullOrWhiteSpace(txtIDSaldo.Text)) return;

            // Obtener el ID del saldo a eliminar
            string queryIDSaldo = "SELECT ID_Producto FROM Saldos WHERE ID_Saldo = @ID_Saldo";

            int idProducto;
            using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
            {
                using (SqlCommand command = new SqlCommand(queryIDSaldo, connection))
                {
                    command.Parameters.AddWithValue("@ID_Saldo", txtIDSaldo.Text);
                    var result = await command.ExecuteScalarAsync(); // Obtener el ID del producto del saldo

                    if (result != null)
                    {
                        idProducto = Convert.ToInt32(result); // Convertir el resultado a int
                    }
                    else
                    {
                        MessageBox.Show("Saldo no encontrado."); // Mensaje si no se encuentra el saldo
                        return; // Salir si no se encuentra el saldo
                    }
                }
            }

            // Ahora eliminar el registro de Saldos usando el ID del producto
            string queryDelete = "DELETE FROM Saldos WHERE ID_Producto = @ID_Producto";

            using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
            {
                using (SqlCommand command = new SqlCommand(queryDelete, connection))
                {
                    command.Parameters.AddWithValue("@ID_Producto", idProducto); // Usar el ID del producto para eliminar
                    await command.ExecuteNonQueryAsync();
                }
            }

            CargarSaldos(); // Recargar la lista de saldos
        }
        private decimal CalculateAvailableQuantity()
        {
         
            decimal cantidadEntrante = decimal.Parse(txtcantidadentrada.Text);
            decimal cantidadSalida = decimal.Parse(txcantidadsalida.Text);
           decimal totalcantidad =  cantidadEntrante - cantidadSalida;
            return totalcantidad;
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            Elimianr();
        }
    }
}
