namespace BarbeariaZanetti.Web.Models
{
    public class Agendamento
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public int BarbeiroId { get; set; }

        public int ServicoId { get; set; }

        public int StatusId { get; set; }

        public int? FormaPagamentoId { get; set; }

        public DateTime DataHoraInicio { get; set; }

        public DateTime DataHoraFim { get; set; }

        public decimal ValorCobrado { get; set; }

        public string? Observacao { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataAtualizacao { get; set; }
    }
}