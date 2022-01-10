using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza_App.Validations
{
    public class CheckStipoPizza: ValidationAttribute
    {
        public CheckStipoPizza() :base("Seleccion el {0}")
        { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valor = Convert.ToInt32(value);
                if (valor == 0)
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);

                }

            }
            return ValidationResult.Success;
        }

    }
}
