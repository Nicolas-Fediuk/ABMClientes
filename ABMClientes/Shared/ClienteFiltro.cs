using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMClientes.Shared
{
    public class ClienteFiltro
    {
        public string? CLIENTE_NOMBRES { get; set; } 
        public string? CLIENTE_APELLIDOS { get; set; }
        public string? CLIENTE_CUIT { get; set; }
    }
}
