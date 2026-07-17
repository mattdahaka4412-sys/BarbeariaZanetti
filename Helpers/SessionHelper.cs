using BarbeariaZanetti.Web.Models;

namespace BarbeariaZanetti.Web.Helpers
{
    public static class SessionHelper
    {
        public static void CriarSessao(HttpContext httpContext, Usuario usuario)
        {
            httpContext.Session.SetInt32("UsuarioId", usuario.Id);
            httpContext.Session.SetString("UsuarioNome", usuario.Nome);
            httpContext.Session.SetInt32("PerfilId", usuario.PerfilId);
        }

        public static int? ObterUsuarioId(HttpContext httpContext)
        {
            return httpContext.Session.GetInt32("UsuarioId");
        }

        public static string? ObterUsuarioNome(HttpContext httpContext)
        {
            return httpContext.Session.GetString("UsuarioNome");
        }

        public static int? ObterPerfilId(HttpContext httpContext)
        {
            return httpContext.Session.GetInt32("PerfilId");
        }

        public static bool UsuarioEstaLogado(HttpContext httpContext)
        {
            return ObterUsuarioId(httpContext) != null;
        }

        public static bool UsuarioEhAdministrador(HttpContext httpContext)
        {
            return ObterPerfilId(httpContext) == 1;
        }

        public static bool UsuarioEhBarbeiro(HttpContext httpContext)
        {
            return ObterPerfilId(httpContext) == 2;
        }

        public static void EncerrarSessao(HttpContext httpContext)
        {
            httpContext.Session.Clear();
        }
    }
}