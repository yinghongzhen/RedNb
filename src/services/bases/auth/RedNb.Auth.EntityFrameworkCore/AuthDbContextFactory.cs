namespace RedNb.Auth.EntityFrameworkCore;

public class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
{
    public AuthDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<AuthDbContext>()
                .UseMySql(
                configuration.GetConnectionString("Default"),
                new MySqlServerVersion(new Version(8, 0, 24)));

        return new AuthDbContext(builder.Options);
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
