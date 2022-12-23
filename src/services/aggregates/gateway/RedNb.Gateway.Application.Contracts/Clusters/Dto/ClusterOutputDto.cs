using RedNb.Core.Contracts;

namespace RedNb.Gateway.Application.Contracts.Clusters;

public class ClusterOutputDto : TreeOutputDto<ClusterOutputDto>
{
    public string Path { get; set; }
}