using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.Image.ImageProvider.Web
{
    public class WebImageSettings
    {
        public string ApiKey { get; set; }
        public string SearchEngineId { get; set; }

        public WebImageSettings(IConfiguration configuration)
        {
            ApiKey = configuration.GetSection("WebImageSettings")["ApiKey"]!;
            SearchEngineId = configuration.GetSection("WebImageSettings")["SearchEngineId"]!;
        }
    }
}
