namespace BarbeariaZanetti.Web.Helpers
{
    public static class PasswordHelper
    {
        public static string GerarHash(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public static bool VerificarSenha(string senhaDigitada, string senhaHash)
        {
            return BCrypt.Net.BCrypt.Verify(senhaDigitada, senhaHash);
        }
    }
}