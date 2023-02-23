using Gugutoyer.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Interfaces.Data
{
    public interface IPalavrasService
    {
        Task<IEnumerable<PalavraDTO>> GetRandomPool(int amount);
        Task<PalavraDTO> GetById(int id);
        Task<bool> UpdateUsed(IEnumerable<PalavraDTO> toUpdate);
    }
}
