namespace RedNb.Auth.Domain.Views;

/// <summary>
/// 视图实体类
/// </summary>
[Table("View")]
public class View : BaseAggregateRoot
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