using AutoMapper;
using Dapper;
using hohland_cblp.ShopBackend.Domain.Entities;
using hohland_cblp.ShopBackend.Domain.Contracts.Repositories;
using hohland_cblp.ShopBackend.Infrastructure.DbEntities;
using Ozon.Route256.Kafka.OrderEventConsumer.Infrastructure;


namespace hohland_cblp.ShopBackend.Infrastructure.Repositories;

public class ProductRepository : PgRepository, IProductRepository
{
    private readonly IMapper _mapper;
    
    public ProductRepository(IMapper mapper, string connectionString) : base(connectionString)
    {
        _mapper = mapper;
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
            conditions.Add($"id = ANY(@Ids)");
            @params.Add($"Ids", ids);
            sqlQuery += $" WHERE {string.Join(" AND ", conditions)}";
        }


        await using var connection = await GetConnection();
        
        var productsV1List =  (await connection.QueryAsync<Product>(
            new CommandDefinition(
                sqlQuery,
                @params,
                cancellationToken: token))).ToList();
        
        return _mapper.Map<List<Product>>(productsV1List);
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

        var product_v1 = await connection.ExecuteScalarAsync<Product>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Id = id
                },
                cancellationToken: token));

        return _mapper.Map<Product>(product_v1);
    }

    public async Task<List<long>> Add(List<Product> products, CancellationToken token)
    {
        var products_ = _mapper.Map<List<Product_v1>>(products);
        
        var sqlQuery =
            """
            BEGIN WORK;
            LOCK TABLE products;
            INSERT INTO products (name, price, currency, product_type)
                SELECT name, price, currency, product_type
                  --FROM UNNEST(ARRAY[{string.Join(", ",array)}])
                  FROM UNNEST(@Products)
            returning id;
            COMMIT WORK;
            """;

        await using var connection = await GetConnection();


        return (await connection.QueryAsync<long>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Products = products_
                },
                cancellationToken:token))).ToList();
    }

    public async Task<long> Add(Product product, CancellationToken token)
    {
        var sqlQuery =
            """
            BEGIN WORK;
            LOCK TABLE products;
            INSERT INTO products (name, price, currency, product_type, creation_date)
                VALUES @Name, @Price, @Currency, @ProductType, @CreationDate
            returning id;
            COMMIT WORK;
            """;

        await using var connection = await GetConnection();

        return (await connection.QueryAsync<long>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Name = product.Name,
                    Price = product.Price,
                    Currency = product.Currency,
                    ProductType = product.ProductType
                },
                cancellationToken: token))).FirstOrDefault();
    }

    public async Task Update(Product product, CancellationToken token)
    {
        var sqlQuery =
            """
            BEGIN WORK;
            LOCK TABLE products;
            UPDATE products
               SET name = @Name
                 , price = @Price
                 , currency = @Currency
                 , product_type = @ProductType
             WHERE id = @Id;
             COMMIT WORK;
            """;

        await using var connection = await GetConnection();

        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Currency = product.Currency,
                    ProductType = product.ProductType,
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
            