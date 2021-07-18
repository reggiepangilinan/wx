using AutoFixture;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Wx.Exercises.Application.Common.Configurations;
using Wx.Exercises.Application.Common.Enums;
using Wx.Exercises.Application.Exercise1.Queries.GetUser;
using Wx.Exercises.Application.Exercise2.Models;
using Wx.Exercises.Services.Proxies.WxApiProxy;
using Wx.Exercises.Services.Proxies.WxProxy.Models;
using Xunit;

namespace Wx.Exercises.Tests.Handlers.Exercise2
{
    public class GetProductsQueryTests
    {
        private readonly GetProductsQueryHandler _sut;

        private readonly Mock<ILogger<GetProductsQueryHandler>> _mockLogger;

        private readonly Mock<IWxApiProxy> _mockApiProxy;

        private readonly Fixture _specimens = new Fixture();

        public GetProductsQueryTests()
        {
            _mockLogger = new Mock<ILogger<GetProductsQueryHandler>>();
            _mockApiProxy = new Mock<IWxApiProxy>();

            _mockApiProxy.Setup(x => x.GetProducts(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateProductsStub());

            _mockApiProxy.Setup(x => x.GetShopperHistory(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateCustomerProductsStub());

            _sut = new GetProductsQueryHandler(
                _mockLogger.Object,
                _mockApiProxy.Object,
                Options.Create(_specimens.Create<WxApiOptions>()));
        }

        private static List<Product> CreateProductsStub()
        {
            return new List<Product>() {
                 new Product { Name = "A", Price = 99.99, Quantity = 0},
                 new Product { Name = "B", Price = 101.99, Quantity = 0},
                 new Product { Name = "C", Price = 10.99, Quantity = 0},
                 new Product { Name = "D", Price = 5, Quantity = 0},
                 new Product { Name = "F", Price = 999999999999, Quantity = 0},
                };
        }

        private static List<CustomerProducts> CreateCustomerProductsStub()
        {
            return new List<CustomerProducts>() {
                 new CustomerProducts
                 {
                     CustomerId = 123,
                     Products = new List<Product>() {
                                                     new Product { Name = "A", Price = 99.99, Quantity = 3},
                                                     new Product { Name = "B", Price = 101.99, Quantity = 1},
                                                     new Product { Name = "F", Price = 999999999999, Quantity = 1},

                                                    }
                 },
                 new CustomerProducts
                 {
                     CustomerId = 23,
                     Products = new List<Product>() {
                                                     new Product { Name = "A", Price = 99.99, Quantity = 2},
                                                     new Product { Name = "B", Price = 101.99, Quantity = 3},
                                                     new Product { Name = "F", Price = 999999999999, Quantity = 1},
                                                    }
                 },
                 new CustomerProducts
                 {
                     CustomerId = 23,
                     Products = new List<Product>() {
                                                     new Product { Name = "C", Price = 10.99, Quantity = 2},
                                                     new Product { Name = "F", Price = 999999999999, Quantity = 2},
                                                    }
                 },
                    new CustomerProducts
                 {
                     CustomerId = 23,
                     Products = new List<Product>() {
                                                    new Product { Name = "A", Price = 99.99, Quantity = 1},
                                                    new Product { Name = "B", Price = 101.99, Quantity = 2},
                                                    new Product { Name = "C", Price = 10.99, Quantity = 2},
                                                    }
                 },

                };
        }

        [Fact]
        public async void Should_SortPriceLowToHigh_AndReturn_ListOf_ProductModel()
        {
            // Arrange
            var request = new GetProductsQuery()
            {
                SortOption = SortOption.Low
            };

            // Act
            var response = await _sut.Handle(request, CancellationToken.None);

            // Assert
            response.ShouldBeOfType<List<ProductModel>>();
            response.ShouldNotBeEmpty();
            response.First().Name.ShouldBe("D");
            response.Last().Name.ShouldBe("F");
        }

        [Fact]
        public async void Should_SortPriceHighToLow_AndReturn_ListOf_ProductModel()
        {
            // Arrange
            var request = new GetProductsQuery()
            {
                SortOption = SortOption.High
            };

            // Act
            var response = await _sut.Handle(request, CancellationToken.None);

            // Assert
            response.ShouldBeOfType<List<ProductModel>>();
            response.ShouldNotBeEmpty();
            response.First().Name.ShouldBe("F");
            response.Last().Name.ShouldBe("D");
        }

        [Fact]
        public async void Should_SortNameAscending_AndReturn_ListOf_ProductModel()
        {
            // Arrange
            var request = new GetProductsQuery()
            {
                SortOption = SortOption.Ascending
            };

            // Act
            var response = await _sut.Handle(request, CancellationToken.None);

            // Assert
            response.ShouldBeOfType<List<ProductModel>>();
            response.ShouldNotBeEmpty();
            response.First().Name.ShouldBe("A");
            response.Last().Name.ShouldBe("F");
        }

        [Fact]
        public async void Should_SortNameDescending_AndReturn_ListOf_ProductModel()
        {
            // Arrange
            var request = new GetProductsQuery()
            {
                SortOption = SortOption.Descending
            };

            // Act
            var response = await _sut.Handle(request, CancellationToken.None);

            // Assert
            response.ShouldBeOfType<List<ProductModel>>();
            response.ShouldNotBeEmpty();
            response.First().Name.ShouldBe("F");
            response.Last().Name.ShouldBe("A");
        }


        [Fact]
        public async void Should_SortByMostPopular_AndReturn_ListOf_ProductModel()
        {
            // Arrange
            var request = new GetProductsQuery()
            {
                SortOption = SortOption.Recommended
            };

            // Act
            var response = await _sut.Handle(request, CancellationToken.None);

            // Assert
            response.ShouldBeOfType<List<ProductModel>>();
            response.ShouldNotBeEmpty();
            response.First().Name.ShouldBe("A");
            response.Last().Name.ShouldBe("D");
        }
    }
}
