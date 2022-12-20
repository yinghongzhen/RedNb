using Volo.Abp.Domain.Entities;

namespace RedNb.Auth.Domain.Tenants;

/// <summary>
/// 产品实体类
/// </summary>
[Table("Product")]
public class Product : BaseAggregateRoot
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

    /// <summary>
    /// 排序号
    /// </summary>
    [Required]
    public decimal Sort { get; set; }
}
