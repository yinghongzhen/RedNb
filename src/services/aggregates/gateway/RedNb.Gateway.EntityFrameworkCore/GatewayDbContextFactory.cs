namespace RedNb.Gateway.EntityFrameworkCore;

public class GatewayDbContextFactory : IDesignTimeDbContextFactory<GatewayDbContext>
{
    public GatewayDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<GatewayDbContext>()
                .UseMySql(
                configuration.GetConnectionString("Default"),
                new MySqlServerVersion(new Version(8, 0, 24)));

        return new GatewayDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: false)
            .AddJsonFile("appsettings.secrets.json", optional: false);

        return builder.Build();
    }
}
