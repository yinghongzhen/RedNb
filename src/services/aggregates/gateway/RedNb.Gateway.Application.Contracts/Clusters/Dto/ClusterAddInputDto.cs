using RedNb.Gateway.Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace RedNb.Gateway.Application.Contracts.Clusters;

public class ClusterAddInputDto
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public EHealthStatus Status { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public int Count { get; set; }
}