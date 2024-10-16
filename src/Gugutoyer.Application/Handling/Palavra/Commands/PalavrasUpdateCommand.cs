﻿using Gugutoyer.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Handling.Palavra.Commands
{
    public class PalavrasUpdateCommand : IRequest<bool>
    {
        public IEnumerable<Gugutoyer.Domain.Entities.Palavra>? ToUpdate { get; set; }
    }
}
