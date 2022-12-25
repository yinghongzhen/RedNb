using RedNb.Core.Domain.Shared;

namespace RedNb.Auth.Domain.Services;

/// <summary>
/// 接口实体类
/// </summary>
[Table("Api")]
public class Api : BaseEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string Path { get; set; }

    /// <summary>
    /// 谓词
    /// </summary>
    [Required]
    public EHttpMethod Method { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Required]
    public bool IsEnabled { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Tags { get; set; }

    /// <summary>
    /// 服务编号
    /// </summary>
    public long ServiceId { get; set; }
}
