using RedNb.Auth.Domain.Tenants;

namespace RedNb.Auth.Domain.Roles;

/// <summary>
/// 角色实体类
/// </summary>
[Table("Role")]
public class Role : BaseAggregateRoot
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

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    [Required]
    public long TenantId { get; set; }

    public virtual Tenant Tenant { get; set; }
}
