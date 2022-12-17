namespace RedNb.Auth.Domain.Users;

/// <summary>
/// 用户数据权限实体类
/// </summary>
[Table("UserDataScope")]
public class UserDataScope : EntityBase
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

    public long UserId { get; set; }

    public virtual User User { get; set; }
}
