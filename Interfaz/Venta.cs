using CapaDatos;
using CapaNegocio;
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
    public partial class Venta : Form
    {
        public Venta()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

          

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private async void Venta_Load(object sender, EventArgs e)
        {
            await CargarDatosVentasAsync();
        }

        public async Task CargarDatosVentasAsync()
        {
            string query = "SELECT TOP (1000) [ID_Venta], [Fecha], [Importe], [Iva], [Total], [Metodo_Pago], [ID_Cliente] FROM [AbarroteDB].[dbo].[Venta]";

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
    }
}
