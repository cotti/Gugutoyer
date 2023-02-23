using MediatR;
using Gugutoyer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Handling.Filtro.Queries
{
    public class GetFilteredQuery : IRequest<IEnumerable<Gugutoyer.Domain.Entities.Filtro>>
    {
        public GetFilteredQuery() { }
    }
}
