using hohland_cblp.ShopBackend.Domain.Entities;

namespace hohland_cblp.ShopBackend.Domain.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetProductsList(CancellationToken token);
    Task<bool> CreateProduct(Product product, CancellationToken token);
    Task<bool> UpdateProductPrice(long id, float price, CancellationToken token);
    Task<bool> DeleteProduct(Product product, CancellationToken token);
    Task<bool> DeleteProductById(long id, CancellationToken token);
    Task<Product> GetProduct(long id, CancellationToken token);
    Task<ProductType> GetProductType(long id, CancellationToken token);
}
