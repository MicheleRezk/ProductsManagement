using Complevo.ProductsManagement.Common;
using Complevo.ProductsManagement.Dtos;
using Complevo.ProductsManagement.Entities;
using Complevo.ProductsManagement.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Complevo.ProductsManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly ServiceSettings _serviceSettings;

        public ProductsController(
            IProductServices productServices,
            IOptions<ServiceSettings> serviceSettings)
        {
            this._productServices = productServices;
            this._serviceSettings = serviceSettings.Value;
        }

        // GET /products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllAsync()
        {
            Console.WriteLine("--> Getting all products....");
            var products = await _productServices.GetAll();
            var response = products.Select(p => p.AsDto()).ToList();

            return Ok(response);
        }

        // GET /products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(Guid id)
        {
            Console.WriteLine("--> Getting Product By Id....");
            var product = await _productServices.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product.AsDto());
        }

        // POST /products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostAsync(CreateProductDto createProductDto)
        {
            Console.WriteLine("--> Creating a new product....");
            var product = await _productServices.GetProductAsync(createProductDto.Name);
            if(product != null) //this means there is product with same name
            {
                return Conflict($"Can't create the product, as there is a product exists with same name: {createProductDto.Name}");
            }
            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                CreatedAt = DateTimeOffset.UtcNow,
            };

            await _productServices.CreateProductAsync(newProduct);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newProduct.Id }, newProduct.AsDto());
        }

        // PUT /products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateProductDto updateProductDto)
        {
            var existingProduct = await _productServices.GetProductAsync(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = updateProductDto.Name;
            existingProduct.Description = updateProductDto.Description;
            existingProduct.UpdatedAt = DateTimeOffset.UtcNow;

            await _productServices.UpdateProductAsync(existingProduct);

            return NoContent();
        }

        // DELETE /products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var product = await _productServices.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productServices.DeleteProductAsync(product.Id);

            return NoContent();
        }

    }
}
