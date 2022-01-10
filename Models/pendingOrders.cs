using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza_App.Models
{
    public class pendingOrders
    {
        public DateTime fechaorden { get; set; }
        public int numeroOrden { get; set; }
        public string solicitanteOrden { get; set; }
        public int cantidadOrden { get; set; }
        public decimal precioOrden { get; set; }
        public decimal totalOrden { get; set; }
        public string usuarioOrden { get; set; }
    }
}
