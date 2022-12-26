using RedNb.Auth.Domain.Tenants;
using Volo.Abp;

namespace RedNb.Auth.Domain.Companys;

/// <summary>
/// 公司实体类
/// </summary>
[Table("Company")]
public class Company : TreeAggregateRoot, ISoftDelete
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

    /// <summary>
    /// 是否启用
    /// </summary>
    [Required]
    public bool IsActive { get; set; }

    /// <summary>
    /// 是否删除
    /// </summary>
    [Required]
    public bool IsDeleted { get; }

    /// <summary>
    /// 租户编号
    /// </summary>
    [Required]
    public long TenantId { get; set; }

    public virtual Tenant Tenant { get; set; }

    //public virtual List<CompanyDepartment> CompanyDepartments { get; set; }
}
