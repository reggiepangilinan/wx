using System.Collections.Generic;

namespace Wx.Exercises.Services.Proxies.WxProxy.Models
{
    public class CustomerProducts
    {
        public int CustomerId { get; init; }
        public List<Product> Products { get; init; } = new List<Product>();
    }

}
