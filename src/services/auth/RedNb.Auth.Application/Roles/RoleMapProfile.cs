using AutoMapper;
using RedNb.Auth.Application.Contracts.Roles.Dtos;
using RedNb.Auth.Domain.Admins;
using RedNb.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Roles
{
    public class RoleMapProfile : Profile
    {
        public RoleMapProfile()
        {
            CreateMap<RoleAddInputDto, Role>();

            CreateMap<RoleUpdateInputDto, Role>();

            CreateMap<Role, RoleOutputDto>();
        }
    }
}
