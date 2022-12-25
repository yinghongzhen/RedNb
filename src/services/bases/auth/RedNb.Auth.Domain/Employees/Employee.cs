using RedNb.Auth.Domain.Companys;
using RedNb.Auth.Domain.Departments;
using RedNb.Auth.Domain.Tenants;
using RedNb.Auth.Domain.Users;

namespace RedNb.Auth.Domain.Employees;

/// <summary>
/// 员工实体类
/// </summary>
[Table("Employee")]
public class Employee : BaseAggregateRoot
{
    /// <summary>
    /// 姓名
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// 英文名
    /// </summary>
    [MaxLength(100)]
    public string EnName { get; set; }

    /// <summary>
    /// 工号
    /// </summary>
    [MaxLength(100)]
    public string Number { get; set; }

    /// <summary>
    /// 入职时间
    /// </summary>
    public DateTime? JoinDate { get; set; }

    [Required]
    public EEmployeeStatus Status { get; set; }

    public long DepartmentId { get; set; }

    public virtual Department Department { get; set; }

    public long? CompanyId { get; set; }

    public virtual Company Company { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; }

    [Required]
    public long TenantId { get; set; }

    public virtual Tenant Tenant { get; set; }
}
