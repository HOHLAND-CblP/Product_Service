using Microsoft.AspNetCore.Mvc;
using hohland_cblp.ProductService.Domain.Services.Interfaces;
using hohland_cblp.ProductService.Domain.Entities;

namespace hohland_cblp.ProductService.Controllers
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
        public async Task<ActionResult<List<Product>>> GetProductList()
        {
            return await _service.GetProductsList();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<bool>> CreateProduct([FromBody] Product product)
        {
            return await _service.CreateProduct(product);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Product>> GetProductById(long id)
        {
            return await _service.GetProduct(id);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<bool>> UpdateProductPrice(long id, float newPrice)
        {
            return await _service.UpdateProductPrice(id, newPrice);
        }
    }
}