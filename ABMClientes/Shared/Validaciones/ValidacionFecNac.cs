using System.ComponentModel.DataAnnotations;

namespace CineAPI.Validaciones
{
    public class ValidacionFecNac : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if(value is DateTime fechaDeNac)
            {
                var hoy = DateTime.Today;
                var edad = hoy.Year - fechaDeNac.Year;

                if (fechaDeNac.Date > hoy.AddYears(-edad))
                {
                    edad--;
                }

                if (edad < 18)
                {
                    return new ValidationResult("Tenés que ser mayor de 18 años.");
                }

                if(fechaDeNac >= DateTime.Now && fechaDeNac <= DateTime.Now.AddYears(-200))
                {
                    return new ValidationResult("Ingrese una fecha de nacimiento valido.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
