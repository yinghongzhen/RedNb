namespace RedNb.Gateway.Domain.Services;

/// <summary>
/// 服务实体类
/// </summary>
[Table("View")]
public class View : AggregateRootBase
{
    /// <summary>
    /// 类型
    /// </summary>
    [Required]
    public EServiceType Type { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string Key { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required]
    public decimal Sort { get; set; }
}