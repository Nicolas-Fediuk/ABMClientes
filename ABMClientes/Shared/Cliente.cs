using CineAPI.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMClientes.Shared
{
    public class Cliente
    {
        public int CLIENTE_ID { get; set;}

        [Required(ErrorMessage = "El campo nombres es requerido.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 30 caracteres.")]
        public string CLIENTE_NOMBRES { get; set; }

        [Required(ErrorMessage = "El campo apellidos es requerido.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El apellido debe tener entre 3 y 30 caracteres.")]
        public string CLIENTE_APELLIDOS { get; set; }

        [ValidacionFecNac]
        public DateTime CLIENTE_FECNAC { get; set; }

        [Required(ErrorMessage = "El campo CUIT es requerido.")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "El CUIT debe tener entre 10 y 12 caracteres.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "En el campo CUIT Solo se permiten números.")]
        public string CLIENTE_CUIT { get; set; }

        public string CLIENTE_DOMICILIO { get; set; }

        [Required(ErrorMessage = "El campo telefono es requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "En el campo Telefono Solo se permiten números.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Deben ser 10 caracteres.")]
        public string CLIENTE_TELEFONO { get; set; }

        [Required(ErrorMessage = "El campo email es requerido.")]
        [EmailAddress(ErrorMessage = "Ingrese un mail válido.")]
        public string CLIENTE_EMAIL { get; set; }
    }
}
