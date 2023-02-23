using Gugutoyer.Application.Handling.Filtro.Commands;
using Gugutoyer.Application.Handling.Palavra.Commands;
using Gugutoyer.Application.Handling.Palavra.Queries;
using Gugutoyer.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Handling.Filtro.Handlers
{

    public class FiltroInsertCommandHandler : IRequestHandler<FiltroInsertCommand, bool>
    {
        private readonly IFiltroRepository _filtroRepository;

        public FiltroInsertCommandHandler(IFiltroRepository filtroRepository)
        {
            _filtroRepository = filtroRepository;
        }

        public async Task<bool> Handle(FiltroInsertCommand request, CancellationToken cancellationToken)
        {
            return await _filtroRepository.Insert(request.ToAdd!);
        }
    }
}
