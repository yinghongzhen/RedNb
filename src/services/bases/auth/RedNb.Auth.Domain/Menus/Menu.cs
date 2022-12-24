using RedNb.Auth.Domain.Platforms;

namespace RedNb.Auth.Domain.Menus;

/// <summary>
/// 权限实体类
/// </summary>
[Table("Menu")]
public class Menu : TreeAggregateRoot
{
    /// <summary>
    /// 权限类型
    /// </summary>
    [Required]
    public EPermissionType Type { get; set; }

    /// <summary>
    /// 权限编码
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Key { get; set; }

    /// <summary>
    /// 路由地址
    /// </summary>
    [MaxLength(200)]
    public string Path { get; set; }

    /// <summary>
    /// 路由组件
    /// </summary>
    [MaxLength(200)]
    public string Component { get; set; }

    /// <summary>
    /// 附带参数
    /// </summary>
    [MaxLength(1000)]
    public string Params { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    [MaxLength(100)]
    public string Icon { get; set; }

    /// <summary>
    /// 颜色
    /// </summary>
    [MaxLength(50)]
    public string Color { get; set; }

    /// <summary>
    /// 是否显示
    /// </summary>
    [Required]
    public bool IsShow { get; set; }

    /// <summary>
    /// 是否增值服务
    /// </summary>
    public bool IsVas { get; set; }

    /// <summary>
    /// 允许关闭
    /// </summary>
    [Required]
    public bool Closeable { get; set; }

    /// <summary>
    /// 备注信息
    /// </summary>
    [MaxLength(200)]
    public string Desc { get; set; }

    /// <summary>
    /// 使用状态
    /// </summary>
    [Required]
    public EUsageStatus Status { get; set; }

    [Required]
    public long PlatformId { get; set; }

    public virtual Platform Platform { get; set; }
}
