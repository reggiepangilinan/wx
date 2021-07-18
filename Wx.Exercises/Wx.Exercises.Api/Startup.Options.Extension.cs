using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using System;
using Wx.Exercises.Application.Common.Configurations;
using Wx.Exercises.Services.Proxies.WxApiProxy;

namespace Wx.Exercises.Api
{
    public static class StartupOptionsExtension
    {
        public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<WxApiOptions>()
                .Bind(configuration.GetSection("WxApiOptions"))
                .ValidateDataAnnotations();

            return services;
        }
    }
}
