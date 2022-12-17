namespace RedNb.Auth.Domain.Users;

/// <summary>
/// 用户信息实体类
/// </summary>
[Table("UserClaim")]
public class UserClaim : EntityBase
{
    [Required]
    [MaxLength(200)]
    public string Type { get; set; }

    [Required]
    public string Value { get; set; }

    /// <summary>
    /// 用户编号
    /// </summary>
    public long UserId { get; set; }

    public virtual User User { get; set; }
}
