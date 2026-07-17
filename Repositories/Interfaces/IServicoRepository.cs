using BarbeariaZanetti.Web.Models;

namespace BarbeariaZanetti.Web.Repositories.Interfaces
{
    public interface IServicoRepository
    {
        Task<IEnumerable<Servico>> BuscarTodosAsync();

        Task<Servico?> BuscarPorIdAsync(int id);

        Task InserirAsync(Servico servico);

        Task AtualizarAsync(Servico servico);

        Task ExcluirAsync(int id);
    }
}