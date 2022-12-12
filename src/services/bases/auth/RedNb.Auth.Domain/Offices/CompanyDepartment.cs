namespace RedNb.Auth.Domain.Offices;

/// <summary>
/// 公司部门实体类
/// </summary>
[Table("CompanyDepartment")]
public class CompanyDepartment : Entity
{
    /// <summary>
    /// 部门编号
    /// </summary>
    public long DepartmentId { get; set; }

    public virtual Department Department { get; set; }

    /// <summary>
    /// 公司编号
    /// </summary>
    public long CompanyId { get; set; }

    public virtual Company Company { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { DepartmentId, CompanyId };
    }
}
