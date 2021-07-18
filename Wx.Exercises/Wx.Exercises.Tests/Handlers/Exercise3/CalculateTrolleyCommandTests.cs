using AutoFixture;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;
using System.Threading;
using Wx.Exercises.Application.Common.Configurations;
using Wx.Exercises.Application.Exercise3.Commands.CalculateTrolley;
using Wx.Exercises.Services.Proxies.WxApiProxy;
using Wx.Exercises.Services.Proxies.WxApiProxy.Models;
using Xunit;

namespace Wx.Exercises.Tests.Handlers.Exercise3
{
    public class CalculateTrolleyCommandTests
    {
        private readonly CalculateTrolleyCommandHandler _sut;

        private readonly Mock<ILogger<CalculateTrolleyCommandHandler>> _mockLogger;

        private readonly Mock<IWxApiProxy> _mockApiProxy;

        private readonly Fixture _specimens = new Fixture();

        public CalculateTrolleyCommandTests()
        {
            _mockLogger = new Mock<ILogger<CalculateTrolleyCommandHandler>>();
            _mockApiProxy = new Mock<IWxApiProxy>();

            _sut = new CalculateTrolleyCommandHandler(
                _mockLogger.Object,
                _mockApiProxy.Object,
                Options.Create(_specimens.Create<WxApiOptions>()));
        }

        [Fact]
        public async void Should_Call_CalculateTrolley_AndReturn_Double()
        {
            // Arrange
            var request = _specimens.Create<CalculateTrolleyCommand>();

            _mockApiProxy.Setup(x => x.CalculateTrolley(It.IsAny<string>(), It.IsAny<Trolley>(), It.IsAny<CancellationToken>()));

            // Act
            var response = await _sut.Handle(request, CancellationToken.None);

            // Assert
            response.ShouldBeOfType<double>();

            _mockApiProxy.Verify(x => x.CalculateTrolley(It.IsAny<string>(), It.IsAny<Trolley>(), It.IsAny<CancellationToken>()),Times.Exactly(1));
        }

    }
}
