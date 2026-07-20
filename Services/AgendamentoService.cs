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

        public async Task<IEnumerable<Agendamento>> BuscarPorMesAsync(
            int ano,
            int mes)
        {
            return await _repository.BuscarPorMesAsync(
                ano,
                mes);
        }

        public async Task<Agendamento?> BuscarPorIdAsync(int id)
        {
            return await _repository.BuscarPorIdAsync(id);
        }

        public async Task CriarAsync(Agendamento agendamento)
        {
            await _repository.CriarAsync(agendamento);

            await AtualizarHorariosDoDiaAsync(
                agendamento.BarbeiroId,
                agendamento.DataHoraInicio.Date);
        }

        public async Task AtualizarAsync(Agendamento agendamento)
        {
            await _repository.AtualizarAsync(agendamento);

            await AtualizarHorariosDoDiaAsync(
                agendamento.BarbeiroId,
                agendamento.DataHoraInicio.Date);
        }

        public async Task AtualizarHorariosDoDiaAsync(
            int barbeiroId,
            DateTime data)
        {
            var agendamentos = (await _repository.BuscarPorBarbeiroEDataAsync(
                barbeiroId,
                data))
                .OrderBy(a => a.DataHoraInicio)
                .ToList();

            for (int i = 0; i < agendamentos.Count; i++)
            {
                var agendamentoAtual = agendamentos[i];

                DateTime dataHoraFim;

                var existeProximoAgendamento = i < agendamentos.Count - 1;

                if (existeProximoAgendamento)
                {
                    var proximoAgendamento = agendamentos[i + 1];

                    dataHoraFim = proximoAgendamento.DataHoraInicio;
                }
                else
                {
                    var horaInicio = agendamentoAtual.DataHoraInicio.TimeOfDay;

                    if (horaInicio == new TimeSpan(19, 0, 0))
                    {
                        dataHoraFim = agendamentoAtual.DataHoraInicio.Date
                            .AddHours(20);
                    }
                    else
                    {
                        dataHoraFim = agendamentoAtual.DataHoraInicio.Date
                            .AddHours(19);
                    }
                }

                await _repository.AtualizarHoraFimAsync(
                    agendamentoAtual.Id,
                    dataHoraFim);
            }
        }
    }
}