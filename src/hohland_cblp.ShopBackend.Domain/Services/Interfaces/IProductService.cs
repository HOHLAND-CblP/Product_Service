using hohland_cblp.ShopBackend.Domain.Entities;

namespace hohland_cblp.ShopBackend.Domain.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetList(CancellationToken token);
    Task<List<Product>> GetList(List<long> ids, CancellationToken token);
    Task<long> CreateProduct(Product product, CancellationToken token);
    Task<List<long>> CreateProducts(List<Product> product, CancellationToken token);
    Task UpdateProduct(Product product, CancellationToken token);
    Task DeleteProduct(Product product, CancellationToken token);
    Task DeleteProductById(long id, CancellationToken token);
    Task<Product> GetProduct(long id, CancellationToken token);
    Task<ProductType> GetProductType(long id, CancellationToken token);
}
