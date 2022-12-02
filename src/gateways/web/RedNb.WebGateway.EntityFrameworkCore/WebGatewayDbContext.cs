using RedNb.WebGateway.Domain.Clusters;
using RedNb.WebGateway.Domain.Routes;

namespace RedNb.WebGateway.EntityFrameworkCore;

public class WebGatewayDbContext : AbpDbContext<WebGatewayDbContext>
{
    public WebGatewayDbContext(DbContextOptions<WebGatewayDbContext> options)
        : base(options)
    {

    }

    public DbSet<Cluster> Clusters { get; set; }
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<MetaData> MetaDatas { get; set; }

    public DbSet<Match> Matchs { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Transform> Transforms { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}