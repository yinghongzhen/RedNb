using JetBrains.Annotations;
using Volo.Abp;

namespace RedNb.Gateway.Domain.Clusters;

public class ClusterManager : DomainService
{
    private readonly IRepository<Cluster, long> _clusterRepository;

    public ClusterManager(IRepository<Cluster, long> clusterRepository)
    {
        _clusterRepository = clusterRepository;
    }

    public async Task<Cluster> CreateAsync(string name)
    {
        if (await _clusterRepository.AnyAsync(x => x.Name == name))
        {
            throw new UserFriendlyException(code: "Welcome");
        }

        return await _clusterRepository.InsertAsync(
                new Cluster(
                    IdentityManager.NewId(),
                    name,
                    "1"
                )
            );
    }
}