using ABMClientes.Server.Datos;
using ABMClientes.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ABMClientes.Server.Controllers
{
    [ApiController]
    [Route("api/cliente")]

    public class ClientesController : ControllerBase
    {
        private readonly IConexion conexion;

        public ClientesController(IConexion conexion)
        {
            this.conexion = conexion;
        }

        [HttpGet]
        public async Task<IEnumerable<Cliente>> Get()
        {
            return await conexion.GetClientes();
        }

        [HttpGet("{id:int}")]
        public async Task<IEnumerable<Cliente>> GetId(int id)
        {
            return await conexion.GetClientesId(id);
        }

        [HttpGet("editar/{id:int}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            Cliente cliente = new Cliente();

            cliente = await conexion.GetCliente(id);

            return Ok(cliente);
        }

        [HttpPost("filtro")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClienteFiltro([FromBody] ClienteFiltro clienteFiltro)
        {
            var clientes = await conexion.GetClienteConFiltro(clienteFiltro);

            return Ok(clientes);
        }

        [HttpPut("editar")]
        public async Task<ActionResult<Cliente>> Put(Cliente cliente)
        {
            bool existeCuit = await conexion.VerificarCuitClienteExistente(cliente);

            if (existeCuit)
            {
                return BadRequest("El CUIT ingresado ya existe");
            }

            await conexion.EditarCliente(cliente);

            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task Delete(int id)
        {
            await conexion.EliminarCliente(id);
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(Cliente cliente)
        {
            try
            {
                bool existeCuit = await conexion.VerificarCuit(cliente.CLIENTE_CUIT.Trim());

                if (existeCuit)
                {
                    return BadRequest("El CUIT ingresado ya existe");
                }

                await conexion.CrearCliente(cliente);
                return Ok();    
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        } 
    }
}
