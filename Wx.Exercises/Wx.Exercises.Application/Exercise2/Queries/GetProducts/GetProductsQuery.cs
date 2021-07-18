using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wx.Exercises.Application.Common.Configurations;
using Wx.Exercises.Application.Common.Constants;
using Wx.Exercises.Application.Common.Enums;
using Wx.Exercises.Application.Exercise2.Models;
using Wx.Exercises.Services.Proxies.WxApiProxy;
using Wx.Exercises.Services.Proxies.WxProxy.Models;

namespace Wx.Exercises.Application.Exercise1.Queries.GetUser
{
    /// <summary>
    /// Request object
    /// </summary>
    public class GetProductsQuery : IRequest<List<ProductModel>>
    {
        public SortOption SortOption { get; init; }
    }

    /// <summary>
    /// Request Handler
    /// </summary>
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductModel>>
    {
        private readonly ILogger<GetProductsQueryHandler> _logger;
        private readonly IWxApiProxy _wxApiProxy;
        private readonly WxApiOptions _wxApiOptions;

        public GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger, IWxApiProxy wxApiProxy, IOptions<WxApiOptions> wxApiOptions)
        {
            _logger = logger;
            _wxApiProxy = wxApiProxy;
            _wxApiOptions = wxApiOptions?.Value;
        }

        public async Task<List<ProductModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{LoggerConstants.ClassName}::{LoggerConstants.MethodName} - Sorting product option: {request.SortOption}",
                        nameof(GetProductsQueryHandler),
                        nameof(Handle));

            return request.SortOption switch
            {
                SortOption.Low => SortByPriceLowToHigh(await GetProducts(cancellationToken)),
                SortOption.High => SortByPriceHighToLow(await GetProducts(cancellationToken)),
                SortOption.Ascending => SortByNameAscending(await GetProducts(cancellationToken)),
                SortOption.Descending => SortByNameDescending(await GetProducts(cancellationToken)),
                SortOption.Recommended => SortByMostPopular(await GetCustomerProducts(cancellationToken)),
                _ => SortByPriceLowToHigh(await GetProducts(cancellationToken))
            };
        }


        private async Task<List<Product>> GetProducts(CancellationToken cancellationToken)
        {
            return await _wxApiProxy.GetProducts(_wxApiOptions.Token, cancellationToken);
        }

        private async Task<List<CustomerProducts>> GetCustomerProducts(CancellationToken cancellationToken)
        {
            return await _wxApiProxy.GetShopperHistory(_wxApiOptions.Token, cancellationToken);
        }


        private List<ProductModel> SortByPriceLowToHigh(List<Product> products)
        {
            return products.OrderBy(x => x.Price)
                .Select(ToProductModel)
                .ToList();
        }

        private List<ProductModel> SortByPriceHighToLow(List<Product> products)
        {
            return products.OrderByDescending(x => x.Price)
                .Select(ToProductModel)
                .ToList();
        }

        private List<ProductModel> SortByNameAscending(List<Product> products)
        {
            return products.OrderBy(x => x.Name)
                .Select(ToProductModel)
                .ToList();
        }

        private List<ProductModel> SortByNameDescending(List<Product> products)
        {
            return products.OrderByDescending(x => x.Name)
                .Select(ToProductModel)
                .ToList();
        }

        private List<ProductModel> SortByMostPopular(List<CustomerProducts> customerProducts)
        {
            var allProducts = customerProducts.SelectMany(x => x.Products).ToList();

            var groupedProducts = allProducts
                        .GroupBy(x => new { x.Name, x.Price })
                        .Select(x =>
                       {
                           Func<Product, bool> MatchesNameAndPrice =
                               z => z.Name == x.Key.Name
                                 && z.Price == x.Key.Price;

                           return new ProductModel
                           {
                               Name = x.Key.Name,
                               Price = x.Key.Price,
                               Quantity = allProducts
                                           .Where(MatchesNameAndPrice)
                                           .Count()
                           };
                       })
                        .OrderByDescending(x => x.Quantity)
                        .ToList();

            return groupedProducts;
        }

        private static ProductModel ToProductModel(Product product)
        {
            return new ProductModel()
            {
                Name = product.Name,
                Quantity = product.Quantity,
                Price = product.Price
            };
        }
    }
}
