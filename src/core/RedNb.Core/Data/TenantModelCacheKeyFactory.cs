
using Microsoft.EntityFrameworkCore.Metadata;
using RedNb.Core.Application;
using RedNb.Core.Contracts;
using RedNb.Core.Domain;
using System.Linq.Expressions;
using Volo.Abp.EntityFrameworkCore;

namespace RedNb.Core.Data;

public class TenantModelCacheKeyFactory<T> : IModelCacheKeyFactory where T : DbContext
{
    public object Create(DbContext context, bool designTime) =>
        context is BaseDbContext<T> dynamicContext
        ? (context.GetType(), dynamicContext.LoginUser?.TenantId, designTime)
        : (object)context.GetType();
}
