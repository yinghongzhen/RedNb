namespace RedNb.Auth.Domain.Dicts;

/// <summary>
/// 字典类型实体类
/// </summary>
[Table("DictType")]
public class DictType : AggregateRootBase
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Type { get; set; }

    /// <summary>
    /// 是否系统字典
    /// </summary>
    [Required]
    public bool IsSystem { get; set; }
}
