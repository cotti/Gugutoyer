using MediatR;
using Gugutoyer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Handling.Palavra.Queries
{
    public class GetPalavrasQuery : IRequest<IEnumerable<Gugutoyer.Domain.Entities.Palavra>>
    {
        public int Amount { get; set; }
        public GetPalavrasQuery(int amount) { Amount = amount; }
    }
}
