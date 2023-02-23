using Gugutoyer.Application.Handling.Palavra.Commands;
using Gugutoyer.Application.Handling.Palavra.Queries;
using Gugutoyer.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Handling.Palavra.Handlers
{

    public class PalavrasUpdateCommandHandler : IRequestHandler<PalavrasUpdateCommand, bool>
    {
        private readonly IPalavraRepository _palavraRepository;

        public PalavrasUpdateCommandHandler(IPalavraRepository palavraRepository)
        {
            _palavraRepository = palavraRepository;
        }

        public async Task<bool> Handle(PalavrasUpdateCommand request, CancellationToken cancellationToken)
        {
            return await _palavraRepository.UpdateUsed(request.ToUpdate!);
        }
    }
}
