using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace RedNb.WebGateway.Data;

/* This is used if database provider does't define
 * IWebGatewayDbSchemaMigrator implementation.
 */
public class NullWebGatewayDbSchemaMigrator : IWebGatewayDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
