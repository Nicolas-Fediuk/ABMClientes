using ABMClientes.Shared;

namespace ABMClientes.Server.Datos
{
    public interface IConexion
    {
        Task CrearCliente(Cliente cliente);
        Task EditarCliente(Cliente cliente);
        Task EliminarCliente(int id);
        Task<Cliente> GetCliente(int id);
        Task<IEnumerable<Cliente>> GetClientes();
    }
}
