namespace RedNb.Auth.Application.Contracts;

[DependsOn(
    typeof(AuthDomainSharedModule),
    typeof(AbpObjectExtendingModule)
)]
public class AuthApplicationContractsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        
    }
}
