using RedNb.Auth.Application.Contracts.Platforms.Dtos;
using RedNb.Auth.Domain.Platforms;

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
