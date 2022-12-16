using AutoMapper;
using RedNb.Auth.Application.Contracts.Permissions.Dtos;
using RedNb.Auth.Application.Contracts.Users.Dtos;
using RedNb.Auth.Domain.Menus;
using RedNb.Core.Contracts;
using RedNb.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Permissions
{
    public class PermissionMapProfile : Profile
    {
        public PermissionMapProfile()
        {
            CreateMap<PermissionAddInputDto, Permission>();

            CreateMap<PermissionUpdateInputDto, Permission>();

            CreateMap<Permission, PermissionOutputDto>()
                .ForMember(m => m.TypeStr, m => m.MapFrom(o => o.Type.GetDescription()))
                .ForMember(m => m.FullPath, m => m.MapFrom(o => String.IsNullOrWhiteSpace(o.Path) ? o.TreeKeys.Replace("_", "/") : o.Path));

            CreateMap<PermissionOutputDto, AntdTreeOutputDto>()
                .ForMember(m => m.Title, m => m.MapFrom(o => o.TreeName))
                .ForMember(m => m.Value, m => m.MapFrom(o => o.Id))
                .ForMember(m => m.Key, m => m.MapFrom(o => o.Key))
                .ForMember(m => m.IsLeaf, m => m.MapFrom(o => o.TreeLeaf));
        }
    }
}
