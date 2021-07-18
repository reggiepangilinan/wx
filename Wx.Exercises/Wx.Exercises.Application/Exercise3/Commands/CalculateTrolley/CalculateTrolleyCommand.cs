using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wx.Exercises.Application.Common.Configurations;
using Wx.Exercises.Application.Common.Constants;
using Wx.Exercises.Application.Exercise3.Models;
using Wx.Exercises.Services.Proxies.WxApiProxy;
using Wx.Exercises.Services.Proxies.WxApiProxy.Models;

namespace Wx.Exercises.Application.Exercise3.Commands.CalculateTrolley
{
    /// <summary>
    /// Request object
    /// </summary>
    public class CalculateTrolleyCommand : IRequest<double>
    {
        public List<TrolleyProductModel> Products { get; init; }
        public List<TrolleySpecialModel> Specials { get; init; }
        public List<TrolleyQuantityModel> Quantities { get; init; }
    }

    /// <summary>
    /// Request Handler
    /// </summary>
    public class CalculateTrolleyCommandHandler : IRequestHandler<CalculateTrolleyCommand, double>
    {
        private readonly ILogger<CalculateTrolleyCommandHandler> _logger;
        private readonly IWxApiProxy _wxApiProxy;
        private readonly WxApiOptions _wxApiOptions;

        public CalculateTrolleyCommandHandler(ILogger<CalculateTrolleyCommandHandler> logger, IWxApiProxy wxApiProxy, IOptions<WxApiOptions> wxApiOptions)
        {
            _logger = logger;
            _wxApiProxy = wxApiProxy;
            _wxApiOptions = wxApiOptions?.Value;
        }

        public async Task<double> Handle(CalculateTrolleyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{LoggerConstants.ClassName}::{LoggerConstants.MethodName} - Calculating trolley",
                        nameof(CalculateTrolleyCommandHandler),
                        nameof(Handle));

            return await _wxApiProxy.CalculateTrolley(_wxApiOptions.Token, CreateTrolleyFrom(request), cancellationToken);
        }

        private static Trolley CreateTrolleyFrom(CalculateTrolleyCommand request)
        {
            Func<TrolleyProductModel, TrolleyProduct> ToTrolleyProduct = x => new TrolleyProduct()
            {
                Name = x.Name,
                Price = x.Price
            };
            Func<TrolleyQuantityModel, TrolleyQuantity> ToTrolleyQuantity = x => new TrolleyQuantity()
            {
                Name = x.Name,
                Quantity = x.Quantity
            };
            Func<TrolleySpecialModel, TrolleySpecial> ToTrolleySpecial = x => new TrolleySpecial()
            {
                Quantities = x.Quantities.Select(ToTrolleyQuantity).ToList(),
                Total = x.Total
            };

            return new Trolley()
            {
                Products = request.Products.Select(ToTrolleyProduct).ToList(),
                Specials = request.Specials.Select(ToTrolleySpecial).ToList(),
                Quantities = request.Quantities.Select(ToTrolleyQuantity).ToList(),
            };
        }
    }
}
