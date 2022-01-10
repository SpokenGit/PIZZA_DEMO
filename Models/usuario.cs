using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza_App.Models
{
    public class usuario
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Display(Name = "Nombre usuario")]
        [Required(ErrorMessage = "Nombre mandatorio")]
        public string nombre_usuario { get; set; }
        [MaxLength(30)]
        [Display(Name = "Tipo Usuario")]
        [Required(ErrorMessage = "Tipo mandatorio")]
        public string tipo_usuario { get; set; }
        [MaxLength(30)]
        [Display(Name = "Clave")]
        [Required(ErrorMessage = "Clave mandatorio")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public DateTime fecha_creacion { get; set; }
    }
}
