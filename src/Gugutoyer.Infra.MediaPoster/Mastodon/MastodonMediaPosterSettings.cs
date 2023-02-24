using ImageMagick.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.MediaPoster.Mastodon
{
    public class MastodonMediaPosterSettings
    {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? Instance { get; set; }
        public string? AuthCode { get; set; }

        public MastodonMediaPosterSettings(IConfiguration configuration)
        {
            ClientId = configuration.GetSection("MastodonMediaPosterSettings")["ClientId"];
            ClientSecret = configuration.GetSection("MastodonMediaPosterSettings")["ClientSecret"];
            Instance = configuration.GetSection("MastodonMediaPosterSettings")["Instance"];
            AuthCode = configuration.GetSection("MastodonMediaPosterSettings")["AuthCode"];
        }
    }
}
