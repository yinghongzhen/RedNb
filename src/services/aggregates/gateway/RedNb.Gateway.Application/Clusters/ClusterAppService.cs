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
        var model =  _objectMapper.Map<ClusterAddInputDto, Cluster>(input);

        await _clusterManager.AddAsync(model);
    }

    /// <summary>
    /// 更新集群
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task UpdateAsync(ClusterUpdateInputDto input)
    {
        var model = _objectMapper.Map<ClusterUpdateInputDto, Cluster>(input);

        await _clusterManager.UpdateAsync(model);
    }

    public async Task<List<ClusterOutputDto>> GetListAsync()
    {
        var list = await _clusterRepository.GetListAsync();

        return _objectMapper.Map<List<Cluster>, List<ClusterOutputDto>>(list);
    }
}
