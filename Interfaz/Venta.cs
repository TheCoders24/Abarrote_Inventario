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

            // Creación de una instancia de NVenta
            NVenta nVenta = new NVenta();
            DateTime fecga = DateTime.Now;


            // Inserción de una nueva venta
            // Llama a la función para insertar la venta
            string resultado = NVenta.InsertarVenta(int.Parse(txtFolio.Text), fecga,
                decimal.Parse(txtImporte.Text), decimal.Parse(txtIVA.Text),
                decimal.Parse(txtTotal.Text), txtmetodopago.Text);

            // Manejo del resultado
            if (resultado == "Ok")
            {
                MessageBox.Show("Venta insertada correctamente.");
            }
            else
            {
                MessageBox.Show("Error al insertar la venta: " + resultado);
            }


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
