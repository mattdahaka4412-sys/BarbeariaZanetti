using BarbeariaZanetti.Web.Helpers;
using BarbeariaZanetti.Web.Models;
using BarbeariaZanetti.Web.Services.Interfaces;
using BarbeariaZanetti.Web.ViewModels.Cliente;
using Microsoft.AspNetCore.Mvc;

namespace BarbeariaZanetti.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(ClienteViewModel model)
        {
            if (!SessionHelper.UsuarioEstaLogado(HttpContext))
            {
                return RedirectToAction("Index", "Login");
            }

            if (string.IsNullOrWhiteSpace(model.Nome))
            {
                TempData["Erro"] = "Informe o nome do cliente.";
                return RedirectToAction("Index", "Agenda");
            }

            var cliente = new Cliente
            {
                Nome = model.Nome,
                Telefone = model.Telefone,
                Observacao = model.Observacao,
                Ativo = true
            };

            await _clienteService.CriarAsync(cliente);

            TempData["Sucesso"] = "Cliente cadastrado com sucesso.";

            return RedirectToAction("Index", "Agenda");
        }
    }
}