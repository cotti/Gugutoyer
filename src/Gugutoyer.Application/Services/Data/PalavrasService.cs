using AutoMapper;
using Gugutoyer.Application.DTOs;
using Gugutoyer.Application.Handling.Palavra.Commands;
using Gugutoyer.Application.Handling.Palavra.Handlers;
using Gugutoyer.Application.Handling.Palavra.Queries;
using Gugutoyer.Application.Interfaces;
using Gugutoyer.Application.Interfaces.Data;
using Gugutoyer.Domain.Entities;
using Gugutoyer.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Services.Data
{
    public class PalavrasService : IPalavrasService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public PalavrasService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<PalavraDTO> GetById(int id)
        {
            var query = new GetPalavraByIdQuery(id);
            return _mapper.Map<PalavraDTO>(await _mediator.Send(query));
        }

        public async Task<IEnumerable<PalavraDTO>> GetRandomPool(int amount)
        {
            var query = new GetPalavrasQuery(amount);
            return _mapper.Map<List<PalavraDTO>>(await _mediator.Send(query));
        }

        public async Task<bool> UpdateUsed(IEnumerable<PalavraDTO> toUpdate)
        {
            var command = new PalavrasUpdateCommand()
            {
                ToUpdate = _mapper.Map<List<Palavra>>(toUpdate)
            };
            return await _mediator.Send(command);
        }
    }
}
