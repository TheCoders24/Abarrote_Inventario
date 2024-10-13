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
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void btnproductos_Click(object sender, EventArgs e)
        {
            Productos productos = new Productos();
            productos.ShowDialog();
            
        }

        private void btnproveedores_Click(object sender, EventArgs e)
        {
            Proveedor proveedor = new Proveedor();
            proveedor.ShowDialog();
        }

        private void btnclientes_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            cliente.ShowDialog();
        }

        private void btndetallesventas_Click(object sender, EventArgs e)
        {
            Venta ventas = new Venta();
            ventas.ShowDialog();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            Inventario inventario = new Inventario();   
            inventario.ShowDialog();    
        }

        private void btnDetallesIvnetario_Click(object sender, EventArgs e)
        {
            DetalleVenta detalleVenta = new DetalleVenta();
            detalleVenta.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReporteForms reporte = new ReporteForms();
            reporte.ShowDialog();
        }
    }
}
