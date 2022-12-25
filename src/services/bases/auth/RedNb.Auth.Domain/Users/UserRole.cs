using RedNb.Auth.Domain.Roles;

namespace RedNb.Auth.Domain.Users;

/// <summary>
/// 用户角色关联实体类
/// </summary>
[Table("UserRole")]
public class UserRole : Entity
{
    /// <summary>
    /// 角色编号
    /// </summary>
    public long RoleId { get; set; }

    public virtual Role Role { get; set; }

    /// <summary>
    /// 用户编号
    /// </summary>
    public long UserId { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { UserId, RoleId };
    }
}
