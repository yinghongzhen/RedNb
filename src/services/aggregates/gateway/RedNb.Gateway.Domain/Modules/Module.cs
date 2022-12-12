namespace RedNb.Auth.Domain.Modules;

/// <summary>
/// 模块实体类
/// </summary>
[Table("Module")]
public class Module : AggregateRoot<long>
{
    /// <summary>
    /// 类型
    /// </summary>
    [Required]
    public EModuleType Type { get; set; }

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
