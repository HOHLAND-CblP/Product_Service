using Dapper;
using hohland_cblp.ShopBackend.Domain.Entities;
using hohland_cblp.ShopBackend.Domain.Contracts.Repositories;
using Ozon.Route256.Kafka.OrderEventConsumer.Infrastructure;


namespace hohland_cblp.ShopBackend.Infrastructure.Repositories;

public class ProductRepository : PgRepository, IProductRepository
{
    public ProductRepository(string connectionString) : base(connectionString)
    { }

    public async Task<List<Product>> GetList(CancellationToken token)
    {
        var sqlQuery =
            """
            SELECT id
                 , name
                 , price
                 , currency
                 , product_type
                 , creation_date
            from products;
            """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<Product>(
            new CommandDefinition(
                sqlQuery,
                cancellationToken: token))).ToList();
    }
    public async Task<List<Product>> GetList(List<long> ids, CancellationToken token) 
    {
        var sqlQuery =
            """
            SELECT id
                 , name
                 , price
                 , currency
                 , product_type
                 , creation_date
            FROM products
            """;
        
        var conditions = new List<string>();
        var @params = new DynamicParameters();

        if (ids.Any())
        {
            conditions.Add($"id = ANY(@TaskIds)");
            @params.Add($"TaskIds", ids);
            sqlQuery += $" WHERE {string.Join(" AND ", conditions)}";
        }


        await using var connection = await GetConnection();
        return (await connection.QueryAsync<Product>(
            new CommandDefinition(
                sqlQuery ,
                @params,
                cancellationToken: token))).ToList();
    }
    public async Task<Product> Get(long id, CancellationToken token)
    {
        var sqlQuery =
            """
            SELECT id
                 , name
                 , price
                 , currency
                 , product_type
                 , creation_date
            FROM products
            WHERE id = @Id;
            """;
        
        await using var connection = await GetConnection();
        return (await connection.ExecuteScalarAsync<Product>(
            new CommandDefinition(
                sqlQuery ,
                new
                {
                    Id = id
                },
                cancellationToken: token)));
    }
    public async Task<List<long>> Add(List<Product> products, CancellationToken token)
    {
        var sqlQuery =
            """
            LOCK TABLE sellers_accounting;
            INSERT INTO products (name, price, currency, product_type, creation_date)
                SELECT name, price, currency, product_type, creation_date
                  FROM UNNEST(@Products)
            returning id;
            """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<long>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Products = products
                },
                cancellationToken: token))).ToList();
    }
    public async Task<long> Add(Product product, CancellationToken token)
    {
        var sqlQuery =
            """
            LOCK TABLE sellers_accounting;
            INSERT INTO products (name, price, currency, product_type, creation_date)
                SELECT name, price, currency, product_type, creation_date
                  FROM Product
            returning id;
            """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<long>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Product = product
                },
                cancellationToken: token))).FirstOrDefault();
    }
    public async Task Update(Product product, CancellationToken token)
    {
        var sqlQuery =
            """
            
            """;
    }
    
    public async Task Delete(long id, CancellationToken token)
    {
        throw new NotImplementedException();
    }
    
    public async Task Delete(List<long> ids, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
