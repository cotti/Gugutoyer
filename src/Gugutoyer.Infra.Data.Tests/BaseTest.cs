using Gugutoyer.Infra.Data.Context.MySQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Gugutoyer.Infra.Data.Tests
{
    public class BaseTest
    {
        private string connectionString = "Server=;User Id=;Password=;Database=";
        private string version = "10.5.15";

        public IServiceProvider ServiceProvider { get; init; }

        public BaseTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(l => l.AddDebug().AddConsole());
            serviceCollection.AddDbContext<GugutoyerMySqlDbContext>(s =>
            {
                s.UseMySql(connectionString, new MySqlServerVersion(version))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using var context = ServiceProvider.GetService<GugutoyerMySqlDbContext>();
        }
    }
}