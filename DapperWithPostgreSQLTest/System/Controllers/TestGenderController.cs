using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperWithPostgreSQL.Controllers;
using DapperWithPostgreSQL.Models;
using DapperWithPostgreSQL.Repository;
using DapperWithPostgreSQL.Test.MockData;
using DapperWithPostgreSQLTest.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DapperWithPostgreSQL.Test.System.Controllers
{
    public class TestGenderController
    {

        [Fact]
        [Trait("Category - Gender", "GetAll")]
        public async Task GetAllGenderAsync_ShouldReturn200Status()
        {
            //Arrange
            var genderService = new Mock<IGenericRepository<Gender>>();

            genderService.Setup(x => x.GetAllAsync()).ReturnsAsync(GenderMockData.GetGenderMockData());

            var systemUnderTest = new GenderController(genderService.Object);

            //Act 
            var result = await systemUnderTest.GetAllAsync();

            // Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);

        }

        [Fact]
        [Trait("Category - Gender", "GetAll")]
        public async Task GetAllGenderAsync_ShouldReturn204Status()
        {

            //Arrange
            var genderService = new Mock<IGenericRepository<Gender>>();

            genderService.Setup(x => x.GetAllAsync()).ReturnsAsync(GenderMockData.GetGenderEmptyData());

            var systemUnderTest = new GenderController(genderService.Object);

            //Act 
            var result = await systemUnderTest.GetAllAsync();

            // Assert
            result.GetType().Should().Be(typeof(NoContentResult));
            (result as NoContentResult).StatusCode.Should().Be(204);

        }
        [Fact]
        [Trait("Category", "GetById")]
        public async Task GetGenderByIdAsync_ShouldReturn200Status()
        {
            //Arrange
            var genderService = new Mock<IGenericRepository<Gender>>();

            genderService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(GenderMockData.GetGenderById());

            var systemUnderTest = new GenderController(genderService.Object);

            //Act 
            var result = await systemUnderTest.GetGenderIdAsync(1);

            // Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);

        }
        [Fact]
        [Trait("Category", "GetById")]
        public async Task GetGenderByIdAsync_ShouldReturn404Status()
        {
            //Arrange
            var genderService = new Mock<IGenericRepository<Gender>>();

            genderService.Setup(x => x.GetByIdAsync(2)).ReturnsAsync((Gender?)null);

            var systemUnderTest = new GenderController(genderService.Object);

            //Act 
            var result = await systemUnderTest.GetGenderIdAsync(2);

            // Assert
            result.GetType().Should().Be(typeof(NotFoundResult));
            (result as NotFoundResult).StatusCode.Should().Be(404);

        }

        [Fact]
        [Trait("Category", "Post")]
        public async Task AddGenderAsync_ShouldReturn201Status()
        {
            var genderService = new Mock<IGenericRepository<Gender>>();

            genderService.Setup(x => x.AddAsync(It.IsAny<Gender>())).Returns(Task.CompletedTask);

            var systemUnderTest = new GenderController(genderService.Object);

            var result = await systemUnderTest.AddGenderAsync(GenderMockData.GetGenderById());

            result.GetType().Should().Be(typeof(CreatedAtActionResult));
            (result as CreatedAtActionResult).StatusCode.Should().Be(201);

        }

        [Fact]
        [Trait("Category", "Post")]

        public async Task AddGenderAsync_ShouldReturn400Status()
        {
            var genderService = new Mock<IGenericRepository<Gender>>();

            genderService.Setup(x => x.AddAsync(It.IsAny<Gender>())).Returns(Task.CompletedTask);

            var systemUnderTest = new GenderController(genderService.Object);


            var result = await systemUnderTest.AddGenderAsync(null);
            //var result = await systemUnderTest.AddCustomerAsync(Customer as null);
            result.GetType().Should().Be(typeof(BadRequestResult));
            (result as BadRequestResult).StatusCode.Should().Be(400);


        }
        [Fact]
        [Trait("Category", "Put")]
        public async Task UpdateGenderAsync_ShouldReturn200Status()
        {
            var genderService = new Mock<IGenericRepository<Gender>>();
            genderService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(GenderMockData.GetGenderById());

            genderService.Setup(x => x.UpdateAsync(It.IsAny<Gender>())).Returns(Task.CompletedTask);

            var systemUnderTest = new GenderController(genderService.Object);

            var result = await systemUnderTest.UpdateGenderAsync(1, GenderMockData.GetGenderById());

            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }
        [Fact]
        [Trait("Category", "Put")]
        public async Task UpdateGenderAsync_ShouldReturn404Status()
        {
            var genderService = new Mock<IGenericRepository<Gender>>();

            genderService.Setup(x => x.UpdateAsync(It.IsAny<Gender>())).Returns(Task.CompletedTask);

            var systemUnderTest = new GenderController(genderService.Object);

            var result = await systemUnderTest.UpdateGenderAsync(-1, null);
            result.GetType().Should().Be(typeof(NotFoundResult));
            (result as NotFoundResult).StatusCode.Should().Be(404);
        }

        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteCustomerAsync_ShouldReturn200Status()
        {
            var genderService = new Mock<IGenericRepository<Gender>>();

            genderService.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(GenderMockData.GetGenderById());
            genderService.Setup(x => x.DeleteAsync(1)).Returns(Task.CompletedTask);

            var systemUnderTest = new GenderController(genderService.Object);

            var result = await systemUnderTest.DeletebyIdAsync(1);
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }
        [Fact]
        [Trait("Category", "Delete")]
        public async Task DeleteGenderAsync_ShouldReturn404Status()
        {
            var genderService = new Mock<IGenericRepository<Gender>>();

            genderService.Setup(x => x.DeleteAsync(1)).Returns(Task.CompletedTask);

            var systemUnderTest = new GenderController(genderService.Object);

            var result = await systemUnderTest.DeletebyIdAsync(-1);
            result.GetType().Should().Be(typeof(NotFoundResult));
            (result as NotFoundResult).StatusCode.Should().Be(404);
        }
    }
}
