using Microsoft.Extensions.DependencyInjection;
using RedNb.Auth.Application.Contracts;
using RedNb.Auth.Data;
using RedNb.Auth.Domain;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace RedNb.Auth.Application
{
    [DependsOn(
        typeof(AuthDomainModule),
        typeof(AbpAutoMapperModule),
        typeof(AuthApplicationContractsModule))]
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
}
