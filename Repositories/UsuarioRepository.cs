using BarbeariaZanetti.Web.Data;
using BarbeariaZanetti.Web.Models;
using BarbeariaZanetti.Web.Repositories.Interfaces;
using Dapper;

namespace BarbeariaZanetti.Web.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConnectionFactory _connectionFactory;

        public UsuarioRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Usuario?> BuscarPorLoginAsync(string login)
        {
            const string sql = @"
                SELECT
                    Id,
                    Nome,
                    Login,
                    SenhaHash,
                    PerfilId,
                    Ativo,
                    DataCriacao,
                    DataAtualizacao
                FROM Usuarios
                WHERE Login = @Login
                AND Ativo = 1;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<Usuario>(
                sql,
                new { Login = login });
        }

        public async Task<IEnumerable<Usuario>> BuscarBarbeirosAsync()
        {
            const string sql = @"
                SELECT
                    Id,
                    Nome,
                    Login,
                    SenhaHash,
                    PerfilId,
                    Ativo,
                    DataCriacao,
                    DataAtualizacao
                FROM Usuarios
                WHERE PerfilId = 2
                  AND Ativo = 1
                ORDER BY Nome;";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Usuario>(sql);
        }
    }
}