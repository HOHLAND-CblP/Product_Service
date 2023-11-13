using hohland_cblp.ProductService.Domain.Entities;

namespace hohland_cblp.ProductService.Domain.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetProductsList();
    Task<bool> CreateProduct(Product product);
    Task<bool> UpdateProductPrice(long id, float price);
    Task<bool> DeleteProduct(Product product);
    Task<bool> DeleteProductById(long id);
    Task<Product> GetProduct(long id);
    Task<ProductType> GetProductType(long id);
}
