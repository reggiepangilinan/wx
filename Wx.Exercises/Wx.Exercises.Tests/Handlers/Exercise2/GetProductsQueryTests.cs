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

namespace Wx.Exercises.Tests.Handlers.Exercise1
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
                 new Product { Name = "A", Price = 99, Quantity = 10},
                 new Product { Name = "B", Price = 199, Quantity = 5},
                 new Product { Name = "C", Price = 299, Quantity = 3},
                 new Product { Name = "D", Price = 399, Quantity = 1},
                };
        }

        private static List<CustomerProducts> CreateCustomerProductsStub()
        {
            return new List<CustomerProducts>() {
                 new CustomerProducts
                 {
                     CustomerId = 1,
                     Products = new List<Product>() {
                                                     new Product { Name = "A", Price = 1, Quantity = 2},
                                                     new Product { Name = "C", Price = 2, Quantity = 5},
                                                    }
                 },
                 new CustomerProducts
                 {
                     CustomerId = 2,
                     Products = new List<Product>() {
                                                     new Product { Name = "C", Price = 2, Quantity = 10},
                                                     new Product { Name = "D", Price = 4, Quantity = 1},
                                                    }
                 },
                 new CustomerProducts
                 {
                     CustomerId = 3,
                     Products = new List<Product>() {
                                                     new Product { Name = "A", Price = 1, Quantity = 2},
                                                     new Product { Name = "B", Price = 100, Quantity = 5},
                                                     new Product { Name = "C", Price = 2, Quantity = 10},
                                                    }
                 },
                new CustomerProducts
                 {
                     CustomerId = 4,
                     Products = new List<Product>() {
                                                     new Product { Name = "C", Price = 2, Quantity = 10},
                                                     new Product { Name = "D", Price = 4, Quantity = 1},
                                                    }
                 }
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
            response.First().Name.ShouldBe("A");
            response.Last().Name.ShouldBe("D");
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
            response.First().Name.ShouldBe("D");
            response.Last().Name.ShouldBe("A");
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
            response.Last().Name.ShouldBe("D");
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
            response.First().Name.ShouldBe("D");
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
            response.First().Name.ShouldBe("C");
            response.Last().Name.ShouldBe("B");
        }
    }
}
