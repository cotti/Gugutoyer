using Gugutoyer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Domain.Interfaces
{
    public interface IPalavraRepository
    {
        Task<IEnumerable<Palavra>> GetRandomPool(int amount);
        Task<Palavra> GetById(int id);
        Task<bool> UpdateUsed(IEnumerable<Palavra> toUpdate);
    }
}
