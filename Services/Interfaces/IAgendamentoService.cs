using BarbeariaZanetti.Web.Models;

namespace BarbeariaZanetti.Web.Services.Interfaces
{
    public interface IAgendamentoService
    {
        Task<IEnumerable<Agendamento>> BuscarPorDataAsync(DateTime data);
        Task<Agendamento?> BuscarPorIdAsync(int id);
        Task CriarAsync(Agendamento agendamento);
        Task AtualizarAsync(Agendamento agendamento);
    }
}