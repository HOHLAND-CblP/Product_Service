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
        throw new NotImplementedException();
    }
    public async Task<List<Product>> GetList(List<long> ids, CancellationToken token) 
    {
        throw new NotImplementedException();
    }
    public async Task<Product> Get(long id, CancellationToken token)
    {
        throw new NotImplementedException();
    }
    public async Task<List<long>> Add(List<Product> products, CancellationToken token)
    {
        throw new NotImplementedException();
    }
    public async Task<long> Add(Product product, CancellationToken token)
    {
        throw new NotImplementedException();
    }
    public async Task Update(Product product, CancellationToken token)
    {
        throw new NotImplementedException();
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
