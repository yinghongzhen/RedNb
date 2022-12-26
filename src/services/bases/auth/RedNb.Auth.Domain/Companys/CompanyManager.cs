using JetBrains.Annotations;
using RedNb.Auth.Domain.Companys;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace RedNb.Gateway.Domain.Companys;

public class CompanyManager : TreeService<Company>
{
    public CompanyManager(IRepository<Company, long> treeEntityRepository) : base(treeEntityRepository)
    {
    }

    public override async Task AddAsync(Company input)
    {
        input.IsActive = true;
        
        await base.AddAsync(input);
    }
}