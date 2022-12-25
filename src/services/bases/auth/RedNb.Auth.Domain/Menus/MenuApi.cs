using RedNb.Auth.Domain.Services;

namespace RedNb.Auth.Domain.Menus;

/// <summary>
/// 权限接口关系实体类
/// </summary>
[Table("MenuApi")]
public class MenuApi : BaseEntity
{
    public long ApiId { get; set; }

    public virtual Api Api { get; set; }

    public long MenuId { get; set; }

    public virtual Menu Menu { get; set; }
}
