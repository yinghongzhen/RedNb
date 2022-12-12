using RedNb.Gateway.Application.Contracts.Clusters;
using RedNb.Gateway.Domain.Clusters;

namespace RedNb.Gateway.Application.Clusters;

public class ClusterMapProfile : Profile
{
    public ClusterMapProfile()
    {
        CreateMap<ClusterAddInputDto, Cluster>();

        CreateMap<ClusterUpdateInputDto, Cluster>();

        CreateMap<Cluster, ClusterOutputDto>();
    }
}