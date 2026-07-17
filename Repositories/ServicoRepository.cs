using BarbeariaZanetti.Web.Data;
using BarbeariaZanetti.Web.Models;
using BarbeariaZanetti.Web.Repositories.Interfaces;
using Dapper;

namespace BarbeariaZanetti.Web.Repositories
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly ConnectionFactory _connectionFactory;

        public ServicoRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Servico>> BuscarTodosAsync()
        {
            const string sql = @"
                SELECT 
                    Id,
                    Nome,
                    Descricao,
                    ValorPadrao,
                    DuracaoMinutos,
                    Ativo,
                    DataCriacao,
                    DataAtualizacao
                FROM Servicos
                WHERE Ativo = 1
                ORDER BY Nome;
            ";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Servico>(sql);
        }

        public async Task<Servico?> BuscarPorIdAsync(int id)
        {
            const string sql = @"
                SELECT 
                    Id,
                    Nome,
                    Descricao,
                    ValorPadrao,
                    DuracaoMinutos,
                    Ativo,
                    DataCriacao,
                    DataAtualizacao
                FROM Servicos
                WHERE Id = @Id;
            ";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<Servico>(sql, new { Id = id });
        }

        public async Task InserirAsync(Servico servico)
        {
            const string sql = @"
                INSERT INTO Servicos
                (
                    Nome,
                    Descricao,
                    ValorPadrao,
                    DuracaoMinutos,
                    Ativo,
                    DataCriacao
                )
                VALUES
                (
                    @Nome,
                    @Descricao,
                    @ValorPadrao,
                    @DuracaoMinutos,
                    @Ativo,
                    NOW()
                );
            ";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, servico);
        }

        public async Task AtualizarAsync(Servico servico)
        {
            const string sql = @"
                UPDATE Servicos
                SET
                    Nome = @Nome,
                    Descricao = @Descricao,
                    ValorPadrao = @ValorPadrao,
                    DuracaoMinutos = @DuracaoMinutos,
                    Ativo = @Ativo,
                    DataAtualizacao = NOW()
                WHERE Id = @Id;
            ";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, servico);
        }

        public async Task ExcluirAsync(int id)
        {
            const string sql = @"
                UPDATE Servicos
                SET 
                    Ativo = 0,
                    DataAtualizacao = NOW()
                WHERE Id = @Id;
            ";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}