using ClienteModel = BarbeariaZanetti.Web.Models.Cliente;
using ServicoModel = BarbeariaZanetti.Web.Models.Servico;

namespace BarbeariaZanetti.Web.ViewModels.Agenda
{
    public class AgendaViewModel
    {
        public bool EhAdministrador { get; set; }

        public int UsuarioId { get; set; }

        public string UsuarioNome { get; set; } = string.Empty;

        public int AnoSelecionado { get; set; }

        public int MesSelecionado { get; set; }

        public string NomeMes { get; set; } = string.Empty;

        public IEnumerable<ClienteModel> Clientes { get; set; }
            = new List<ClienteModel>();

        public IEnumerable<ServicoModel> Servicos { get; set; }
            = new List<ServicoModel>();
    }
}