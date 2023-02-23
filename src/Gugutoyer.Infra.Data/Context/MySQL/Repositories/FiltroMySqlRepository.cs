using Gugutoyer.Domain.Entities;
using Gugutoyer.Domain.Interfaces;
using Gugutoyer.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Gugutoyer.Infra.Data.Context.MySQL.Repositories
{

    public class FiltroMySqlRepository : IFiltroRepository
    {
        private readonly GugutoyerMySqlDbContext _context;

        public FiltroMySqlRepository(GugutoyerMySqlDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountRemaining()
        {
            return await _context.Set<Filtro>().CountAsync();
        }

        public async Task<IEnumerable<Filtro>> GetFiltered()
        {
            return await _context.Set<Filtro>().Include(f => f.Palavra).ToListAsync();
        }
        public async Task<bool> Insert(IEnumerable<Filtro> toInsert)
        {
            try
            {
                foreach (var item in toInsert) { _context.Entry(item).State = EntityState.Unchanged; }
                await _context.Set<Filtro>().AddRangeAsync(toInsert);
                return await _context.SaveChangesAsync() == toInsert.Count();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<Filtro> GetById(int id)
        {
            return await _context.Set<Filtro>().Where(f => f.Id == id).Include(f => f.Palavra).SingleAsync();
        }

        public async Task<Filtro> GetNext()
        {
            return await _context.Set<Filtro>().OrderBy(x => EF.Functions.Random()).FirstAsync();
        }

        public async Task<bool> Delete(IEnumerable<Filtro> toDelete)
        {
            var inDb = toDelete.Select(f => f.Id).ToArray();
            var resultRows = await _context.Set<Filtro>().Where(f => inDb.Contains(f.Id)).ExecuteDeleteAsync();
            return await _context.SaveChangesAsync() == resultRows && inDb.Length == resultRows;
        }
    }
}
