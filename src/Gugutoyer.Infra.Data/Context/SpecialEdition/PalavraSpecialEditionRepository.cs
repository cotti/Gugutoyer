using Gugutoyer.Application.Interfaces.Environment;
using Gugutoyer.Domain.Entities;
using Gugutoyer.Domain.Interfaces;
using Gugutoyer.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gugutoyer.Infra.Data.Context.SpecialEdition.Repositories
{
    public class PalavraSpecialEditionRepository : IPalavraRepository
    {
        private readonly IInputArgsService _inputArgsService;
        public PalavraSpecialEditionRepository(IInputArgsService inputArgsService)
        {
            _inputArgsService = inputArgsService;
        }
        public Task<Palavra> GetById(int id)
        {
            return new (() => new Palavra(id, _inputArgsService.GetWord(), false));
        }

        public Task<IEnumerable<Palavra>> GetRandomPool(int amount)
        {
            return new(() => new List<Palavra>());
        }

        public Task<bool> UpdateUsed(IEnumerable<Palavra> toUpdate)
        {
            return new(() => true);
        }
    }
}
