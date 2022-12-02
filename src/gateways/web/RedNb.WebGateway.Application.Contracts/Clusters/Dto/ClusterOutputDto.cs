using RedNb.Core.Contracts;
using Volo.Abp.Application.Dtos;

namespace RedNb.WebGateway.Application.Contracts.Clusters;

public class ClusterOutputDto : EntityBaseDto
{
    public string Name { get; set; }
}