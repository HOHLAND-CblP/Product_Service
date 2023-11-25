using  hohland_cblp.ShopBackend.Domain.Entities;

namespace hohland_cblp.ShopBackend.Domain.Contracts.Repositories;


public interface IProductRepository
{
    Task<List<Product>> GetList(CancellationToken token);
    Task<List<Product>> GetList(List<long> ids, CancellationToken token);
    Task<Product> Get(long id, CancellationToken token);
    Task<List<long>> Add(List<Product> products, CancellationToken token);
    Task<long> Add(Product product, CancellationToken token);
    Task Update(Product product, CancellationToken token);
    Task Delete(long id, CancellationToken token);
    Task Delete(List<long> ids, CancellationToken token);
}