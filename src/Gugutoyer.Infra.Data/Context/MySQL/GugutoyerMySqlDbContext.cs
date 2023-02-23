using Gugutoyer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.Data.Context.MySQL
{
    public class GugutoyerMySqlDbContext : DbContext
    {
        public GugutoyerMySqlDbContext(DbContextOptions<GugutoyerMySqlDbContext> options) : base(options) { }

        public DbSet<Palavra> Palavras { get; set; }
        public DbSet<Filtro> Filtros { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(GugutoyerMySqlDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Debug).EnableDetailedErrors().EnableSensitiveDataLogging();
    }
}
