namespace RedNb.WebGateway.EntityFrameworkCore;

public class WebGatewayDbContextFactory : IDesignTimeDbContextFactory<WebGatewayDbContext>
{
    public WebGatewayDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var conn = configuration.GetConnectionString("Default");

        Console.WriteLine(conn);

        var builder = new DbContextOptionsBuilder<WebGatewayDbContext>()
            .UseMySql(conn,
                new MySqlServerVersion(new Version(8, 0, 27)));

        return new WebGatewayDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{envName}.json", optional: false)
            .AddUserSecrets(typeof(WebGatewayDbContext).Assembly, optional: false);

        return builder.Build();
    }
}
