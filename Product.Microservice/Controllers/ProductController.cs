using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Microservice.Services;

namespace Product.Microservice.Controllers
{
    using Product.Microservice.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProductsServices _productsService;
        public ProductController(ProductsServices productsService) => _productsService = productsService;

        [HttpGet]
        public async Task<List<Product>> Get() => await _productsService.GetProductsAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> Get(string _id)
        {
           var product = await _productsService.GetProductsAsync(_id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product _product)
        {
            await _productsService.CreaterProduct(_product);
            return CreatedAtAction(nameof(Get), new { id = _product.Id }, _product);
        }

        [HttpPut]
        public async Task<IActionResult> Put(string _id,  Product _product)
        {
            var product = await _productsService.GetProductsAsync(_id);
            if (product == null) return NotFound();
            product.Id = _product.Id;
            await _productsService.UpdateProduct(_id, _product);
            return Ok(product);
        }

        [HttpDelete]
        public async Task<IActionResult> Del(string _id)
        {
            var product = await _productsService.GetProductsAsync(_id);
            if (product == null) return NotFound();
            await _productsService.DeleteProduct(_id);
            return NoContent();
        }

        [HttpGet("{id}/{name}")]
        public async Task<ActionResult<Product>> GetByIdName(string _id, string _name)
        {
            Console.WriteLine(_name, "_name");
            var product = await _productsService.GetProductsAsync(_id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

    }
}
