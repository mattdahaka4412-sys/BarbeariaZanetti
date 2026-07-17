using BarbeariaZanetti.Web.Models;

namespace BarbeariaZanetti.Web.Services.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> BuscarTodosAsync();

        Task<int> CriarAsync(Cliente cliente);
    }
}