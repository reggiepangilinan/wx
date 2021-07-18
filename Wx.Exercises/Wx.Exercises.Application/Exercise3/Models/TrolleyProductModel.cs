using Newtonsoft.Json;

namespace Wx.Exercises.Application.Exercise3.Models
{
    public class TrolleyProductModel
    {
        [JsonProperty("name")]
        public string Name { get; init; }
        [JsonProperty("price")]
        public decimal Price { get; init; }
    }
}
