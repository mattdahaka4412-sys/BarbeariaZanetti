using BarbeariaZanetti.Web.Models;
using BarbeariaZanetti.Web.Repositories.Interfaces;
using BarbeariaZanetti.Web.Services.Interfaces;

namespace BarbeariaZanetti.Web.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario?> BuscarPorLoginAsync(string login)
        {
            return await _usuarioRepository.BuscarPorLoginAsync(login);
        }
    }
}