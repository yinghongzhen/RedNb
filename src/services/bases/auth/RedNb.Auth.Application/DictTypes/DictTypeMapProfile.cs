using AutoMapper;
using RedNb.Auth.Application.Contracts.DictDatas.Dtos;
using RedNb.Auth.Application.Contracts.DictTypes.Dtos;
using RedNb.Auth.Domain.Admins;
using RedNb.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.DictTypes
{
    public class DictTypeMapProfile : Profile
    {
        public DictTypeMapProfile()
        {
            CreateMap<DictTypeAddInputDto, DictType>();

            CreateMap<DictTypeUpdateInputDto, DictType>();

            CreateMap<DictType, DictTypeOutputDto>();

            CreateMap<DictDataAddInputDto, DictData>();

            CreateMap<DictDataUpdateInputDto, DictData>();

            CreateMap<DictData, DictDataOutputDto>();
        }
    }
}
