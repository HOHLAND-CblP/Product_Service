using hohland_cblp.ShopBackend.Domain.Entities;
using hohland_cblp.ShopBackend.Domain.RepositoryContracts;
using hohland_cblp.ShopBackend.Domain.Services.Interfaces;


namespace hohland_cblp.ShopBackend.Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> GetProductsList(CancellationToken token)
    {
        return await _repository.GetList(token);
    }

    public async Task CreateProduct(Product product, CancellationToken token)
    {
        return await _repository.Insert(product, token);
    }

    public async Task<bool> UpdateProductPrice(long id, float newPrice, CancellationToken token)
    {
        var product = await _repository.GetById(id);
        //product.Price = newPrice;
        return await _repository.Update(product);
    }

    public async Task<bool> DeleteProduct(Product product, CancellationToken token)
    {
        return await _repository.Delete(product);
    }

    public async Task<bool> DeleteProductById(long id, CancellationToken token)
    {
        return await _repository.DeleteById(id);
    }

    public async Task<Product> GetProduct(long id, CancellationToken token)
    {
        return await _repository.GetById(id);
    }

    public async Task<ProductType> GetProductType(long id, CancellationToken token)
    {
        var product = await _repository.GetById(id);
        return product.ProductType;
    }

    Task<bool> IProductService.CreateProduct(Product product, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}

