using AutoMapper;
using Gugutoyer.Application.DTOs;
using Gugutoyer.Application.Handling.Filtro.Commands;
using Gugutoyer.Application.Handling.Filtro.Queries;
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
    public class FiltrosService : IFiltrosService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public FiltrosService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<int> CountRemaining()
        {
            var query = new CountRemainingQuery();
            return await _mediator.Send(query);
        }

        public async Task<IEnumerable<FiltroDTO>> GetFiltered()
        {
            var query = new GetFilteredQuery();
            return _mapper.Map<List<FiltroDTO>>(await _mediator.Send(query));
        }

        public async Task<bool> Insert(IEnumerable<FiltroDTO> toInsert)
        {
            var command = new FiltroInsertCommand()
            {
                ToAdd = _mapper.Map<List<Filtro>>(toInsert)
            };
            return await _mediator.Send(command);
        }

        public async Task<FiltroDTO> GetById(int id)
        {
            var query = new GetFiltroByIdQuery(id);
            return _mapper.Map<FiltroDTO>(await _mediator.Send(query));
        }

        public async Task<FiltroDTO> GetNext()
        {
            var query = new GetNextQuery();
            var filtro = await _mediator.Send(query);

            PalavrasUpdateCommand command = new PalavrasUpdateCommand()
            {
                ToUpdate = new List<Palavra>() { filtro.Palavra }
            };
            await _mediator.Send(command);

            return _mapper.Map<FiltroDTO>(filtro);
        }

        public async Task<bool> Delete(IEnumerable<FiltroDTO> toRemove)
        {
            var command = new FiltroDeleteCommand()
            {
                ToRemove = _mapper.Map<List<Filtro>>(toRemove)
            };
            return await _mediator.Send(command);
        }
    }
}
