using AutoMapper;
using Gugutoyer.Application.DTOs;
using Gugutoyer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.Mapping
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Palavra, PalavraDTO>().ReverseMap();
            CreateMap<Filtro, FiltroDTO>().ReverseMap();
        }
    }
}
