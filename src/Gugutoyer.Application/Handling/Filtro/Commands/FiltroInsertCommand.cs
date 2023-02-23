using Gugutoyer.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Handling.Filtro.Commands
{
    public class FiltroInsertCommand : IRequest<bool>
    {
        public IEnumerable<Gugutoyer.Domain.Entities.Filtro>? ToAdd { get; set; }
    }
}
