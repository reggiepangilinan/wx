using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using System;
using Wx.Exercises.Application.Common.Configurations;
using Wx.Exercises.Services.Proxies.WxApiProxy;

namespace Wx.Exercises.Api
{
    public static class StartupRefitExtension
    {
        public static IServiceCollection RegisterProxies(this IServiceCollection services)
        {
            // Register Proxies
            services
            .AddRefitClient<IWxApiProxy>()
            .ConfigureHttpClient((provider, httpClient) =>
            {
                var options = provider.GetService<IOptions<WxApiOptions>>();
                var url = new Uri(options.Value.BaseUrl);
                httpClient.BaseAddress = url;
            });
            return services;
        }
    }
}
