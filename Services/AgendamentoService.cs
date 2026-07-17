using BarbeariaZanetti.Web.Models;
using BarbeariaZanetti.Web.Repositories.Interfaces;
using BarbeariaZanetti.Web.Services.Interfaces;

namespace BarbeariaZanetti.Web.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _repository;

        public AgendamentoService(IAgendamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Agendamento>> BuscarPorDataAsync(DateTime data)
        {
            return await _repository.BuscarPorDataAsync(data);
        }

        public async Task<Agendamento?> BuscarPorIdAsync(int id)
        {
            return await _repository.BuscarPorIdAsync(id);
        }

        public async Task CriarAsync(Agendamento agendamento)
        {
            await _repository.CriarAsync(agendamento);
        }

        public async Task AtualizarAsync(Agendamento agendamento)
        {
            await _repository.AtualizarAsync(agendamento);
        }
    }
}