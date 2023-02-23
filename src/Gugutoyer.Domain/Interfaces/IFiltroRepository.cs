using Gugutoyer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Domain.Interfaces
{
    public interface IFiltroRepository
    {
        Task<IEnumerable<Filtro>> GetFiltered();
        Task<Filtro> GetById(int id);
        Task<bool> Insert(IEnumerable<Filtro> toInsert);
        Task<int> CountRemaining();
        Task<Filtro> GetNext();
        Task<bool> Delete(IEnumerable<Filtro> toDelete);
    }
}
