using Microsoft.Extensions.DependencyInjection;

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
        context.Services.AddAutoMapperObjectMapper<WebGatewayApplicationModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<WebGatewayApplicationModule>(validate: false);
        });
    }
}
