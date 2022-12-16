using AutoMapper;
using RedNb.Auth.Application.Contracts.Configs.Dtos;
using RedNb.Auth.Domain.Configs;
using RedNb.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Configs
{
    public class ConfigMapProfile : Profile
    {
        public ConfigMapProfile()
        {
            CreateMap<ConfigAddInputDto, Config>();

            CreateMap<ConfigUpdateInputDto, Config>();

            CreateMap<Config, ConfigOutputDto>();
        }
    }
}
