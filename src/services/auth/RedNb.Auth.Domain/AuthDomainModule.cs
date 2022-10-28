using RedNb.Auth.Domain.Shared;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace RedNb.Auth.Domain;

[DependsOn(
    typeof(AuthDomainSharedModule)
)]
public class AuthDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        
    }
}
