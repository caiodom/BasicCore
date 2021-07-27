using AutoMapper;
using Core.Application.DTO;
using Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Configuration
{
    public abstract class BaseAutoMapperConfig:Profile
    {
       protected void ResolveMapper()
        {
            CreateMap<BaseEntity, BaseEntityDTO>().ReverseMap();
            CreateMap<BaseEntityDTO, BaseEntity>().ReverseMap();
        }
    }
}
