using hohland_cblp.ShopBackend.Domain.Entities;
using hohland_cblp.ShopBackend.Domain.RepositoryContracts;
using Ozon.Route256.Kafka.OrderEventConsumer.Infrastructure;


namespace hohland_cblp.ShopBackend.Infrastructure.Repositories;

public class ProductRepository : PgRepository, IProductRepository
{
    private readonly Dictionary<long, Product> _products;

    public ProductRepository(string connectionString) : base(connectionString)
    {
        _products = new Dictionary<long, Product>();
    }

    public async Task<List<Product>> GetList(CancellationToken token)
    {
        return _products.Values.ToList();
    }

    public async Task<bool> Insert(Product product, CancellationToken token)
    {
        if (_products.ContainsKey(product.Id))
            return false;

        return _products.TryAdd(product.Id, product);
    }

    public async Task<bool> Update(Product product, CancellationToken token)
    {
        if (!_products.ContainsKey(product.Id))
            return false;

        _products[product.Id] = product;
        return true;
    }

    public async Task<bool> Delete(Product product, CancellationToken token)
    {
        return await DeleteById(product.Id);
    }

    public async Task<bool> DeleteById(long id, CancellationToken token)
    {
        if (!_products.ContainsKey(id))
            return false;

        _products.Remove(id);
        return true;
    }

    public async Task<Product> GetById(long id, CancellationToken token)
    {
        if (!_products.ContainsKey(id))
            return null;
        return _products[id];
    }
}
