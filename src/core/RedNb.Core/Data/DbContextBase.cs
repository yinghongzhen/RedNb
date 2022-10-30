using RedNb.Core.Application;
using RedNb.Core.Domain;
using RedNb.Core.Domain.Audit;
using RedNb.Core.Extensions;

namespace RedNb.Core.Data;

public class DbContextBase<TDbContext> : AbpDbContext<TDbContext> where TDbContext : DbContext
{
    public LoginUser LoginUser { get; set; }

    public DbContextBase(DbContextOptions<TDbContext> options)
        : base(options)
    {

    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var entries = base.ChangeTracker.Entries();

        var milliSecond = 0;
        var now = DateTime.Now;

        foreach (var item in entries)
        {
            milliSecond++;

            var currentTime = now.AddMilliseconds(milliSecond);

            if (item.State == EntityState.Added)
            {
                if (item.Entity is AuditFullEntity)
                {
                    var entity = (AuditFullEntity)item.Entity;

                    entity.CreateTime = currentTime;
                    entity.UpdateTime = currentTime;

                    if (LoginUser != null && LoginUser.IsValid())
                    {
                        entity.CreateId = LoginUser.UserId;
                        entity.CreateName = LoginUser.Username;
                        entity.UpdateId = LoginUser.UserId;
                        entity.UpdateName = LoginUser.Username;
                    }
                    else
                    {
                        entity.CreateName = "-";
                        entity.UpdateName = "-";
                    }
                }

                if (item.Entity is IHasTenant)
                {
                    var entity = (IHasTenant)item.Entity;

                    if (LoginUser != null && LoginUser.IsValid() && entity.TenantId == 0)
                    {
                        entity.TenantId = LoginUser.TenantId;
                    }
                }

                if (item.Entity is IHasConcurrency)
                {
                    var entity = (IHasConcurrency)item.Entity;

                    entity.RowVersion = currentTime;
                }

                if (item.Entity is ISoftDelete)
                {
                    var entity = (ISoftDelete)item.Entity;

                    entity.IsDeleted = false;
                }
            }
            else if (item.State == EntityState.Modified)
            {
                if (item.Entity is AuditFullEntity)
                {
                    var entity = (AuditFullEntity)item.Entity;

                    entity.UpdateTime = currentTime;

                    if (LoginUser != null && LoginUser.IsValid())
                    {
                        entity.UpdateId = LoginUser.UserId;
                        entity.UpdateName = LoginUser.Username;
                    }
                }

                if (item.Entity is IHasConcurrency)
                {
                    var entity = (IHasConcurrency)item.Entity;

                    entity.RowVersion = currentTime;
                }
            }
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ReplaceService<IModelCacheKeyFactory, TenantModelCacheKeyFactory<TDbContext>>();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        builder.EnableQueryFilter(LoginUser?.TenantId);
    }
}
