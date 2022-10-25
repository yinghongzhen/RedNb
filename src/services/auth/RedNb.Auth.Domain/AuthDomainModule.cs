using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace RedNb.Auth;

[DependsOn(
    typeof(AuthDomainSharedModule),
    typeof(AbpAuditLoggingDomainModule),
    typeof(AbpBackgroundJobsDomainModule),
    typeof(AbpFeatureManagementDomainModule),
    typeof(AbpIdentityDomainModule),
    typeof(AbpOpenIddictDomainModule),
    typeof(AbpPermissionManagementDomainOpenIddictModule),
    typeof(AbpPermissionManagementDomainIdentityModule),
    typeof(AbpSettingManagementDomainModule),
    typeof(AbpTenantManagementDomainModule),
    typeof(AbpEmailingModule)
)]
public class AuthDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        AuthDomainErrorCodes.
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = false;
        });
    }
}
