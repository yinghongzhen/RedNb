using RedNb.Auth.Domain.Departments;

namespace RedNb.Auth.Domain.Companys;

/// <summary>
/// 公司部门实体类
/// </summary>
[Table("CompanyDepartment")]
public class CompanyDepartment : Entity
{
    /// <summary>
    /// 公司编号
    /// </summary>
    public long CompanyId { get; set; }

    public virtual Company Company { get; set; }

    /// <summary>
    /// 部门编号
    /// </summary>
    public long DepartmentId { get; set; }

    public virtual Department Department { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { DepartmentId, CompanyId };
    }
}
