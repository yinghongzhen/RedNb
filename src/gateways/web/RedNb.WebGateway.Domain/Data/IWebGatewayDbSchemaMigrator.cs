using System.Threading.Tasks;

namespace RedNb.WebGateway.Data;

public interface IWebGatewayDbSchemaMigrator
{
    Task MigrateAsync();
}
