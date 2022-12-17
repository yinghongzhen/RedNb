namespace RedNb.Auth.Domain.Roles;

/// <summary>
/// 角色数据权限实体类
/// </summary>
[Table("RoleDataScope")]
public class RoleDataScope : EntityBase
{
    [Required]
    [MaxLength(20)]
    public string Type { get; set; }

    [Required]
    [MaxLength(50)]
    public string Data { get; set; }

    [Required]
    [MaxLength(50)]
    public string Permission { get; set; }

    /// <summary>
    /// 角色编号
    /// </summary>
    public long RoleId { get; set; }

    public virtual Role Role { get; set; }
}
