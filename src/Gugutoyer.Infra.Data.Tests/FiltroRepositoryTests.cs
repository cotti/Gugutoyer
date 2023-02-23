using Gugutoyer.Domain.Entities;
using Gugutoyer.Infra.Data.Context.MySQL;
using Gugutoyer.Infra.Data.Context.MySQL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Gugutoyer.Infra.Data.Tests
{
    public class FiltroRepositoryTests : IClassFixture<BaseTest>
    {
        private IServiceProvider _serviceProvider;

        public FiltroRepositoryTests(BaseTest baseTest)
        {
            _serviceProvider = baseTest.ServiceProvider;
        }

        [Fact]
        public async Task CanAccessFilter()
        {
            using var context = _serviceProvider.GetService<GugutoyerMySqlDbContext>();
            var repository = new FiltroMySqlRepository(context!);
            var testFiltro = new Filtro() { Id = 1, Palavra = new Palavra(1000, "ábia", false), PalavraId = 1000 };
            var result = await repository.Insert(new List<Filtro>() { testFiltro });

            Assert.True(result);

            context!.Filtros.Remove(testFiltro);
            await context!.SaveChangesAsync();
        }

    }
}
