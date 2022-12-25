using RedNb.Auth.Domain.Menus;

namespace RedNb.Auth.Domain.Roles;

/// <summary>
/// 角色权限关联实体类
/// </summary>
[Table("RoleMenu")]
public class RoleMenu : Entity
{
    /// <summary>
    /// 权限编号
    /// </summary>
    public long MenuId { get; set; }

    public virtual Menu Menu { get; set; }

    /// <summary>
    /// 角色编号
    /// </summary>
    public long RoleId { get; set; }

    public virtual Role Role { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { RoleId, MenuId };
    }
}
