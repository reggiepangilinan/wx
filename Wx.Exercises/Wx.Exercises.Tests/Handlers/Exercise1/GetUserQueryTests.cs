using AutoFixture;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;
using System.Threading;
using Wx.Exercises.Application.Common.Configurations;
using Wx.Exercises.Application.Exercise1.Models;
using Wx.Exercises.Application.Exercise1.Queries.GetUser;
using Xunit;

namespace Wx.Exercises.Tests.Handlers.Exercise1
{
    public class GetUserQueryTests
    {
        private readonly GetUserHandler _sut;

        private readonly Mock<ILogger<GetUserHandler>> _mockLogger;

        private readonly Fixture _specimens = new Fixture();

        public GetUserQueryTests()
        {
            _mockLogger = new Mock<ILogger<GetUserHandler>>();
            _sut = new GetUserHandler(_mockLogger.Object, Options.Create(_specimens.Create<WxApiOptions>()));
        }

        [Fact]
        public async void Should_Return_BasicResponseModel()
        {
            // Arrange
            var request = new GetUserQuery();

            // Act
            var response = await _sut.Handle(request, CancellationToken.None);

            // Assert
            response.ShouldBeOfType<UserModel>();
            response.Name.ShouldNotBeEmpty();
            response.Token.ShouldNotBeEmpty();
        }
    }
}
