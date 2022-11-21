using RedNb.WebGateway.Domain;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;

namespace RedNb.WebGateway.EntityFrameworkCore;

[DependsOn(
    typeof(WebGatewayDomainModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule)
    )]
public class WebGatewayEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = configuration["ConnectionStrings:Default"];
        });

        context.Services.AddAbpDbContext<WebGatewayDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseNpgsql();
        });
    }
}
