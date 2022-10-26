namespace RedNb.Auth.Domain.Offices;

/// <summary>
/// 员工附属部门关联实体类
/// </summary>
[Table("EmployeeDepartment")]
public class EmployeeDepartment : Entity
{
    /// <summary>
    /// 部门编号
    /// </summary>
    public long DepartmentId { get; set; }

    public virtual Department Department { get; set; }

    /// <summary>
    /// 岗位编号
    /// </summary>
    public long PostId { get; set; }

    public virtual Post Post { get; set; }

    /// <summary>
    /// 员工编号
    /// </summary>
    public long EmployeeId { get; set; }

    public virtual Employee Employee { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { DepartmentId, PostId, EmployeeId };
    }
}
