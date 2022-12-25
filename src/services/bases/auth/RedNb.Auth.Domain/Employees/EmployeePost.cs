using RedNb.Auth.Domain.Posts;

namespace RedNb.Auth.Domain.Employees;

/// <summary>
/// 员工岗位关联实体类
/// </summary>
[Table("EmployeePost")]
public class EmployeePost : Entity
{
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
        return new object[] { PostId, EmployeeId };
    }
}
