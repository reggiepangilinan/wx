using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Wx.Exercises.Application.Exercise1.Queries.GetUser;

namespace Wx.Exercises.Api
{
    public static class StartupMediatRExtension
    {
        public static IServiceCollection RegisterMediatR(this IServiceCollection services)
        {
            // Register Mediatr
            var assemblyServiceType = typeof(GetUserQuery).GetTypeInfo();
            var assemblyServiceInfo = assemblyServiceType.Assembly;

            return services.AddMediatR(assemblyServiceInfo);
        }
    }
}
