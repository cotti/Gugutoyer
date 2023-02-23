using Gugutoyer.Application.Handling.Palavra.Queries;
using Gugutoyer.Domain.Entities;
using Gugutoyer.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Handling.Palavra.Handlers
{
    public class GetPalavraByIdQueryHandler : IRequestHandler<GetPalavraByIdQuery, Gugutoyer.Domain.Entities.Palavra>
    {
        private readonly IPalavraRepository _palavraRepository;

        public GetPalavraByIdQueryHandler(IPalavraRepository palavraRepository)
        {
            _palavraRepository = palavraRepository;
        }

        public async Task<Gugutoyer.Domain.Entities.Palavra> Handle(GetPalavraByIdQuery request, CancellationToken cancellationToken)
        {
            return await _palavraRepository.GetById(request.Id);
        }
    }
}
