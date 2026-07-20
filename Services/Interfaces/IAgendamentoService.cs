using BarbeariaZanetti.Web.Models;

namespace BarbeariaZanetti.Web.Services.Interfaces
{
    public interface IAgendamentoService
    {
        Task<IEnumerable<Agendamento>> BuscarPorDataAsync(DateTime data);

        Task<IEnumerable<Agendamento>> BuscarPorMesAsync(
            int ano,
            int mes);

        Task<Agendamento?> BuscarPorIdAsync(int id);

        Task CriarAsync(Agendamento agendamento);

        Task AtualizarAsync(Agendamento agendamento);

        Task AtualizarHorariosDoDiaAsync(
            int barbeiroId,
            DateTime data);
    }
}