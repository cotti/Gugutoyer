using Gugutoyer.Application.DTOs;
using Gugutoyer.Application.Interfaces.Environment;
using Gugutoyer.Application.Interfaces.ImageProcessing;
using Gugutoyer.Application.Services.ImageProcessing;
using Gugutoyer.Infra.Environment.Environment;
using Gugutoyer.Infra.Image.ImageProvider.Web;
using Gugutoyer.Infra.Image.MultipleImageProvider.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.CrossCutting.DependencyInjection
{
    public static class ConfigureImage
    {
        public static IServiceCollection AddImageProcessing(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IImageProcessor, ImageProcessor>();
            services.AddSingleton<WebImageSettings>();

            var templates = new List<TemplateDTO>();
            foreach (var key in configuration.GetSection("Templates").GetChildren())
                templates.Add(new TemplateDTO() { File = key["File"], HorizontalCut = uint.Parse(key["horizontalCut"]!), VerticalCut = uint.Parse(key["verticalCut"]!) });
            
            services.AddSingleton<IList<TemplateDTO>>(templates);
            services.AddHttpClient("imageprovider", client => 
            {
            });
            services.AddHttpClient("imagescraper", client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);
            });
            services.AddTransient<IImageProvider, WebImageProvider>();
            services.AddTransient<IMultipleImageProvider, ScraperMultipleImageProvider>();
            return services;
        }
    }
}
