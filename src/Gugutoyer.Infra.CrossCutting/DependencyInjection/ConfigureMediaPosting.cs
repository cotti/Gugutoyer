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
            //Mastodon
            services.AddHttpClient("mastodon", client =>
            {
            });
            if (bool.Parse(configuration.GetSection("Register")["MastodonApp"]!))
            {
                services.AddSingleton<MastodonMediaPosterRegistrationHelperSettings>();
                services.AddSingleton<MastodonMediaPosterRegistrationHelper>();
            }
            services.AddSingleton<MastodonMediaPosterSettings>();
            services.AddTransient<IMediaPoster, MastodonMediaPoster>();

            //Twitter
            services.AddSingleton<TwitterMediaPosterSettings>();
            services.AddTransient<IMediaPoster, TwitterMediaPoster>();
            return services;
        }
    }
}
