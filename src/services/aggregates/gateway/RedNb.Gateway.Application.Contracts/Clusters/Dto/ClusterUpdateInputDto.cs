using RedNb.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace RedNb.Gateway.Application.Contracts.Clusters;

public class ClusterUpdateInputDto : TreeUpdateInputDto
{
    [Required]
    [MaxLength(100)]
    public string Path { get; set; }
}