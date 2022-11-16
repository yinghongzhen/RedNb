using RedNb.WebGateway.Domain.Shared;

namespace RedNb.WebGateway.Application.Contracts;

[DependsOn(
    typeof(WebGatewayDomainSharedModule),
    typeof(AbpObjectExtendingModule)
)]
public class WebGatewayApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {

    }
}
