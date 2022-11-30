using RedNb.WebGateway.Domain.Clusters;
using RedNb.WebGateway.Domain.Tests;

namespace RedNb.WebGateway.EntityFrameworkCore;

public class WebGatewayDbContext : AbpDbContext<WebGatewayDbContext>
{
    public WebGatewayDbContext(DbContextOptions<WebGatewayDbContext> options)
        : base(options)
    {

    }

    public DbSet<Test> Tests { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}