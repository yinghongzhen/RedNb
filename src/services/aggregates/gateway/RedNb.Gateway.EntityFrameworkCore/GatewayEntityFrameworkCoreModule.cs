using RedNb.Gateway.Domain;

namespace RedNb.Gateway.EntityFrameworkCore;

[DependsOn(
    typeof(GatewayDomainModule),
    typeof(AbpEntityFrameworkCoreMySQLModule)
    )]
public class GatewayEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = configuration["ConnectionStrings:Default"];
        });

        context.Services.AddAbpDbContext<GatewayDbContext>(options =>
        {
            options.AddDefaultRepositories();
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseMySQL();
        });
    }
}
