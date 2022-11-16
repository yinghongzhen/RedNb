namespace RedNb.WebGateway;

[DependsOn(
    typeof(AbpAutoMapperModule),
    typeof(WebGatewayApplicationContractsModule),
    typeof(WebGatewayDomainModule)
    )]
public class WebGatewayApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<WebGatewayApplicationModule>();
        });
    }
}
