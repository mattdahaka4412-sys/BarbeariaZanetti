namespace BarbeariaZanetti.Web.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public string SenhaHash { get; set; } = string.Empty;

        public int PerfilId { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public bool PrimeiroAcesso { get; set; }
    }
}