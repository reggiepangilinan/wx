using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Wx.Exercises.Application.Exercise1.Queries.GetUserAndToken;

namespace Wx.Exercises.Api
{
    public static class StartupMediatRExtension
    {
        public static IServiceCollection RegisterMediatR(this IServiceCollection services)
        {
            // Register Mediatr
            var assemblyServiceType = typeof(GetUserAndTokenQuery).GetTypeInfo();
            var assemblyServiceInfo = assemblyServiceType.Assembly;

            
            return services.AddMediatR(assemblyServiceInfo);
        }
    }
}
