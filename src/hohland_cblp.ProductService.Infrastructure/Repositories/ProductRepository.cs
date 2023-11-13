using hohland_cblp.ProductService.Domain.Entities;
using hohland_cblp.ProductService.Domain.RepositoryContracts;


namespace hohland_cblp.ProductService.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly Dictionary<long, Product> _products;

    public ProductRepository()
    {
        _products = new Dictionary<long, Product>();
    }

    public async Task<List<Product>> GetList()
    {
        return _products.Values.ToList();
    }

    public async Task<bool> Insert(Product product)
    {
        if (_products.ContainsKey(product.Id))
            return false;

        return _products.TryAdd(product.Id, product);
    }

    public async Task<bool> Update(Product product)
    {
        if (!_products.ContainsKey(product.Id))
            return false;

        _products[product.Id] = product;
        return true;
    }

    public async Task<bool> Delete(Product product)
    {
        return await DeleteById(product.Id);
    }

    public async Task<bool> DeleteById(long id)
    {
        if (!_products.ContainsKey(id))
            return false;

        _products.Remove(id);
        return true;
    }

    public async Task<Product> GetById(long id)
    {
        if (!_products.ContainsKey(id))
            return null;
        return _products[id];
    }
}
