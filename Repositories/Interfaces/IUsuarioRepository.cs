using BarbeariaZanetti.Web.Models;

namespace BarbeariaZanetti.Web.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> BuscarPorLoginAsync(string login);

        Task<IEnumerable<Usuario>> BuscarBarbeirosAsync();
    }
}