using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Wx.Exercises.Application.Common.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortOption
    {
        [EnumMember(Value = "Low")]
        Low,
        [EnumMember(Value = "High")]
        High,
        [EnumMember(Value = "Ascending")]
        Ascending,
        [EnumMember(Value = "Descending")]
        Descending,
        [EnumMember(Value = "Recommended")]
        Recommended
    }
}
