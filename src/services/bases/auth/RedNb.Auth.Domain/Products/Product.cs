
using RedNb.Auth.Domain.Platforms;

namespace RedNb.Auth.Domain.Products;

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

    public virtual ICollection<Platform> Platforms { get; set; }
}