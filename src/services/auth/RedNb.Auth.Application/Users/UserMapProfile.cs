using AutoMapper;
using RedNb.Auth.Application.Contracts.Users.Dtos;
using RedNb.Auth.Domain.Admins;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Users
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserAddInputDto, User>();

            CreateMap<UserUpdateInputDto, User>();

            CreateMap<User, UserOutputDto>()
                .ForMember(m => m.ManagerTypeStr, m => m.MapFrom(o => o.ManagerType.GetDescription()))
                .ForMember(m => m.SexStr, m => m.MapFrom(o => o.Sex.GetDescription()));

            CreateMap<UserOutputDto, User>();

            CreateMap<UserRoleAddInputDto, UserRole>();

            CreateMap<UserRole, UserRoleOutputDto>();
        }
    }
}
