using BarbeariaZanetti.Web.Models;
using BarbeariaZanetti.Web.Repositories.Interfaces;
using BarbeariaZanetti.Web.Services.Interfaces;

namespace BarbeariaZanetti.Web.Services
{
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _servicoRepository;

        public ServicoService(IServicoRepository servicoRepository)
        {
            _servicoRepository = servicoRepository;
        }

        public async Task<IEnumerable<Servico>> BuscarTodosAsync()
        {
            return await _servicoRepository.BuscarTodosAsync();
        }

        public async Task<Servico?> BuscarPorIdAsync(int id)
        {
            return await _servicoRepository.BuscarPorIdAsync(id);
        }

        public async Task InserirAsync(Servico servico)
        {
            await _servicoRepository.InserirAsync(servico);
        }

        public async Task AtualizarAsync(Servico servico)
        {
            await _servicoRepository.AtualizarAsync(servico);
        }

        public async Task ExcluirAsync(int id)
        {
            await _servicoRepository.ExcluirAsync(id);
        }
    }
}