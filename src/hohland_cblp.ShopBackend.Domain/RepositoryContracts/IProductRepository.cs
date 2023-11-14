using  hohland_cblp.ShopBackend.Domain.Entities;

namespace hohland_cblp.ShopBackend.Domain.RepositoryContracts;


public interface IProductRepository
{
    Task<List<Product>> GetList();
    Task<bool> Insert(Product product);
    Task<bool> Update(Product product);
    Task<bool> Delete(Product product);
    Task<bool> DeleteById(long id);
    Task<Product> GetById(long id);
}