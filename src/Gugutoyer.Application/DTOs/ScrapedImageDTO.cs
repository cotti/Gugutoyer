using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.DTOs
{
    public class ScrapedImageDTO
    {
        public string? URL { get; set; }

        public byte[]? Image { get; set; }
    }
}
