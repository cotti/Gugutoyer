using Gugutoyer.Application.DTOs;
using Gugutoyer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Interfaces.Data
{
    public interface IFiltrosService
    {
        Task<IEnumerable<FiltroDTO>> GetFiltered();
        Task<FiltroDTO> GetById(int id);
        Task<bool> Insert(IEnumerable<FiltroDTO> toInsert);
        Task<int> CountRemaining();
        Task<FiltroDTO> GetNext();
        Task<bool> Delete(IEnumerable<FiltroDTO> toRemove);
    }
}
