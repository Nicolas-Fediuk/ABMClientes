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
        [RegularExpression(@"^\d{11}$", ErrorMessage = "En el campo CUIT Solo se permiten 11 números.")]
        public string CLIENTE_CUIT { get; set; }

        public string CLIENTE_DOMICILIO { get; set; }

        [Required(ErrorMessage = "El campo telefono es requerido")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "En el campo Telefono solo se permiten 10 números.")]
        public string CLIENTE_TELEFONO { get; set; }

        [Required(ErrorMessage = "El campo email es requerido.")]
        [EmailAddress(ErrorMessage = "Ingrese un mail válido.")]
        public string CLIENTE_EMAIL { get; set; }
    }
}
