using hohland_cblp.ShopBackend.Domain.Entities;
using hohland_cblp.ShopBackend.Domain.RepositoryContracts;
using  hohland_cblp.ShopBackend.Domain.Services.Interfaces;


namespace hohland_cblp.ShopBackend.Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> GetProductsList()
    {
        return await _repository.GetList();
    }

    public async Task<bool> CreateProduct(Product product)
    {
        return await _repository.Insert(product);
    }

    public async Task<bool> UpdateProductPrice(long id, float newPrice)
    {
        var product = await _repository.GetById(id);
        product.Price = newPrice;
        return await _repository.Update(product);
    }

    public async Task<bool> DeleteProduct(Product product)
    {
        return await _repository.Delete(product);
    }

    public async Task<bool> DeleteProductById(long id)
    {
        return await _repository.DeleteById(id);
    }

    public async Task<Product> GetProduct(long id)
    {
        return await _repository.GetById(id);
    }

    public async Task<ProductType> GetProductType(long id)
    {
        var product = await _repository.GetById(id);
        return product.ProductType;
    }
}

