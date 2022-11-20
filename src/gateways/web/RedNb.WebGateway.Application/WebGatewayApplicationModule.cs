namespace RedNb.WebGateway;

[DependsOn(
    typeof(WebGatewayApplicationContractsModule),
    typeof(WebGatewayDomainModule),
    typeof(AbpAutoMapperModule)
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
