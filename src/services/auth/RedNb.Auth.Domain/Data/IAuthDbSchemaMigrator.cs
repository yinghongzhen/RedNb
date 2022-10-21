using System.Threading.Tasks;

namespace RedNb.Auth.Data;

public interface IAuthDbSchemaMigrator
{
    Task MigrateAsync();
}
