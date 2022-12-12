namespace RedNb.Gateway.Application.Contracts.Clusters;

public interface IClusterAppService : IApplicationService, ITransientDependency
{
    public Task AddAsync(ClusterAddInputDto input);

    public Task<List<ClusterOutputDto>> GetListAsync();
}