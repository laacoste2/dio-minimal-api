using Microsoft.EntityFrameworkCore;
using Products.Context;
using Products.Controllers;
using Products.Models;
using Products.Services;
using Products.DTOs;
using Moq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Services
{
    public class ServicesTests
    {
        private ProductsContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ProductsContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            return new ProductsContext(options);
        }

        [Fact]
        public void Product_Fields_Should_Be_The_Same_As_Informed()
        {
            var entity = new Product("teste", "teste", 00.00m, "CZ");

            Assert.Equal("teste", entity.Name);
            Assert.Equal("teste", entity.Description);
            Assert.Equal(00.00m, entity.Price);
            Assert.Equal("CZ", entity.Nationality);
        }

        [Fact]
        public async void PostProductServiceAsync_Should_Return_Product_Object_Type()
        {
            var testContext = GetInMemoryContext();
            var testService = new ProductsManagerService(testContext);
            var entity = new ProductDTO("teste", "teste", 00.00m, "BR");

            var addProduct =  await testService.PostProductServiceAsync(entity);

            Assert.IsType<Product>(addProduct);
        }

        [Fact]
        public void Product_With_Missing_Required_Fields_Should_Be_Invalid()
        {
            var product = new Product();

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            Assert.False(isValid);
            Assert.NotEmpty(validationResults);
        }

        [Fact]
        public async Task PostProductAsync_Should_Create_Product_And_Return_ActionResult()
        {
            var mockService = new Mock<IProductsManagerService>();
            var controller = new ProductsManagerController(mockService.Object);
            var newProduct = new Product("Teste", "Teste", 00.00m, "BR" );
            var newProductDTO = new ProductDTO(newProduct.Name, newProduct.Description, newProduct.Price, newProduct.Nationality);
            mockService.Setup(s => s.PostProductServiceAsync(It.IsAny<ProductDTO>())).ReturnsAsync(newProduct);

            var result = await controller.PostProductAsync(newProductDTO);

            Assert.IsType<ActionResult<Product>>(result);
        }
    }
}