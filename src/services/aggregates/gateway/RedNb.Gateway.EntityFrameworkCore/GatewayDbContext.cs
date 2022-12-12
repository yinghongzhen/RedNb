using RedNb.Gateway.Domain.Clusters;
using RedNb.Gateway.Domain.Routes;

namespace RedNb.Gateway.EntityFrameworkCore;

public class GatewayDbContext : AbpDbContext<GatewayDbContext>
{
    public GatewayDbContext(DbContextOptions<GatewayDbContext> options)
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