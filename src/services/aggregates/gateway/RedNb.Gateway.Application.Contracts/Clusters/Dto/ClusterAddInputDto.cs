using RedNb.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace RedNb.Gateway.Application.Contracts.Clusters;

public class ClusterAddInputDto : TreeAddInputDto
{
    [Required]
    [MaxLength(100)]
    public string Path { get; set; }
}