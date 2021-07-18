using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Wx.Exercises.Services.Proxies.WxApiProxy.Models;
using Wx.Exercises.Services.Proxies.WxProxy.Models;

namespace Wx.Exercises.Services.Proxies.WxApiProxy
{
    public interface IWxApiProxy
    {
        [Get("/api/resource/products")]
        Task<List<Product>> GetProducts([Query] string token, CancellationToken cancellationToken);

        [Get("/api/resource/shopperHistory")]
        Task<List<CustomerProducts>> GetShopperHistory([Query] string token, CancellationToken cancellationToken);

        [Post("/api/resource/trolleyCalculator")]
        Task<double> CalculateTrolley([Query] string token, [Body] Trolley request, CancellationToken cancellationToken);
    }
}
