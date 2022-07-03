using Complevo.ProductsManagement.Common;
using Complevo.ProductsManagement.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Complevo.ProductsManagement.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient _testClient;
        protected readonly Random _random = new Random();
        protected readonly IProductServices _productServices;
        protected readonly ISerializerService _serializerService;   
        protected IntegrationTest()
        {
            var appFactory = new TestingWebAppFactory<Program>();
            _testClient = appFactory.CreateDefaultClient();
            this._serializerService = appFactory.Server.Services.GetService<ISerializerService>();
            this._productServices = appFactory.Server.Services.GetService<IProductServices>();
        }
        protected async Task DeleteProduct(Guid productId)
        {
           await _productServices.DeleteProductAsync(productId);
        }
        protected async Task<Product> CreateProductForTesting()
        {
            var r = _random.Next(1, int.MaxValue);
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = $"Product_{r}",
                Description = $"Desc_{r}",
                CreatedAt = DateTime.UtcNow,
            };
            await _productServices.CreateProductAsync(product);
            return product;
        }
    }
}
