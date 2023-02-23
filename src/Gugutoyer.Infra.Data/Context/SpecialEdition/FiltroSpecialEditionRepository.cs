using Gugutoyer.Application.Interfaces.Environment;
using Gugutoyer.Domain.Entities;
using Gugutoyer.Domain.Interfaces;
using Gugutoyer.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Infra.Data.Context.SpecialEdition.Repositories
{
    public class FiltroSpecialEditionRepository : IFiltroRepository
    {
        private readonly IInputArgsService _inputArgsService;
        public FiltroSpecialEditionRepository(IInputArgsService inputArgsService)
        {
            _inputArgsService = inputArgsService;
        }
        public Task<int> CountRemaining()
        {
            return new (() => 1);
        }

        public Task<IEnumerable<Filtro>> GetFiltered()
        {
            return new(() => new List<Filtro>());
            
        }
        public Task<bool> Insert(IEnumerable<Filtro> toInsert)
        {
            return new (() => true);
        }

        public Task<Filtro> GetById(int id)
        {
            return new (() => new Filtro());
        }

        public Task<Filtro> GetNext()
        {

            return new(() =>
                _inputArgsService.HasValidArgs() ?     
            new Filtro 
                { 
                    Id = 1,
                    PalavraId = 1,
                    Palavra = new Palavra (1, _inputArgsService.GetWord(), false)
                } : throw new InvalidOperationException("No words were defined for the special edition"));
        }

        public Task<bool> Delete(IEnumerable<Filtro> toDelete)
        {
            return new(() => true);
        }
    }
}
