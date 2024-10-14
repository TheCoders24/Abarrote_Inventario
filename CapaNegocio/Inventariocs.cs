using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class Inventariocs
    {
        public int ID_Inventario { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public string Observaciones { get; set; }
        public decimal Importe { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public int ID_Proveedor { get; set; }
    }
}
