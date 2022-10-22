using System.Threading.Tasks;

namespace RedNb.Base.Data;

public interface IBaseDbSchemaMigrator
{
    Task MigrateAsync();
}
