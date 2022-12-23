using RedNb.Auth.Domain;

namespace RedNb.Auth.EntityFrameworkCore;

[DependsOn(
    typeof(AuthDomainModule),
    typeof(AbpEntityFrameworkCoreMySQLModule)
    )]
public class AuthEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = configuration["ConnectionStrings:Default"];
        });

        context.Services.AddAbpDbContext<AuthDbContext>(options =>
        {
            options.AddDefaultRepositories();
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseMySQL();
        });
    }
}
