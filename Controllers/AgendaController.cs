using BarbeariaZanetti.Web.Helpers;
using BarbeariaZanetti.Web.Models;
using BarbeariaZanetti.Web.Services.Interfaces;
using BarbeariaZanetti.Web.ViewModels.Agenda;
using Microsoft.AspNetCore.Mvc;

namespace BarbeariaZanetti.Web.Controllers
{
    public class AgendaController : Controller
    {
        private readonly IAgendamentoService _agendamentoService;
        private readonly IClienteService _clienteService;
        private readonly IServicoService _servicoService;

        public AgendaController(
            IAgendamentoService agendamentoService,
            IClienteService clienteService,
            IServicoService servicoService)
        {
            _agendamentoService = agendamentoService;
            _clienteService = clienteService;
            _servicoService = servicoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? mes, int? ano)
        {
            if (!SessionHelper.UsuarioEstaLogado(HttpContext))
            {
                return RedirectToAction("Index", "Login");
            }

            var dataAtual = DateTime.Today;

            var mesSelecionado = mes ?? dataAtual.Month;
            var anoSelecionado = ano ?? dataAtual.Year;

            if (mesSelecionado < 1 || mesSelecionado > 12)
            {
                mesSelecionado = dataAtual.Month;
            }

            if (anoSelecionado < 2000 || anoSelecionado > 2100)
            {
                anoSelecionado = dataAtual.Year;
            }

            var primeiroDiaDoMes = new DateTime(anoSelecionado, mesSelecionado, 1);

            var model = new AgendaViewModel
            {
                EhAdministrador = SessionHelper.UsuarioEhAdministrador(HttpContext),
                UsuarioId = SessionHelper.ObterUsuarioId(HttpContext)!.Value,
                UsuarioNome = SessionHelper.ObterUsuarioNome(HttpContext) ?? string.Empty,

                AnoSelecionado = anoSelecionado,
                MesSelecionado = mesSelecionado,
                NomeMes = primeiroDiaDoMes.ToString(
                "MMMM yyyy",
                new System.Globalization.CultureInfo("pt-BR")),

                Clientes = await _clienteService.BuscarTodosAsync(),
                Servicos = await _servicoService.BuscarTodosAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(NovoAgendamentoViewModel model)
        {
            if (!SessionHelper.UsuarioEstaLogado(HttpContext))
            {
                return RedirectToAction("Index", "Login");
            }

            var usuarioId = SessionHelper.ObterUsuarioId(HttpContext);

            if (usuarioId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var dataHoraInicio = model.Data.Date + model.HoraInicio;
            var dataHoraFim = model.Data.Date + model.HoraFim;

            var agendamento = new Agendamento
            {
                ClienteId = model.ClienteId,
                BarbeiroId = usuarioId.Value,
                ServicoId = model.ServicoId,
                StatusId = 1,
                FormaPagamentoId = null,
                DataHoraInicio = dataHoraInicio,
                DataHoraFim = dataHoraFim,
                ValorCobrado = model.ValorCobrado,
                Observacao = model.Observacao
            };

            await _agendamentoService.CriarAsync(agendamento);

            return RedirectToAction("Index");
        }
    }
}