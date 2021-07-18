using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Wx.Exercises.Api.Controllers;
using Wx.Exercises.Application.Common.Enums;
using Wx.Exercises.Application.Exercise1.Models;
using Wx.Exercises.Application.Exercise1.Queries.GetUser;
using Wx.Exercises.Application.Exercise2.Models;
using Wx.Exercises.Application.Exercise3.Commands.CalculateTrolley;
using Xunit;

namespace Wx.Exercises.Tests.Controllers
{
    public class ExercisesControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<ExercisesController>> _mockLogger;
        private readonly Fixture specimens = new Fixture();
        private readonly ExercisesController _sut;

        public ExercisesControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<ExercisesController>>();
            _sut = new ExercisesController(_mockLogger.Object, _mockMediator.Object);
        }

        [Fact]
        public async Task GetUser_Should_Return_Ok_WithTypeOf_UserModel()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(specimens.Create<UserModel>());

            // Act
            var response = await _sut.GetUser();

            // Assert
            response.ShouldBeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)response;

            okResult.StatusCode.ShouldBe((int)HttpStatusCode.OK);
            okResult.Value.ShouldBeOfType<UserModel>();
            okResult.Value.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(SortOption.Low)]
        [InlineData(SortOption.High)]
        [InlineData(SortOption.Ascending)]
        [InlineData(SortOption.Descending)]
        [InlineData(SortOption.Recommended)]
        public async Task GetProducts_Should_Return_Ok_WithTypeOf_ListOfProductModel(SortOption sortOption)
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(specimens.CreateMany<ProductModel>().ToList());

            // Act
            var response = await _sut.GetProducts(sortOption);

            // Assert
            response.ShouldBeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)response;

            okResult.StatusCode.ShouldBe((int)HttpStatusCode.OK);
            okResult.Value.ShouldBeOfType<List<ProductModel>>();
            okResult.Value.ShouldNotBeNull();
        }


        [Fact]
        public async Task CalculateTrolley_Should_Return_Ok_WithTypeOf_Doublel()
        {
            // Arrange
            _mockMediator.Setup(x => x.Send(It.IsAny<CalculateTrolleyCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(1.00);

            // Act
            var response = await _sut.CalculateTrolley(specimens.Create<CalculateTrolleyCommand>());

            // Assert
            response.ShouldBeOfType<OkObjectResult>();

            var okResult = (OkObjectResult)response;

            okResult.StatusCode.ShouldBe((int)HttpStatusCode.OK);
            okResult.Value.ShouldBeOfType<double>();
        }

    }
}
