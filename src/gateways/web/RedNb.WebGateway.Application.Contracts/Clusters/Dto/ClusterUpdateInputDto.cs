using System.ComponentModel.DataAnnotations;

namespace RedNb.WebGateway.Application.Contracts.Clusters;

public class ClusterUpdateInputDto
{
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}