using System.Threading.Tasks;
using System.Transactions;
using Npgsql;

namespace Ozon.Route256.Kafka.OrderEventConsumer.Infrastructure;

public abstract class PgRepository
{
    private readonly string _connectionString;

    protected PgRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected async Task<NpgsqlConnection> GetConnection()
    {
        if (Transaction.Current is not null &&
            Transaction.Current.TransactionInformation.Status is TransactionStatus.Aborted)
        {
            throw new TransactionAbortedException("Transaction was aborted (probably by user cancellation request)");
        }

        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        return connection;
    }
}