using Gugutoyer.Application.Interfaces.Environment;
using Gugutoyer.Application.Mapping;
using Gugutoyer.Application.Services;
using Gugutoyer.Domain.Interfaces;
using Gugutoyer.Infra.Data.Context.MySQL.Repositories;
using Gugutoyer.Infra.Environment.Environment;
using Gugutoyer.Infra.Environment.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.CrossCutting.DependencyInjection
{
    public static class ConfigureEnvironment
    {
        public static IServiceCollection AddCommandLineArgs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IInputArgsService, CommandLineInputArgsService>();
            services.AddSingleton<SettingsInputSettings>();
            services.AddSingleton<IInputArgsService, SettingsInputArgsService>();
            return services;
        }
    }
}
