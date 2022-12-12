using System.ComponentModel.DataAnnotations;

namespace RedNb.Gateway.Application.Contracts.Clusters;

public class ClusterAddInputDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}