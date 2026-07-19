namespace BarbeariaZanetti.Web.ViewModels.Agenda
{
    public class NovoAgendamentoViewModel
    {
        public int ClienteId { get; set; }

        public int ServicoId { get; set; }

        // Apenas o Admin preencherá esse campo
        public int? BarbeiroId { get; set; }

        public DateTime Data { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFim { get; set; }

        public decimal ValorCobrado { get; set; }

        public string? Observacao { get; set; }
    }
}