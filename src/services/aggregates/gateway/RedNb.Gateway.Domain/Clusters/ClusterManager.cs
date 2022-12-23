using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.ObjectMapping;

namespace RedNb.Gateway.Domain.Clusters;

public class ClusterManager : TreeService<Cluster>
{
    public ClusterManager(IRepository<Cluster, long> treeEntityRepository) : base(treeEntityRepository)
    {
    }
}