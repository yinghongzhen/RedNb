namespace RedNb.Gateway.Domain.Services;

/// <summary>
/// 视图实体类
/// </summary>
[Table("View")]
public class View : AggregateRootBase
{
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
}