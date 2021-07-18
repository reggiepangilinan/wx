using Newtonsoft.Json;
using System.Collections.Generic;

namespace Wx.Exercises.Services.Proxies.WxApiProxy.Models
{
    public class Trolley
    {
        [JsonProperty("products")]
        public List<TrolleyProduct> Products { get; init; }
        [JsonProperty("specials")]
        public List<TrolleySpecial> Specials { get; init; }
        [JsonProperty("quantities")]
        public List<TrolleyQuantity> Quantities { get; init; }
    }

    public class TrolleyProduct
    {
        [JsonProperty("name")]
        public string Name { get; init; }
        [JsonProperty("price")]
        public decimal Price { get; init; }
    }

    public class TrolleySpecial
    {
        [JsonProperty("quantities")]
        public List<TrolleyQuantity> Quantities { get; init; }
        [JsonProperty("total")]
        public double Total { get; init; }
    }

    public class TrolleyQuantity
    {
        [JsonProperty("name")]
        public string Name { get; init; }
        [JsonProperty("quantity")]
        public int Quantity { get; init; }
    }
}
