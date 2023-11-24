using hohland_cblp.ShopBackend.Domain.Entities;
using hohland_cblp.ShopBackend.Domain.RepositoryContracts;
using Ozon.Route256.Kafka.OrderEventConsumer.Infrastructure;


namespace hohland_cblp.ShopBackend.Infrastructure.Repositories;

public class ProductRepository : PgRepository, IProductRepository
{
    public ProductRepository(string connectionString) : base(connectionString)
    {
    }

    public Task<List<Product>> GetList(CancellationToken token)
    {
        throw new NotImplementedException();
    }
    public Task<List<Product>> Get(List<long> ids, CancellationToken token) 
    {
        throw new NotImplementedException();
    }
    public Task<Product> GetById(long id, CancellationToken token)
    {
        throw new NotImplementedException();
    }
    public Task<List<long>> Add(List<Product> products, CancellationToken token)
    {
        throw new NotImplementedException();
    }
    public Task<long> Insert(Product product, CancellationToken token)
    {
        throw new NotImplementedException();
    }
    public Task Update(Product product, CancellationToken token)
    {
        throw new NotImplementedException();
    }
    public Task Delete(Product product, CancellationToken token)
    {
        throw new NotImplementedException();
    }
    public Task DeleteById(long id, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
