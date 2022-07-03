using Complevo.ProductsManagement.Entities;

namespace Complevo.ProductsManagement.Common
{
    public interface IProductServices
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetProductAsync(Guid productId);
        Task<Product> GetProductAsync(string name);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid productId);
    }
}
