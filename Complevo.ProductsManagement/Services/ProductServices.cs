using Complevo.ProductsManagement.Common;
using Complevo.ProductsManagement.Entities;

namespace Complevo.ProductsManagement.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository<Product> _productsRepo;

        public ProductServices(IRepository<Product> productsRepo)
        {
            this._productsRepo = productsRepo;
        }
        public async Task CreateProductAsync(Product product)
        {
            await _productsRepo.CreateAsync(product);
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            await _productsRepo.RemoveAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productsRepo.GetAllAsync();
        }

        public async Task<Product> GetProductAsync(Guid productId)
        {
            return await _productsRepo.GetAsync(productId);
        }
        public async Task<Product> GetProductAsync(string name)
        {
            return await _productsRepo.GetAsync(p => p.Name.ToLower() == name.ToLower());
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productsRepo.UpdateAsync(product);
        }
    }
}
