using RedNb.Core.Contracts;
using Volo.Abp.Application.Dtos;

namespace RedNb.Gateway.Application.Contracts.Clusters;

public class ClusterOutputDto : EntityBaseDto
{
    public string Name { get; set; }
}