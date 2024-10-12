using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.MediaPoster.Bluesky
{
    public class BlueskyMediaPosterSettings
    {
        public string UserName { get; set; }
        public string AppPassword { get; set; }
        public string MessageTargetHandlerId { get; set; }

        public BlueskyMediaPosterSettings(IConfiguration configuration)
        {
            UserName = configuration.GetSection("BlueskyMediaPosterSettings")["UserName"]!;
            AppPassword = configuration.GetSection("BlueskyMediaPosterSettings")["AppPassword"]!;
            MessageTargetHandlerId = configuration.GetSection("BlueskyMediaPosterSettings")["MessageTargetHandlerId"]!;
        }
    }
}
