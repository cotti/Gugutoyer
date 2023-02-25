using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.MediaPoster.Mastodon
{
    public class MastodonMediaPosterRegistrationHelperSettings
    {
        public string Instance { get; set; }
        public string AppName { get; set; }

        public MastodonMediaPosterRegistrationHelperSettings(IConfiguration configuration)
        {
            AppName = configuration.GetSection("MastodonMediaPosterRegistrationHelperSettings")["AppName"]!;
            Instance = configuration.GetSection("MastodonMediaPosterRegistrationHelperSettings")["Instance"]!;
        }
    }
}
