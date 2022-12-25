namespace RedNb.Auth.Domain.Dicts;

/// <summary>
/// 字典数据实体类
/// </summary>
[Table("DictData")]
public class DictData : BaseEntity
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
    [MaxLength(100)]
    public string Key { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Value { get; set; }

    public long DictTypeId { get; set; }
}
