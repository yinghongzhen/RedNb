namespace RedNb.Auth.Domain.Tenants;

/// <summary>
/// 产品实体类
/// </summary>
[Table("Product")]
public class Product : AggregateRoot<long>
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
