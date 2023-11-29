using Microsoft.AspNetCore.Mvc;
using hohland_cblp.ShopBackend.Domain.Contracts.Services;
using hohland_cblp.ShopBackend.Domain.Entities;

namespace hohland_cblp.ShopBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Product>>> GetList(CancellationToken token)
        {
            return await _service.GetList(token);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<List<Product>>> GetListByIds([FromBody] List<long> ids, CancellationToken token)
        {
            return await _service.GetList(ids, token);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult<Product>> Get(long id, CancellationToken token)
        {
            return await _service.Get(id, token);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<ProductType>> GetProductType(long id, CancellationToken token)
        {
            return await _service.GetProductType(id, token);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<long>> Create([FromBody] Product product, CancellationToken token)
        {
            return await _service.Create(product, token);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<List<long>>> CreateSeveral([FromBody] List<Product> products, CancellationToken token)
        {
            return await _service.Create(products, token);
        }

        
        
        [HttpPost("[action]")]
        public async Task Update([FromBody] Product product, CancellationToken token)
        {
            await _service.Update(product, token);
        }
        
        [HttpDelete("[action]")]
        public async Task Delete(long id, CancellationToken token)
        {
            await _service.Delete(id, token);
        }
        
        [HttpDelete("[action]")]
        public async Task DeleteSeveral(List<long> ids, CancellationToken token)
        {
            await _service.Delete(ids, token);
        }
    }
}