using Gugutoyer.Domain.Entities;
using Gugutoyer.Domain.Interfaces;
using Gugutoyer.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gugutoyer.Infra.Data.Context.Mock.Repositories
{
    public class PalavraMockRepository : IPalavraRepository
    {
        public async Task<Palavra> GetById(int id)
        {
            await Task.Delay(10);
            return new Palavra(1, "adiantado", false);
        }

        public async Task<IEnumerable<Palavra>> GetRandomPool(int amount)
        {
            await Task.Delay(10);
            return new List<Palavra>()
            {
                new Palavra(1, "Doctiloquente", false),
                new Palavra(2, "Talismã", false)
            };
        }

        public async Task<bool> UpdateUsed(IEnumerable<Palavra> toUpdate)
        {
            await Task.Delay(10);
            return true;
        }
    }
}
