using RedNb.WebGateway.Domain.Shared.Localization;

namespace RedNb.WebGateway.Domain.Shared;

[DependsOn()]
public class WebGatewayDomainSharedModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WebGatewayDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<WebGatewayResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/WebGateway");

            options.DefaultResourceType = typeof(WebGatewayResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("WebGateway", typeof(WebGatewayResource));
        });
    }
}
