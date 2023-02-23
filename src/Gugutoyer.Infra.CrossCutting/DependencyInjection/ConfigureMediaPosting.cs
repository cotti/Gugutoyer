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

namespace Gugutoyer.Infra.CrossCutting.DependencyInjection
{
    public static class ConfigureMediaPosting
    {
        public static IServiceCollection AddMediaPoster(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new TwitterMediaPosterSettings()
            {
                ApiKey = configuration.GetSection("TwitterMediaPosterSettings")["ApiKey"],
                ApiSecret = configuration.GetSection("TwitterMediaPosterSettings")["ApiSecret"],
                AccessToken = configuration.GetSection("TwitterMediaPosterSettings")["AccessToken"],
                AccessTokenSecret = configuration.GetSection("TwitterMediaPosterSettings")["AccessTokenSecret"],
                MessageTargetHandlerId = configuration.GetSection("TwitterMediaPosterSettings")["MessageTargetHandlerId"]
            });
            services.AddTransient<IMediaPoster, TwitterMediaPoster>();
            return services;
        }
    }
}
