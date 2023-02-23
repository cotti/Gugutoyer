using Gugutoyer.Application.Interfaces.Data;
using Gugutoyer.Application.Services.Data;
using Gugutoyer.Domain.Interfaces;
using Gugutoyer.Infra.Data.Context.MySQL;
using Gugutoyer.Infra.Data.Context.MySQL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gugutoyer.Infra.CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPalavraRepository, PalavraMySqlRepository>();
            services.AddScoped<IFiltroRepository, FiltroMySqlRepository>();
            services.AddScoped<IPalavrasService, PalavrasService>();
            services.AddScoped<IFiltrosService, FiltrosService>();
            
            services.AddDbContext<GugutoyerMySqlDbContext>(s =>
            {
                s.UseMySql(configuration.GetSection("DB:ConnectionString").Value, new MySqlServerVersion(configuration.GetSection("DB:Version").Value))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);
            return services;
        }
    }
}
