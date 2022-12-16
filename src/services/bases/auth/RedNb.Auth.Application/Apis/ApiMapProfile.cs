using AutoMapper;
using RedNb.Auth.Application.Contracts.Apis.Dtos;
using RedNb.Auth.Domain.Services;
using RedNb.Core.Extensions;

namespace RedNb.Auth.Application.Apis
{
    public class ApiMapProfile : Profile
    {
        public ApiMapProfile()
        {
            CreateMap<ApiAddInputDto, Api>();

            CreateMap<ApiUpdateInputDto, Api>();

            CreateMap<Api, ApiOutputDto>()
                .ForMember(m => m.MethodStr, m => m.MapFrom(o => o.Method.GetDescription()));
        }
    }
}
