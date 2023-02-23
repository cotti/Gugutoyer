using Gugutoyer.Domain.Entities;
using Gugutoyer.Domain.Interfaces;
using Gugutoyer.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gugutoyer.Infra.Data.Context.MySQL.Repositories
{
    public class PalavraMySqlRepository : IPalavraRepository
    {
        private readonly GugutoyerMySqlDbContext _context;

        public PalavraMySqlRepository(GugutoyerMySqlDbContext context)
        {
            _context = context;
        }
        public async Task<Palavra> GetById(int id)
        {
            return await _context.Set<Palavra>().SingleAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Palavra>> GetRandomPool(int amount)
        {
            return await _context.Set<Palavra>().Where(p => p.Usado == false).OrderBy(x => EF.Functions.Random()).Take(amount).ToListAsync();
        }

        public async Task<bool> UpdateUsed(IEnumerable<Palavra> toUpdate)
        {
            var inDb = toUpdate.Select(p => p.Id).ToArray();
            var changedRows = await _context.Set<Palavra>().Where(p => inDb.Contains(p.Id)).ExecuteUpdateAsync(v => v.SetProperty(p => p.Usado, p => true));
            return await _context.SaveChangesAsync() == changedRows && inDb.Length == changedRows;
        }
    }
}
