﻿using DapperWithPostgreSQL.Controllers;
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
    }
}
