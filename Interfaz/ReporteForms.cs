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
    public partial class ReporteForms : Form
    {
        public ReporteForms()
        {
            InitializeComponent();
            LoadReportTypes();
        }

        private void ReporteForms_Load(object sender, EventArgs e)
        {

        }

        private void LoadReportTypes()
        {
            comboBox1.Items.Add("Todos los Clientes");
            comboBox1.Items.Add("Todos los Proveedores");
            comboBox1.Items.Add("Productos y Stock");
        }

        private async void btnGenerar_Click(object sender, EventArgs e)
        {
            string selectedReport = comboBox1.SelectedItem.ToString();
            DataTable reportData = new DataTable();

            switch (selectedReport)
            {
                case "Todos los Clientes":
                    reportData = await GetAllClients();
                    break;
                case "Todos los Proveedores":
                    reportData = await GetAllSuppliers();
                    break;
                case "Productos y Stock":
                    reportData = await GetProductsAndStock();
                    break;
                default:
                    MessageBox.Show("Seleccione un tipo de reporte.");
                    return;
            }

            dataGridViewreport.DataSource = reportData;
        }

        private async Task<DataTable> GetAllClients()
        {
            // Crear la conexión de forma asíncrona
            using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
            {
                string query = "SELECT * FROM Cliente";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader); // Cargar datos directamente del SqlDataReader
                        return dataTable;
                    }
                }
            }
        }

        private async Task<DataTable> GetAllSuppliers()
        {
            // Crear la conexión de forma asíncrona
            using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
            {
                string query = "SELECT * FROM Proveedor";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader); // Cargar datos directamente del SqlDataReader
                        return dataTable;
                    }
                }
            }
        }

        private async Task<DataTable> GetProductsAndStock()
        {
            // Crear la conexión de forma asíncrona
            using (SqlConnection connection = await Utilidades.ObtenerConexionAsync())
            {
                string query = @"
                SELECT p.ID_Producto, p.Nombre, 
                       ISNULL(s.Cantidad_Disponible, 0) AS Cantidad_Disponible
                FROM Producto p
                LEFT JOIN Saldos s ON p.ID_Producto = s.ID_Producto";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader); // Cargar datos directamente del SqlDataReader
                        return dataTable;
                    }
                }
            }
        }


    }
}
