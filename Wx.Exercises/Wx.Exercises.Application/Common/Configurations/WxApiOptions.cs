using System.ComponentModel.DataAnnotations;

namespace Wx.Exercises.Application.Common.Configurations
{
    public class WxApiOptions
    {
        [Required]
        public string BaseUrl { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string User { get; set; }
    }
}
