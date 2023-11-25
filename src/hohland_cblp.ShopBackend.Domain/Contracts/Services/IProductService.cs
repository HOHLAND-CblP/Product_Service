using hohland_cblp.ShopBackend.Domain.Entities;

namespace hohland_cblp.ShopBackend.Domain.Contracts.Services;

public interface IProductService
{
    Task<List<Product>> GetList(CancellationToken token);
    Task<List<Product>> GetList(List<long> ids, CancellationToken token);
    Task<Product> Get(long id, CancellationToken token);
    Task<ProductType> GetProductType(long id, CancellationToken token);
    Task<long> Create(Product product, CancellationToken token);
    Task<List<long>> Create(List<Product> product, CancellationToken token);
    Task Update(Product product, CancellationToken token);
    Task Delete(long id, CancellationToken token);
    Task Delete(List<long> ids, CancellationToken token);
}