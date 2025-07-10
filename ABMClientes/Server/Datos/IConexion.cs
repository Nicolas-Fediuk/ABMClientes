using ABMClientes.Shared;

namespace ABMClientes.Server.Datos
{
    public interface IConexion
    {
        Task CrearCliente(Cliente cliente);
        Task EditarCliente(Cliente cliente);
        Task EliminarCliente(int id);
        Task<Cliente> GetCliente(int id);
        Task<IEnumerable<Cliente>> GetClienteConFiltro(ClienteFiltro clienteFiltro);
        Task<IEnumerable<Cliente>> GetClientes();
        Task<IEnumerable<Cliente>> GetClientesId(int id);
        Task PostError(LogErrores logErrores);
        Task<bool> VerificarCuit(string cuit);
        Task<bool> VerificarCuitClienteExistente(Cliente cliente);
    }
}
