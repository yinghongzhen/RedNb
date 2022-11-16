using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RedNb.WebGateway.Data;
using Volo.Abp.DependencyInjection;

namespace RedNb.WebGateway;

public class EntityFrameworkCoreWebGatewayDbSchemaMigrator
    : IWebGatewayDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreWebGatewayDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the WebGatewayDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<WebGatewayDbContext>()
            .Database
            .MigrateAsync();
    }
}
