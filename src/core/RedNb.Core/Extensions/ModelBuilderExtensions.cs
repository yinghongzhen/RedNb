using RedNb.Core.Domain;

namespace RedNb.Core.Extensions;

public static class ModelBuilderExtensions
{
    public static void EnableQueryFilter(this ModelBuilder builder, long? tenantId)
    {
        if (tenantId != null)
        {
            var entityTypes = builder.Model.GetEntityTypes();

            foreach (var entityType in entityTypes)
            {
                ParameterExpression parameter = Expression.Parameter(entityType.ClrType, "e");

                BinaryExpression be = null;

                if (typeof(IHasTenant).IsAssignableFrom(entityType.ClrType))
                {
                    BinaryExpression body = Expression.Equal(
                        Expression.Call(typeof(EF),
                        nameof(EF.Property), new[] { typeof(long) },
                        parameter,
                        Expression.Constant("TenantId")),
                        Expression.Constant(tenantId));

                    if (be == null)
                    {
                        be = body;
                    }
                    else
                    {
                        be = Expression.AndAlso(be, body);
                    }
                }

                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    BinaryExpression body = Expression.Equal(
                        Expression.Call(typeof(EF),
                        nameof(EF.Property), new[] { typeof(bool) },
                        parameter,
                        Expression.Constant("IsDeleted")),
                        Expression.Constant(false));

                    if (be == null)
                    {
                        be = body;
                    }
                    else
                    {
                        be = Expression.AndAlso(be, body);
                    }
                }

                if (be != null)
                {
                    builder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(be, parameter));
                }
            }
        }
    }
}
