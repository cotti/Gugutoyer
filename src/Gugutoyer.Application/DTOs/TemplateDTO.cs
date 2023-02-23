using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gugutoyer.Application.DTOs
{
    public class TemplateDTO
    {
        [JsonPropertyName("file")]
        public string? File { get; set; }
        [JsonPropertyName("horizontalcut")]
        public int HorizontalCut { get; set; }
        [JsonPropertyName("verticalcut")]
        public int VerticalCut { get; set; }
    }
}
