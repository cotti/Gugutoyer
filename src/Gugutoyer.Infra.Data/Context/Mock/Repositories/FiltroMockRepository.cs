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

namespace Gugutoyer.Infra.Data.Context.Mock.Repositories
{
    public class FiltroMockRepository : IFiltroRepository
    {
        public async Task<int> CountRemaining()
        {
            await Task.Delay(10);
            return 10;
        }

        public async Task<IEnumerable<Filtro>> GetFiltered()
        {
            await Task.Delay(10);
            return new List<Filtro>()
            {
                new Filtro{ Id = 1, PalavraId = 1, Palavra = new Palavra(1, "Doctiloquente", false)},
                new Filtro{ Id = 2, PalavraId = 2, Palavra = new Palavra(2, "Talismã", false)},
            };
        }
        public async Task<bool> Insert(IEnumerable<Filtro> toInsert)
        {
            await Task.Delay(10);
            return true;
        }

        public async Task<Filtro> GetById(int id)
        {
            await Task.Delay(10);
            return new Filtro { Id = 1, PalavraId = 1, Palavra = new Palavra(1, "Doctiloquente", false) };
        }

        public async Task<Filtro> GetNext()
        {
            await Task.Delay(10);
            return new Filtro { Id = 1, PalavraId = 1, Palavra = new Palavra(1, "Doctiloquente", false) };
        }

        public async Task<bool> Delete(IEnumerable<Filtro> toDelete)
        {
            await Task.Delay(10);
            return true;
        }
    }
}
