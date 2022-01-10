using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizza_App.Models
{
    public class Account
    {
        [Required(ErrorMessage = "Usuario mandatorio")]
        public string userlogged { get; set; }
        [Required(ErrorMessage = "Password mandatorio")]
        public string password { get; set; }
    }
}
