using Gugutoyer.Application.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.CrossCutting.DependencyInjection
{
    public static class ConfigureMediator
    {
        public static IServiceCollection AddMediator(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(m => m.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("Gugutoyer.Application")));
            return services;
        }
    }
}
