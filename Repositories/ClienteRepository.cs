using BarbeariaZanetti.Web.Data;
using BarbeariaZanetti.Web.Models;
using BarbeariaZanetti.Web.Repositories.Interfaces;
using Dapper;

namespace BarbeariaZanetti.Web.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ConnectionFactory _connectionFactory;

        public ClienteRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Cliente>> BuscarTodosAsync()
        {
            const string sql = @"
                SELECT
                    Id,
                    Nome,
                    Telefone,
                    Observacao,
                    Ativo,
                    DataCriacao,
                    DataAtualizacao
                FROM Clientes
                WHERE Ativo = 1
                ORDER BY Nome;
            ";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Cliente>(sql);
        }

        public async Task<int> CriarAsync(Cliente cliente)
        {
            const string sql = @"
                INSERT INTO Clientes
                (
                    Nome,
                    Telefone,
                    Observacao,
                    Ativo,
                    DataCriacao
                )
                VALUES
                (
                    @Nome,
                    @Telefone,
                    @Observacao,
                    @Ativo,
                    NOW()
                );

                SELECT LAST_INSERT_ID();
            ";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteScalarAsync<int>(sql, cliente);
        }
    }
}