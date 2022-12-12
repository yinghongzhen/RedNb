using AutoMapper;
using RedNb.Auth.Application.Contracts.Platforms.Dtos;
using RedNb.Auth.Domain.Admins;
using RedNb.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Platforms
{
    public class PlatformMapProfile : Profile
    {
        public PlatformMapProfile()
        {
            CreateMap<PlatformAddInputDto, Platform>();

            CreateMap<PlatformUpdateInputDto, Platform>();

            CreateMap<Platform, PlatformOutputDto>();
        }
    }
}
