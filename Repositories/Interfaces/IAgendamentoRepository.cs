using BarbeariaZanetti.Web.Models;

namespace BarbeariaZanetti.Web.Repositories.Interfaces
{
    public interface IAgendamentoRepository
    {
        Task<IEnumerable<Agendamento>> BuscarPorDataAsync(DateTime data);

        Task<IEnumerable<Agendamento>> BuscarPorBarbeiroEDataAsync(
            int barbeiroId,
            DateTime data);

        Task<Agendamento?> BuscarPorIdAsync(int id);

        Task CriarAsync(Agendamento agendamento);

        Task AtualizarAsync(Agendamento agendamento);

        Task AtualizarHoraFimAsync(
            int agendamentoId,
            DateTime dataHoraFim);
    }
}