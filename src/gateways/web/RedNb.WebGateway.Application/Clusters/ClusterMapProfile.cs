using RedNb.WebGateway.Application.Contracts.Clusters;
using RedNb.WebGateway.Domain.Clusters;

namespace RedNb.WebGateway.Application.Clusters;

public class ClusterMapProfile : Profile
{
    public ClusterMapProfile()
    {
        CreateMap<ClusterAddInputDto, Cluster>();

        CreateMap<Cluster, ClusterOutputDto>();
    }
}