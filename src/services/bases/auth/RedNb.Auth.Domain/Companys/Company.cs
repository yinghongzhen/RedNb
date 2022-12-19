using RedNb.Auth.Domain.Tenants;

namespace RedNb.Auth.Domain.Offices;

/// <summary>
/// 公司实体类
/// </summary>
[Table("Company")]
public class Company : AggregateRoot<long>
{
    /// <summary>
    /// 编码
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Key { get; set; }

    /// <summary>
    /// 公司全称
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string FullName { get; set; }

    /// <summary>
    /// 区域编码
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string AreaCode { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// 租户编号
    /// </summary>
    [Required]
    public long TenantId { get; set; }

    public virtual Tenant Tenant { get; set; }
}
