using MediatR;
using Gugutoyer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Handling.Palavra.Queries
{
    public class GetPalavraByIdQuery : IRequest<Gugutoyer.Domain.Entities.Palavra>
    {
        public int Id { get; set; }
        public GetPalavraByIdQuery(int id) { Id = id; }
    }
}
