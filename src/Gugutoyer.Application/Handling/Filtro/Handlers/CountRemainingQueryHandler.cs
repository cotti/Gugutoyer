using Gugutoyer.Application.Handling.Filtro.Queries;
using Gugutoyer.Application.Handling.Palavra.Queries;
using Gugutoyer.Domain.Entities;
using Gugutoyer.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Handling.Filtro.Handlers
{
    public class CountRemainingQueryHandler : IRequestHandler<CountRemainingQuery, int>
    {
        private readonly IFiltroRepository _filtroRepository;

        public CountRemainingQueryHandler(IFiltroRepository filtroRepository)
        {
            _filtroRepository = filtroRepository;
        }

        public async Task<int> Handle(CountRemainingQuery request, CancellationToken cancellationToken)
        {
            return await _filtroRepository.CountRemaining();
        }
    }
}
