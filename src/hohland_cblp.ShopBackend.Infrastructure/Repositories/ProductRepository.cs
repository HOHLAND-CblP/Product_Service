using Dapper;
using hohland_cblp.ShopBackend.Domain.Entities;
using hohland_cblp.ShopBackend.Domain.Contracts.Repositories;
using Ozon.Route256.Kafka.OrderEventConsumer.Infrastructure;


namespace hohland_cblp.ShopBackend.Infrastructure.Repositories;

public class ProductRepository : PgRepository, IProductRepository
{
    public ProductRepository(string connectionString) : base(connectionString)
    {
    }

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
                sqlQuery,
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
                sqlQuery,
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
            INSERT INTO products (id, name, price, currency, product_type, creation_date)
                VALUES @{nameof(Product.Id)}
                     , @{nameof(Prodcut.Name)}
                     , @{nameof(Prodcut.Price)}
                     , @{nameof(Prodcut.Currency)}
                     , @{nameof(Prodcut.ProductType)}
                     , @{nameof(Prodcut.CreationDate)}
            returning id;
            """;

        await using var connection = await GetConnection();

        return (connection.Query<long>(sqlQuery, products)).ToList();
    }

    public async Task<long> Add(Product product, CancellationToken token)
    {
        var sqlQuery =
            """
            INSERT INTO products (id, name, price, currency, product_type, creation_date)
                VALUES (@Id, @Name, @Price, @Currency, @ProductType, @CreationDate)
            returning id;
            """;

        await using var connection = await GetConnection();

        return (await connection.QueryAsync<long>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Currency = product.Currency,
                    ProductType = product.ProductType,
                    CreationDate = product.CreationDate
                },
                cancellationToken: token))).FirstOrDefault();
    }

    public async Task Update(Product product, CancellationToken token)
    {
        var sqlQuery =
            """
            LOCK TABLE products;
            UPDATE products
               SET name = @Name
                 , price = @Price
                 , currency = @Currency
                 , product_type = @ProductType
             WHERE id = @Id;
            """;

        await using var connection = await GetConnection();

        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Name = product.Name,
                    Price = product.Price,
                    Currency = product.Currency,
                    ProductType = product.ProductType,
                    Id = product.Id
                },
                cancellationToken: token));
    }

    public async Task Delete(long id, CancellationToken token)
    {
        var sqlQuery =
            """
            LOCK TABLE products;
            DELETE FROM products
             WHERE id = @Id;
            """;
        
        await using var connection = await GetConnection();
        
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Id = id
                },
                cancellationToken: token));
    }

    public async Task Delete(List<long> ids, CancellationToken token)
    {
        var sqlQuery =
            """
            LOCK TABLE products;
            DELETE FROM products
             WHERE id = ANY(@Ids)
            """;
        
        await using var connection = await GetConnection();
        
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Ids = ids
                },
                cancellationToken: token));
    }
}
            