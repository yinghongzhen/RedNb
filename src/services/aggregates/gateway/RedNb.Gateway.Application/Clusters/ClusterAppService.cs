using RedNb.Gateway.Application.Contracts.Clusters;
using RedNb.Gateway.Domain.Clusters;

namespace RedNb.Gateway.Application.Clusters;

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

    /// <summary>
    /// 添加集群
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task AddAsync(ClusterAddInputDto input)
    {
        await _clusterManager.CreateAsync(input.Name);
    }

    public async Task UpdateAsync(ClusterUpdateInputDto input)
    {
        var cluster = await _clusterRepository.GetAsync(input.Id);
    }

    public async Task<List<ClusterOutputDto>> GetListAsync()
    {
        var list = await _clusterRepository.GetListAsync();

        return _objectMapper.Map<List<Cluster>, List<ClusterOutputDto>>(list);
    }
}
