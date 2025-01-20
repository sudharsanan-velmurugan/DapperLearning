using DapperWithPostgreSQL.Controllers;
using DapperWithPostgreSQL.Models;
using DapperWithPostgreSQL.Repository;
using DapperWithPostgreSQLTest.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DapperWithPostgreSQL.Test.System.Controllers
{
    public class TestCustomerController
    {

        [Fact]
        [Trait("Category","GetAll")]
        public async Task GetAllCustomerAsync_ShouldReturn200Status()
        {
            //Arrange
            var customerService = new Mock<IGenericRepository<Customer>>();

            customerService.Setup(x => x.GetAllAsync()).ReturnsAsync(CustomerMockData.GetCustomersMockData());

            var systemUnderTest = new CustomerController(customerService.Object);

            //Act 
            var result = await systemUnderTest.GetAllCustomerAsync();

            // Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);

        }

        [Fact]
        [Trait("Category", "GetAll")]
        public async Task GetAllCustomerAsync_ShouldReturn204Status()
        {
            //Arrange
            var customerService = new Mock<IGenericRepository<Customer>>();

            customerService.Setup(x => x.GetAllAsync()).ReturnsAsync(CustomerMockData.GetCustomersEmptyData());

            var systemUnderTest = new CustomerController(customerService.Object);

            //Act 
            var result = await systemUnderTest.GetAllCustomerAsync();

            // Assert
            result.GetType().Should().Be(typeof(NoContentResult));
            (result as NoContentResult).StatusCode.Should().Be(204);

        }
        [Fact]
        [Trait("Category", "GetById")]
        public async Task GetCustomerByIdAsync_ShouldReturn200Status()
        {
            //Arrange
            var customerService = new Mock<IGenericRepository<Customer>>();

            customerService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(CustomerMockData.GetCustomersById());

            var systemUnderTest = new CustomerController(customerService.Object);

            //Act 
            var result = await systemUnderTest.GetCustomerByIdAsync(1);

            // Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);

        }
        [Fact]
        [Trait("Category", "GetById")]
        public async Task GetCustomerByIdAsync_ShouldReturn404Status()
        {
            //Arrange
            var customerService = new Mock<IGenericRepository<Customer>>();

            customerService.Setup(x => x.GetByIdAsync(2)).ReturnsAsync((Customer?)null);

            var systemUnderTest = new CustomerController(customerService.Object);

            //Act 
            var result = await systemUnderTest.GetCustomerByIdAsync(2);

            // Assert
            result.GetType().Should().Be(typeof(NotFoundResult));
            (result as NotFoundResult).StatusCode.Should().Be(404);

        }

        [Fact]
        [Trait("Category", "Post")]
        public async Task AddCustomerAsync_ShouldReturn201Status()
        {
            var customerService = new Mock<IGenericRepository<Customer>>();

             customerService.Setup(x => x.AddAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);

            var systemUnderTest = new CustomerController(customerService.Object);

            var result = await systemUnderTest.AddCustomerAsync(CustomerMockData.GetCustomersById());

            result.GetType().Should().Be(typeof(CreatedAtActionResult));
            (result as CreatedAtActionResult).StatusCode.Should().Be(201);

        }

        [Fact]
        [Trait("Category","Post")]

        public async Task AddCustomerAsync_ShouldReturn400Status()
        {
            var customerService = new Mock<IGenericRepository<Customer>>();

            customerService.Setup(x=>x.AddAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);

            var systemUnderTest = new CustomerController(customerService.Object);


            var result = await systemUnderTest.AddCustomerAsync(null);
            //var result = await systemUnderTest.AddCustomerAsync(Customer as null);
            result.GetType().Should().Be(typeof(BadRequestResult));
            (result as BadRequestResult).StatusCode.Should().Be(400);

            
        }
        [Fact]
        [Trait("Category", "Put")]
        public async Task UpdateCustomerAsync_ShouldReturn200Status()
        {
            var customerService = new Mock<IGenericRepository<Customer>>();
            customerService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(CustomerMockData.GetCustomersById());

            customerService.Setup(x => x.UpdateAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);

            var systemUnderTest = new CustomerController(customerService.Object);

            var result = await systemUnderTest.UpdateCustomerAsync(1,CustomerMockData.GetCustomersById());

            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }
        [Fact]
        [Trait("Category", "Put")]
        public async Task UpdateCustomerAsync_ShouldReturn404Status()
        {
            var customerService = new Mock<IGenericRepository<Customer>>();

            customerService.Setup(x=>x.UpdateAsync(It.IsAny<Customer>())).Returns(Task.CompletedTask);

            var systemUnderTest = new CustomerController(customerService.Object);

            var result = await systemUnderTest.UpdateCustomerAsync(-1,null);
            result.GetType().Should().Be(typeof(NotFoundResult));
            (result as NotFoundResult).StatusCode.Should().Be(404);
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteCustomerAsync_ShouldReturn200Status()
        {
            var customerService = new Mock<IGenericRepository<Customer>>();

            customerService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(CustomerMockData.GetCustomersById()); 
            customerService.Setup(x => x.DeleteAsync(1)).Returns(Task.CompletedTask);

            var systemUnderTest = new CustomerController(customerService.Object);

            var result = await systemUnderTest.DeleteCustomerByIdAsync(1);
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }
        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteCustomerAsync_ShouldReturn404Status()
        {
            var customerService = new Mock<IGenericRepository<Customer>>();

            customerService.Setup(x => x.DeleteAsync(1)).Returns(Task.CompletedTask);

            var systemUnderTest = new CustomerController(customerService.Object);

            var result = await systemUnderTest.DeleteCustomerByIdAsync(-1);
            result.GetType().Should().Be(typeof(NotFoundResult));
            (result as NotFoundResult).StatusCode.Should().Be(404);
        }
    }
}
