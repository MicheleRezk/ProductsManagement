using Complevo.ProductsManagement.Common;
using Complevo.ProductsManagement.Controllers;
using Complevo.ProductsManagement.Dtos;
using Complevo.ProductsManagement.Entities;
using Complevo.ProductsManagement.Services;
using Complevo.ProductsManagement.Tests.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Complevo.ProductsManagement.Tests.Controllers
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            //Arrange
            var mockProducts = ProductsMockData.GetProducts(2) as IReadOnlyCollection<Product>;
            var serviceSettings = ProductsMockData.GetServiceSettingsOptions();
            var repo = new Mock<IRepository<Product>>();
            repo.Setup(_ => _.GetAllAsync()).ReturnsAsync(mockProducts);
            var productServices = new ProductServices(repo.Object);
            var sut = new ProductsController(productServices, serviceSettings);

            //Act
            var actionResult = await sut.GetAllAsync();

            //Assert
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            var response = result.Value as IEnumerable<ProductDto>;
            response.Should().NotBeNull();
            response.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetByIdAsync_NotExistingId_ShouldReturnNotFound()
        {
            //Arrange
            var mockProducts = ProductsMockData.GetProducts(2) as IReadOnlyCollection<Product>;
            var serviceSettings = ProductsMockData.GetServiceSettingsOptions();
            var repo = new Mock<IRepository<Product>>();
            repo.Setup(_ => _.GetAsync(It.IsAny<Guid>())).ReturnsAsync(() => { return null; });
            var productServices = new ProductServices(repo.Object);
            var sut = new ProductsController(productServices, serviceSettings);

            //Act
            var actionResult = await sut.GetByIdAsync(Guid.NewGuid());

            //Assert
            var result = actionResult.Result as NotFoundResult;
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task PostAsync_ShouldReturn201Status()
        {
            //Arrange
            var mockProducts = ProductsMockData.GetProducts(2) as IReadOnlyCollection<Product>;
            var serviceSettings = ProductsMockData.GetServiceSettingsOptions();
            var repo = new Mock<IRepository<Product>>();
            repo.Setup(_ => _.CreateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            var productServices = new ProductServices(repo.Object);
            var sut = new ProductsController(productServices, serviceSettings);

            //Act
            var actionResult = await sut.PostAsync(new CreateProductDto("Playstation 5", "Desc"));

            //Assert
            var result = actionResult.Result as CreatedAtActionResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(201);
            var response = result.Value as ProductDto;
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task PostAsync_WithNameExists_ShouldReturn409ConflictStatus()
        {
            //Arrange
            var mockProducts = ProductsMockData.GetProducts(2) as IReadOnlyCollection<Product>;
            var serviceSettings = ProductsMockData.GetServiceSettingsOptions();
            var repo = new Mock<IRepository<Product>>();
            repo.Setup(_ => _.GetAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(mockProducts.FirstOrDefault());
            repo.Setup(_ => _.CreateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            var productServices = new ProductServices(repo.Object);
            var sut = new ProductsController(productServices, serviceSettings);

            //Act
            var nameAlreadyExists = mockProducts.FirstOrDefault().Name;
            var actionResult = await sut.PostAsync(new CreateProductDto(nameAlreadyExists, "Desc"));

            //Assert
            var result = actionResult.Result as ConflictObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(409);
        }
    }
}
