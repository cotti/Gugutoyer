﻿using Gugutoyer.Application.Handling.Filtro.Queries;
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
    public class GetFilteredQueryHandler : IRequestHandler<GetFilteredQuery, IEnumerable<Gugutoyer.Domain.Entities.Filtro>>
    {
        private readonly IFiltroRepository _filtroRepository;

        public GetFilteredQueryHandler(IFiltroRepository filtroRepository)
        {
            _filtroRepository = filtroRepository;
        }

        public async Task<IEnumerable<Gugutoyer.Domain.Entities.Filtro>> Handle(GetFilteredQuery request, CancellationToken cancellationToken)
        {
            return await _filtroRepository.GetFiltered();
        }
    }
}
