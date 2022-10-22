using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace RedNb.Base.Data;

/* This is used if database provider does't define
 * IBaseDbSchemaMigrator implementation.
 */
public class NullBaseDbSchemaMigrator : IBaseDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
