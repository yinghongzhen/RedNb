using RedNb.Core.Data;
using RedNb.Auth.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace RedNb.Auth.EntityFrameworkCore;

public class AuthDbContext : BaseDbContext<AuthDbContext>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Platform> Platforms { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>(b =>
        {
            b.ConfigureByConvention();

            b.HasMany(u => u.Platforms).WithOne().HasForeignKey(uc => uc.ProductId).IsRequired();

            b.ApplyObjectExtensionMappings();
        });
    }
}