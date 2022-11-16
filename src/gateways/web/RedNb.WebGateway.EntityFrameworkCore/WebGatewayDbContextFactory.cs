namespace RedNb.WebGateway.EntityFrameworkCore;

public class WebGatewayDbContextFactory : IDesignTimeDbContextFactory<WebGatewayDbContext>
{
    public WebGatewayDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<WebGatewayDbContext>()
            .UseMySql(
                configuration.GetConnectionString("Default"),
                new MySqlServerVersion(new Version(8, 0, 27)));

        return new WebGatewayDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
