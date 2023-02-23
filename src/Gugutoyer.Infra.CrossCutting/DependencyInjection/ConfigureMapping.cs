using Gugutoyer.Application.Mapping;
using Gugutoyer.Infra.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.CrossCutting.DependencyInjection
{
    public static class ConfigureMapping
    {
        public static IServiceCollection AddMapping(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            return services;
        }
    }
}
