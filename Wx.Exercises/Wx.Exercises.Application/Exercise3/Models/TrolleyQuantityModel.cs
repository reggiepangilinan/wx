using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wx.Exercises.Application.Exercise3.Models
{
    public class TrolleyQuantityModel
    {
        [JsonProperty("name")]
        public string Name { get; init; }

        [JsonProperty("quantity")]
        public int Quantity { get; init; }
    }
}
