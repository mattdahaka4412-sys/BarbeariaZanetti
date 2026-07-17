using BarbeariaZanetti.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarbeariaZanetti.Web.Controllers
{
    public class ServicoController : Controller
    {
        private readonly IServicoService _servicoService;

        public ServicoController(IServicoService servicoService)
        {
            _servicoService = servicoService;
        }

        public async Task<IActionResult> Index()
        {
            var servicos = await _servicoService.BuscarTodosAsync();

            return View(servicos);
        }
    }
}