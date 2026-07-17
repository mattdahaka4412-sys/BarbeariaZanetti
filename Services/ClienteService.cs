using BarbeariaZanetti.Web.Models;
using BarbeariaZanetti.Web.Repositories.Interfaces;
using BarbeariaZanetti.Web.Services.Interfaces;

namespace BarbeariaZanetti.Web.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> BuscarTodosAsync()
        {
            return await _clienteRepository.BuscarTodosAsync();
        }

        public async Task<int> CriarAsync(Cliente cliente)
        {
            return await _clienteRepository.CriarAsync(cliente);
        }
    }
}