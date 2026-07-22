using BarbeariaZanetti.Web.Helpers;
using Dapper;

namespace BarbeariaZanetti.Web.Data
{
    public class DatabaseInitializer
    {
        private readonly ConnectionFactory _connectionFactory;

        public DatabaseInitializer(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task SeedAsync()
        {
            using var connection = _connectionFactory.CreateConnection();

            await SeedPerfisAsync(connection);
            await SeedStatusAtendimentoAsync(connection);
            await SeedFormasPagamentoAsync(connection);
            await SeedServicosAsync(connection);
            await SeedAdministradorAsync(connection);
        }

        private static async Task SeedPerfisAsync(
            MySqlConnector.MySqlConnection connection)
        {
            var count = await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Perfis;");

            if (count == 0)
            {
                await connection.ExecuteAsync(@"
                    INSERT INTO Perfis (Nome)
                    VALUES
                    ('Administrador'),
                    ('Barbeiro');");
            }
        }

        private static async Task SeedStatusAtendimentoAsync(
            MySqlConnector.MySqlConnection connection)
        {
            var count = await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM StatusAtendimento;");

            if (count == 0)
            {
                await connection.ExecuteAsync(@"
                    INSERT INTO StatusAtendimento
                    (
                        Nome,
                        ClasseBootstrap,
                        ContaFinanceiro,
                        Ordem
                    )
                    VALUES
                    ('Agendado', 'warning', 0, 1),
                    ('Concluído', 'success', 1, 2),
                    ('Cancelado', 'danger', 0, 3),
                    ('Não Compareceu', 'secondary', 0, 4);");
            }
        }

        private static async Task SeedFormasPagamentoAsync(
            MySqlConnector.MySqlConnection connection)
        {
            var count = await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM FormasPagamento;");

            if (count == 0)
            {
                await connection.ExecuteAsync(@"
                    INSERT INTO FormasPagamento
                    (
                        Nome,
                        Ordem,
                        Ativo
                    )
                    VALUES
                    ('Dinheiro', 1, 1),
                    ('PIX', 2, 1),
                    ('Cartão de Débito', 3, 1),
                    ('Cartão de Crédito', 4, 1);");
            }
        }

        private static async Task SeedServicosAsync(
            MySqlConnector.MySqlConnection connection)
        {
            var count = await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Servicos;");

            if (count == 0)
            {
                await connection.ExecuteAsync(@"
                    INSERT INTO Servicos
                    (
                        Nome,
                        Descricao,
                        ValorPadrao,
                        DuracaoMinutos
                    )
                    VALUES
                    ('Corte', 'Corte de cabelo', 40.00, 50),
                    ('Barba', 'Barba completa', 30.00, 30),
                    ('Corte + Barba', 'Corte de cabelo e barba', 60.00, 80);");
            }
        }

        private static async Task SeedAdministradorAsync(
            MySqlConnector.MySqlConnection connection)
        {
            var count = await connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Usuarios WHERE Login = @Login;",
                new { Login = "admin" });

            if (count == 0)
            {
                var senhaHash = PasswordHelper.GerarHash("admin123");

                await connection.ExecuteAsync(@"
                    INSERT INTO Usuarios
                    (
                        Nome,
                        Login,
                        SenhaHash,
                        PerfilId,
                        Ativo,
                        PrimeiroAcesso
                    )
                    VALUES
                    (
                        @Nome,
                        @Login,
                        @SenhaHash,
                        @PerfilId,
                        @Ativo,
                        @PrimeiroAcesso
                    );",
                    new
                    {
                        Nome = "Administrador",
                        Login = "admin",
                        SenhaHash = senhaHash,
                        PerfilId = 1,
                        Ativo = true,
                        PrimeiroAcesso = true
                    });
            }
        }
    }
}