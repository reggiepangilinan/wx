using System.Collections.Generic;

namespace Wx.Exercises.Application.Exercise3.Models
{
    public class TrolleySpecialModel
    {
        public List<TrolleyQuantityModel> Quantities { get; init; }
        public int Total { get; init; }
    }
}
