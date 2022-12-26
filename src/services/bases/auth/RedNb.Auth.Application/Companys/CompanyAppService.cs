using RedNb.Auth.Application.Contracts.Companys;
using RedNb.Auth.Application.Contracts.Companys.Dtos;
using RedNb.Auth.Domain.Companys;
using RedNb.Core.Domain;

namespace RedNb.Auth.Application.Companys;

public class CompanyAppService : TreeAppService<Company, CompanyAddInputDto, CompanyUpdateInputDto, CompanyOutputDto>,
    ICompanyAppService
{
    private readonly IRepository<Company, long> _companyRepository;
    private readonly IObjectMapper _objectMapper;

    public CompanyAppService(
        IRepository<Company, long> companyRepository,
        IObjectMapper objectMapper) :
        base(companyRepository, objectMapper)
    {
        _companyRepository = companyRepository;
        _objectMapper = objectMapper;
    }
}