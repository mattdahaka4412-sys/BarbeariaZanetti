using BarbeariaZanetti.Web.Models;

namespace BarbeariaZanetti.Web.Services.Interfaces
{
    public interface IServicoService
    {
        Task<IEnumerable<Servico>> BuscarTodosAsync();
        Task<Servico?> BuscarPorIdAsync(int id);
        Task InserirAsync(Servico servico);
        Task AtualizarAsync(Servico servico);
        Task ExcluirAsync(int id);
    }
}