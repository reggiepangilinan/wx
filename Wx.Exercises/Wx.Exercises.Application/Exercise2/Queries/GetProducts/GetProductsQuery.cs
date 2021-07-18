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

            var products = await GetProducts(cancellationToken);

            return request.SortOption switch
            {
                SortOption.Low => SortByPriceLowToHigh(products),
                SortOption.High => SortByPriceHighToLow(products),
                SortOption.Ascending => SortByNameAscending(products),
                SortOption.Descending => SortByNameDescending(products),
                SortOption.Recommended => SortByMostPopular(products, await GetCustomerProducts(cancellationToken)),
                _ => SortByPriceLowToHigh(products)
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

        private List<ProductModel> SortByMostPopular(List<Product> products, List<CustomerProducts> customerProducts)
        {
            var allProducts = customerProducts.SelectMany(x => x.Products).ToList();

            var productsSortedByPopularity =
                products.Select(p => new { Product = p, SoldCount = allProducts.Count(cp => cp.Name == p.Name) })
                .OrderByDescending(x => x.SoldCount)
                .Select(x => x.Product)
                .Select(ToProductModel)
                .ToList();

            return productsSortedByPopularity;
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
