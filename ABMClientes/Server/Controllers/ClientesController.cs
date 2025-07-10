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
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            try
            {
                var clientes = await conexion.GetClientes();
                return Ok(clientes);    
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetId(int id)
        {
            try
            {
                var cliente = await conexion.GetClientesId(id);
                return Ok(cliente);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("editar/{id:int}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            Cliente cliente = new Cliente();

            try
            {
                var datosCliente = await conexion.GetCliente(id);
                return Ok(datosCliente);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("filtro")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClienteFiltro([FromBody] ClienteFiltro clienteFiltro)
        {
            try
            {
                var clientes = await conexion.GetClienteConFiltro(clienteFiltro);
                return Ok(clientes);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("editar")]
        public async Task<ActionResult<Cliente>> Put(Cliente cliente)
        {
            try
            {
                bool existeCuit = await conexion.VerificarCuitClienteExistente(cliente);

                if (existeCuit)
                {
                    return BadRequest("El CUIT ingresado ya existe");
                }

                await conexion.EditarCliente(cliente);

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
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
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        } 
    }
}
