using Gugutoyer.Application.Interfaces.Data;
using Gugutoyer.Application.Services.Data;
using Gugutoyer.Domain.Interfaces;
using Gugutoyer.Infra.Data.Context.MySQL.Repositories;
using Gugutoyer.Infra.Data.Context.MySQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gugutoyer.Application.Interfaces.ImageProcessing;
using Gugutoyer.Application.Services.ImageProcessing;
using Gugutoyer.Infra.Image.ImageProvider.Web;
using Gugutoyer.Application.Interfaces.MediaPoster;
using Gugutoyer.Infra.MediaPoster.Twitter;
using Gugutoyer.Infra.MediaPoster.Mastodon;

namespace Gugutoyer.Infra.CrossCutting.DependencyInjection
{
    public static class ConfigureMediaPosting
    {
        public static IServiceCollection AddMediaPoster(this IServiceCollection services, IConfiguration configuration)
        {
            if (bool.Parse(configuration.GetSection("Register")["MastodonApp"]!))
            {
                services.AddSingleton(new MastodonMediaPosterRegistrationHelperSettings()
                {
                    AppName = configuration.GetSection("MastodonMediaPosterRegistrationHelperSettings")["AppName"],
                    Instance = configuration.GetSection("MastodonMediaPosterRegistrationHelperSettings")["Instance"]
                });
                services.AddSingleton<MastodonMediaPosterRegistrationHelper>();
            }
            services.AddSingleton<MastodonMediaPosterSettings>();
            services.AddHttpClient("mastodon",client =>
            {
            });
            services.AddTransient<IMediaPoster, MastodonMediaPoster>();
            services.AddSingleton(new TwitterMediaPosterSettings()
            {
                ApiKey = configuration.GetSection("TwitterMediaPosterSettings")["ApiKey"],
                ApiSecret = configuration.GetSection("TwitterMediaPosterSettings")["ApiSecret"],
                AccessToken = configuration.GetSection("TwitterMediaPosterSettings")["AccessToken"],
                AccessTokenSecret = configuration.GetSection("TwitterMediaPosterSettings")["AccessTokenSecret"],
                MessageTargetHandlerId = configuration.GetSection("TwitterMediaPosterSettings")["MessageTargetHandlerId"]
            });
            //services.AddTransient<IMediaPoster, TwitterMediaPoster>();
            return services;
        }
    }
}
