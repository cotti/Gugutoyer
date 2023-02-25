using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.MediaPoster.Twitter
{
    public class TwitterMediaPosterSettings
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public string MessageTargetHandlerId { get; set; }

        public TwitterMediaPosterSettings(IConfiguration configuration)
        {
            ApiKey = configuration.GetSection("TwitterMediaPosterSettings")["ApiKey"]!;
            ApiSecret = configuration.GetSection("TwitterMediaPosterSettings")["ApiSecret"]!;
            AccessToken = configuration.GetSection("TwitterMediaPosterSettings")["AccessToken"]!;
            AccessTokenSecret = configuration.GetSection("TwitterMediaPosterSettings")["AccessTokenSecret"]!;
            MessageTargetHandlerId = configuration.GetSection("TwitterMediaPosterSettings")["MessageTargetHandlerId"]!;
        }
    }
}
