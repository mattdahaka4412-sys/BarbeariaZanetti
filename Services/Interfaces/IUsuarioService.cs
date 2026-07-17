using BarbeariaZanetti.Web.Models;

namespace BarbeariaZanetti.Web.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario?> BuscarPorLoginAsync(string login);
    }
}