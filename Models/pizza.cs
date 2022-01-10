using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza_App.Models
{
    public class pizza
    {
        public int Id { get; set; }
        [Display(Name = "Nombre Pizza")]
        [Required(ErrorMessage = "Nombre mandatorio")]
        [MaxLength(100)]
        public string nombre_pizza { get; set; }
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Cantidad mandatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int cantidad_porciones { get; set; }
        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Descripcion mandatorio")]
        [MaxLength(200)]
        public string descripcion { get; set; }
        [Display(Name = "Precio Unitario")]
        [Required(ErrorMessage = "Precio mandatorio")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal precio_unitario { get; set; }
        [Display(Name = "Tamano")]
        [Required(ErrorMessage = "Tamano mandatorio")]
        [MaxLength(50)]
        public string tamano { get; set; }
    }
}
