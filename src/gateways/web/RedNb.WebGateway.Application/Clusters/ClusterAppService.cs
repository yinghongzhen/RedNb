using RedNb.WebGateway.Application.Contracts.Clusters;
using RedNb.WebGateway.Domain.Clusters;
using Volo.Abp.Application.Dtos;

namespace RedNb.WebGateway.Application.Clusters;

public class ClusterAppService : IClusterAppService
{
    private readonly ClusterManager _clusterManager;
    private readonly IRepository<Cluster, long> _clusterRepository;
    private readonly IObjectMapper _objectMapper;

    public ClusterAppService(
        IRepository<Cluster, long> ClusterRepository,
        IObjectMapper objectMapper,
        ClusterManager clusterManager)
    {
        _clusterRepository = ClusterRepository;
        _objectMapper = objectMapper;
        _clusterManager = clusterManager;
    }

    public async Task AddAsync(ClusterAddInputDto input)
    {
        await _clusterManager.CreateAsync(input.Name);
    }

    public async Task UpdateAsync(ClusterUpdateInputDto input)
    {
        var cluster = await _clusterRepository.GetAsync(input.Id);

        cluster.SetName(input.Name);

        //await _clusterRepository.UpdateAsync(cluster);
    }

    public async Task<List<ClusterOutputDto>> GetListAsync()
    {
        var list = await _clusterRepository.GetListAsync();

        return _objectMapper.Map<List<Cluster>, List<ClusterOutputDto>>(list);
    }
}
