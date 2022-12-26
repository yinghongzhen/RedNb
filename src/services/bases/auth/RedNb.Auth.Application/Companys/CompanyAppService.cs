using RedNb.Auth.Application.Contracts.Companys;
using RedNb.Auth.Application.Contracts.Companys.Dtos;
using RedNb.Auth.Domain.Companys;
using RedNb.Core.Domain;
using RedNb.Gateway.Domain.Companys;

namespace RedNb.Auth.Application.Companys;

public class CompanyAppService : TreeAppService<Company, CompanyAddInputDto, CompanyUpdateInputDto, CompanyOutputDto>,
    ICompanyAppService
{
    private readonly CompanyManager _companyManager;
    private readonly IRepository<Company, long> _companyRepository;
    private readonly IObjectMapper _objectMapper;

    public CompanyAppService(
        IRepository<Company, long> companyRepository,
        IObjectMapper objectMapper,
        CompanyManager companyManager) :
        base(companyManager, objectMapper, companyRepository)
    {
        _companyRepository = companyRepository;
        _objectMapper = objectMapper;
        _companyManager = companyManager;
    }
}