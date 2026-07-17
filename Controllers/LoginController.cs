using BarbeariaZanetti.Web.Helpers;
using BarbeariaZanetti.Web.Services.Interfaces;
using BarbeariaZanetti.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BarbeariaZanetti.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Login) || string.IsNullOrWhiteSpace(model.Senha))
            {
                ViewBag.Erro = "Informe o login e a senha.";
                return View(model);
            }

            var usuario = await _usuarioService.BuscarPorLoginAsync(model.Login);

            if (usuario == null)
            {
                ViewBag.Erro = "Login ou senha inválidos.";
                return View(model);
            }

            var senhaValida = PasswordHelper.VerificarSenha(model.Senha, usuario.SenhaHash);

            if (!senhaValida)
            {
                ViewBag.Erro = "Login ou senha inválidos.";
                return View(model);
            }

            SessionHelper.CriarSessao(HttpContext, usuario);

            if (usuario.PrimeiroAcesso)
            {
                return RedirectToAction("AlterarSenhaInicial", "Usuario");
            }

            return RedirectToAction("Index", "Agenda");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            SessionHelper.EncerrarSessao(HttpContext);

            return RedirectToAction("Index");
        }
    }
}