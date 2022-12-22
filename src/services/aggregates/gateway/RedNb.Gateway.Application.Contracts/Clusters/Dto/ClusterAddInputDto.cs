using RedNb.Core.Contracts;
using RedNb.Gateway.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace RedNb.Gateway.Application.Contracts.Clusters;

public class ClusterAddInputDto : TreeAddInputDto
{
    [Required]
    [MaxLength(100)]
    public string Path { get; set; }
}