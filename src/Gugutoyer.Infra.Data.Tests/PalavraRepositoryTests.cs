using Gugutoyer.Infra.Data.Context.MySQL;
using Gugutoyer.Infra.Data.Context.MySQL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Gugutoyer.Infra.Data.Tests
{
    public class PalavraRepositoryTests : IClassFixture<BaseTest>
    {
        private IServiceProvider _serviceProvider;

        public PalavraRepositoryTests(BaseTest baseTest)
        {
            _serviceProvider = baseTest.ServiceProvider;
        }

        [Fact]
        public async Task CanAccessWordList()
        {
            using var context = _serviceProvider.GetService<GugutoyerMySqlDbContext>();
            var repository = new PalavraMySqlRepository(context!);
            var result = await repository.GetRandomPool(10);

            Assert.NotNull(result);
            Assert.Equal(10,result.Count());
        }

        [Fact]
        public async Task CanAccessWord()
        {
            using var context = _serviceProvider.GetService<GugutoyerMySqlDbContext>();
            var repository = new PalavraMySqlRepository(context!);
            var result = await repository.GetById(1000);

            Assert.NotNull(result);
            Assert.Equal("ábia", result.Verbete);
        }
    }
}
