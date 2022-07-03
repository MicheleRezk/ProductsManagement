using Complevo.ProductsManagement.Dtos;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Complevo.ProductsManagement.IntegrationTests
{
    public class ProductsControllerTests : IntegrationTest
    {

        [Fact]
        public async void PostAsync_ShouldReturnCreatedStatus()
        {
            //Arrange
            var rand = _random.Next(1, int.MaxValue);
            var createProductDto = new CreateProductDto($"Product_{rand}", $"Desc_{rand}");

            //Act
            var response = await _testClient.PostAsJsonAsync(ApiRoutes.Products.Post, createProductDto);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var productDto = _serializerService.Deserialize<ProductDto>(content);
            Assert.NotNull(productDto);
            productDto.Name.Should().Be(createProductDto.Name);

            //Cleanup
            await DeleteProduct(productDto.Id);
        }

        [Fact]
        public async void GetByIdAsync_WithIdNotExists_ShouldReturnNotFoundStatus()
        {
            //Arrange
            var url = ApiRoutes.Products.GetById.Replace("{productId}", Guid.NewGuid().ToString());

            //Act
            var response = await _testClient.GetAsync(url);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void GetByIdAsync_WithIdExists_ShouldReturnOkStatus()
        {
            //Arrange
            var product = await CreateProductForTesting();
            var url = ApiRoutes.Products.GetById.Replace("{productId}", product.Id.ToString());

            //Act
            var response = await _testClient.GetAsync(url);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var productDto = _serializerService.Deserialize<ProductDto>(content);
            Assert.NotNull(productDto);
            productDto.Name.Should().Be(product.Name);

            //Cleanup
            await DeleteProduct(product.Id);
        }

        [Fact]
        public async void PutAsync_WithIdNotExists_ShouldReturnNotFoundStatus()
        {
            //Arrange
            var url = ApiRoutes.Products.PutById.Replace("{productId}", Guid.NewGuid().ToString());

            //Act
            var response = await _testClient.PutAsJsonAsync(url, new UpdateProductDto("Name","Desc"));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


    }
}
