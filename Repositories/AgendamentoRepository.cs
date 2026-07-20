using BarbeariaZanetti.Web.Data;
using BarbeariaZanetti.Web.Models;
using BarbeariaZanetti.Web.Repositories.Interfaces;
using Dapper;

namespace BarbeariaZanetti.Web.Repositories
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly ConnectionFactory _connectionFactory;

        public AgendamentoRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Agendamento>> BuscarPorDataAsync(DateTime data)
        {
            const string sql = @"
                SELECT
                    Id,
                    ClienteId,
                    BarbeiroId,
                    ServicoId,
                    StatusId,
                    FormaPagamentoId,
                    DataHoraInicio,
                    DataHoraFim,
                    ValorCobrado,
                    Observacao,
                    DataCriacao,
                    DataAtualizacao
                FROM Agendamentos
                WHERE DATE(DataHoraInicio) = DATE(@Data)
                ORDER BY DataHoraInicio;
            ";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Agendamento>(
                sql,
                new { Data = data });
        }

        public async Task<IEnumerable<Agendamento>> BuscarPorBarbeiroEDataAsync(
            int barbeiroId,
            DateTime data)
        {
            const string sql = @"
                SELECT
                    Id,
                    ClienteId,
                    BarbeiroId,
                    ServicoId,
                    StatusId,
                    FormaPagamentoId,
                    DataHoraInicio,
                    DataHoraFim,
                    ValorCobrado,
                    Observacao,
                    DataCriacao,
                    DataAtualizacao
                FROM Agendamentos
                WHERE BarbeiroId = @BarbeiroId
                  AND DATE(DataHoraInicio) = DATE(@Data)
                ORDER BY DataHoraInicio;
            ";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Agendamento>(
                sql,
                new
                {
                    BarbeiroId = barbeiroId,
                    Data = data
                });
        }

        public async Task<IEnumerable<Agendamento>> BuscarPorMesAsync(
            int ano,
            int mes)
        {
            var inicioMes = new DateTime(ano, mes, 1);
            var inicioProximoMes = inicioMes.AddMonths(1);

            const string sql = @"
            SELECT
                Id,
                ClienteId,
                BarbeiroId,
                ServicoId,
                StatusId,
                FormaPagamentoId,
                DataHoraInicio,
                DataHoraFim,
                ValorCobrado,
                Observacao,
                DataCriacao,
                DataAtualizacao
            FROM Agendamentos
            WHERE DataHoraInicio >= @InicioMes
              AND DataHoraInicio < @InicioProximoMes
            ORDER BY DataHoraInicio;
            ";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Agendamento>(
            sql,
            new
            {
                InicioMes = inicioMes,
                InicioProximoMes = inicioProximoMes
            });
        }

        public async Task<Agendamento?> BuscarPorIdAsync(int id)
        {
            const string sql = @"
                SELECT
                    Id,
                    ClienteId,
                    BarbeiroId,
                    ServicoId,
                    StatusId,
                    FormaPagamentoId,
                    DataHoraInicio,
                    DataHoraFim,
                    ValorCobrado,
                    Observacao,
                    DataCriacao,
                    DataAtualizacao
                FROM Agendamentos
                WHERE Id = @Id;
            ";

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<Agendamento>(
                sql,
                new { Id = id });
        }

        public async Task CriarAsync(Agendamento agendamento)
        {
            const string sql = @"
                INSERT INTO Agendamentos
                (
                    ClienteId,
                    BarbeiroId,
                    ServicoId,
                    StatusId,
                    FormaPagamentoId,
                    DataHoraInicio,
                    DataHoraFim,
                    ValorCobrado,
                    Observacao,
                    DataCriacao
                )
                VALUES
                (
                    @ClienteId,
                    @BarbeiroId,
                    @ServicoId,
                    @StatusId,
                    @FormaPagamentoId,
                    @DataHoraInicio,
                    @DataHoraFim,
                    @ValorCobrado,
                    @Observacao,
                    NOW()
                );
            ";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, agendamento);
        }

        public async Task AtualizarAsync(Agendamento agendamento)
        {
            const string sql = @"
                UPDATE Agendamentos
                SET
                    ClienteId = @ClienteId,
                    BarbeiroId = @BarbeiroId,
                    ServicoId = @ServicoId,
                    StatusId = @StatusId,
                    FormaPagamentoId = @FormaPagamentoId,
                    DataHoraInicio = @DataHoraInicio,
                    DataHoraFim = @DataHoraFim,
                    ValorCobrado = @ValorCobrado,
                    Observacao = @Observacao,
                    DataAtualizacao = NOW()
                WHERE Id = @Id;
            ";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, agendamento);
        }

        public async Task AtualizarHoraFimAsync(
            int agendamentoId,
            DateTime dataHoraFim)
        {
            const string sql = @"
                UPDATE Agendamentos
                SET
                    DataHoraFim = @DataHoraFim,
                    DataAtualizacao = NOW()
                WHERE Id = @Id;
            ";

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(
                sql,
                new
                {
                    Id = agendamentoId,
                    DataHoraFim = dataHoraFim
                });
        }
    }
}