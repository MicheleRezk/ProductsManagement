using Complevo.ProductsManagement.Entities;
using Complevo.ProductsManagement.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Complevo.ProductsManagement.Tests.MockData
{
    internal class ProductsMockData
    {
        public static IEnumerable<Product> GetProducts(int count)
        {
            var products = new List<Product>();
            for(int i = 1; i<= count; i++)
            {
                products.Add(
                    new Product { 
                        Id = Guid.NewGuid(),
                        Name = $"Prod_{i}",
                        Description = $"Desc_{i}",
                        CreatedAt = DateTime.UtcNow,
                    });
            }
            return products;
        }
        public static IOptions<ServiceSettings> GetServiceSettingsOptions()
        {
            return Options.Create<ServiceSettings>(new ServiceSettings
            {
                ServiceName = "ProductTestDB"
            });
        }
    }
}
