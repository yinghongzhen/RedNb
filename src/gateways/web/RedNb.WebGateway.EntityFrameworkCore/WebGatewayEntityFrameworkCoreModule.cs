using RedNb.WebGateway.Domain;

namespace RedNb.WebGateway.EntityFrameworkCore;

[DependsOn(
    typeof(WebGatewayDomainModule),
    typeof(AbpEntityFrameworkCoreMySQLModule)
    )]
public class WebGatewayEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<WebGatewayDbContext>(options =>
        {
            options.AddDefaultRepositories();
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseMySQL();
        });
    }
}
