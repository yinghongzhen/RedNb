using RedNb.Auth.Application.Contracts.Companys.Dtos;
using RedNb.Auth.Domain.Companys;

namespace RedNb.Auth.Application.Companys;

public class CompanyMapProfile : Profile
{
    public CompanyMapProfile()
    {
        CreateMap<CompanyAddInputDto, Company>();

        CreateMap<CompanyUpdateInputDto, Company>();

        CreateMap<Company, CompanyOutputDto>();
    }
}
