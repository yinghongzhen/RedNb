namespace RedNb.Auth.Application;

[DependsOn(
    typeof(AuthApplicationContractsModule),
    typeof(AuthDomainModule),
    typeof(AbpAutoMapperModule)
    )]
public class AuthApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AuthApplicationModule>();
        });
    }
}
