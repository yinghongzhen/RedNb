namespace RedNb.Gateway.Application.Contracts;

[DependsOn(
    typeof(GatewayDomainSharedModule),
    typeof(AbpObjectExtendingModule)
)]
public class GatewayApplicationContractsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

    }
}
