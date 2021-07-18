using Newtonsoft.Json;
using System.Collections.Generic;

namespace Wx.Exercises.Application.Exercise3.Models
{
    public class TrolleySpecialModel
    {
        [JsonProperty("quantities")]
        public List<TrolleyQuantityModel> Quantities { get; init; }
        [JsonProperty("total")]
        public double Total { get; init; }
    }
}
