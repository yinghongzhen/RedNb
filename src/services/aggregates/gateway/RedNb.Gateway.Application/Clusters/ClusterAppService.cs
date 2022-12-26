using RedNb.Core.Domain;
using RedNb.Gateway.Application.Contracts.Clusters;
using RedNb.Gateway.Domain.Clusters;

namespace RedNb.Gateway.Application.Clusters;


public class ClusterAppService : TreeAppService<Cluster, ClusterAddInputDto, ClusterUpdateInputDto, ClusterOutputDto>,
    IClusterAppService
{
    private readonly IRepository<Cluster, long> _clusterRepository;
    private readonly IObjectMapper _objectMapper;

    public ClusterAppService(
        IRepository<Cluster, long> clusterRepository,
        IObjectMapper objectMapper) :
        base(clusterRepository, objectMapper)
    {
        _clusterRepository = clusterRepository;
        _objectMapper = objectMapper;
    }
}
