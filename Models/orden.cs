using Pizza_App.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza_App.Models
{
    public class orden
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Nombre del Solicitante mandatorio")]
        [Display(Name= "Nombre del Solicitante")]
        public string nombre_solicitante { get; set; }
        [Display(Name ="Tipo de Pizza")]
        [CheckStipoPizza]
        public int pizzaId { get; set; }
        public pizza pizza { get; set; }
        [Display(Name ="Cantidad")]
        [Range(0, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int cantidad { get; set; }
        [Display(Name ="Fecha de Orden")]
        public DateTime fecha_orden { get; set; }
        [MaxLength(200)]
        [Display(Name ="Direccion de envio")]
        public string direccion_envio { get; set; }
        [MaxLength(200)]
        [Display(Name ="Comentarios de la Orden")]
        public string comentarios { get; set; }
        [Display(Name ="Estado de la Orden")]
        [CheckStatusOrden]
        public int estado_orden { get; set; }
        public int usuarioId { get; set; }
        public usuario usuario { get; set; }

    }
}
