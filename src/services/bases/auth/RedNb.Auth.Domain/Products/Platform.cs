using JetBrains.Annotations;

namespace RedNb.Auth.Domain.Products;

/// <summary>
/// 平台实体类
/// </summary>
[Table("Platform")]
public class Platform : BaseEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public long ProductId { get; set; }

    public virtual Product Product { get; set; }

    protected Platform() { }
}