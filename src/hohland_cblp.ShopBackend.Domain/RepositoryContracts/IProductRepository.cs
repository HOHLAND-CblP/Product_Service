using  hohland_cblp.ShopBackend.Domain.Entities;

namespace hohland_cblp.ShopBackend.Domain.RepositoryContracts;


public interface IProductRepository
{
    Task<List<Product>> GetList(CancellationToken token);
    Task<List<Product>> Get(List<long> ids, CancellationToken token);
    Task<Product> GetById(long id, CancellationToken token);
    Task<List<long>> Add(List<Product> products, CancellationToken token);
    Task<long> Insert(Product product, CancellationToken token);
    Task Update(Product product, CancellationToken token);
    Task Delete(Product product, CancellationToken token);
    Task DeleteById(long id, CancellationToken token);
}