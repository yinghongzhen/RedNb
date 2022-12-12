using Volo.Abp.Localization;

namespace RedNb.Gateway;

[DependsOn(
    typeof(GatewayApplicationContractsModule),
    typeof(GatewayDomainModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpLocalizationModule)
    )]
public class GatewayApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<GatewayApplicationModule>();
        });
    }
}
