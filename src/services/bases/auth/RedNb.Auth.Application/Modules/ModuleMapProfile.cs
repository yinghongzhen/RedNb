using AutoMapper;
using RedNb.Auth.Application.Contracts.Modules.Dtos;
using RedNb.Auth.Domain.Admins;
using RedNb.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Modules
{
    public class ModuleMapProfile : Profile
    {
        public ModuleMapProfile()
        {
            CreateMap<ModuleAddInputDto, Module>();

            CreateMap<ModuleUpdateInputDto, Module>();

            CreateMap<Module, ModuleOutputDto>()
                .ForMember(m => m.TypeStr, m => m.MapFrom(o => o.Type.GetDescription()));

            CreateMap<InstanceAddInputDto, Instance>();

            CreateMap<InstanceUpdateInputDto, Instance>();

            CreateMap<Instance, InstanceOutputDto>()
                .ForMember(m => m.StatusStr, m => m.MapFrom(o => o.Status.GetDescription()));
        }
    }
}
