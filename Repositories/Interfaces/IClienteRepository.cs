using BarbeariaZanetti.Web.Models;

namespace BarbeariaZanetti.Web.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> BuscarTodosAsync();

        Task<int> CriarAsync(Cliente cliente);
    }
}