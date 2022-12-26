using RedNb.Auth.Application.Contracts.Platforms.Dtos;
using RedNb.Core.Contracts;

namespace RedNb.Auth.Application.Contracts.Platforms;

public interface IPlatformAppService : IApplicationService, ITransientDependency
{
    Task AddAsync(PlatformAddInputDto input);

    Task DeleteBatchAsync(DeleteBatchInputDto input);

    Task UpdateAsync(PlatformUpdateInputDto input);
}