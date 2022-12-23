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
