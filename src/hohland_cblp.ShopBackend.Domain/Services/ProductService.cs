using hohland_cblp.ShopBackend.Domain.Entities;
using hohland_cblp.ShopBackend.Domain.Contracts.Repositories;
using hohland_cblp.ShopBackend.Domain.Contracts.Services;


namespace hohland_cblp.ShopBackend.Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }


    public async Task<List<Product>> GetList(CancellationToken token)
    {
        return await _repository.GetList(token);
    }

    public async Task<List<Product>> GetList(List<long> ids, CancellationToken token)
    {
        return await _repository.GetList(ids, token);
    }

    public async Task<Product> Get(long id, CancellationToken token)
    {
        return await _repository.Get(id, token);
    }

    public Task<ProductType> GetProductType(long id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<long> Create(Product product, CancellationToken token)
    {
        return await _repository.Add(product, token);
    }

    public async Task<List<long>> Create(List<Product> products, CancellationToken token)
    {
        return await _repository.Add(products, token);
    }

    public async Task Update(Product product, CancellationToken token)
    {
        await _repository.Update(product, token);
    }

    public async Task Delete(long id, CancellationToken token)
    {
        await _repository.Delete(id, token);
    }
    
    public async Task Delete(List<long> ids, CancellationToken token)
    {
        await _repository.Delete(ids, token);
    }
}

