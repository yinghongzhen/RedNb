namespace RedNb.Auth.Domain.Admins;

/// <summary>
/// 登录账户实体类
/// </summary>
[Table("LoginAccount")]
public class LoginAccount : AuditFullEntity
{
    /// <summary>
    /// 手机
    /// </summary>
    [StringLength(255)]
    public string Mobile { get; set; }

    /// <summary>
    /// 手机验证码
    /// </summary>
    [StringLength(100)]
    public string MobileCode { get; set; }

    /// <summary>
    /// 手机是否验证
    /// </summary>
    [Required]
    public bool IsMobileConfirmed { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [StringLength(255)]
    public string Email { get; set; }

    /// <summary>
    /// 邮箱验证码
    /// </summary>
    [StringLength(100)]
    public string EmailCode { get; set; }

    /// <summary>
    /// 邮箱是否验证
    /// </summary>
    [Required]
    public bool IsEmailConfirmed { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [StringLength(255)]
    public string Password { get; set; }

    /// <summary>
    /// 微信唯一id
    /// </summary>
    [StringLength(255)]
    public string WxUnionId { get; set; }

    /// <summary>
    /// 微信公众号
    /// </summary>
    [StringLength(255)]
    public string WxMpOpenId { get; set; }

    /// <summary>
    /// 微信小程序
    /// </summary>
    [StringLength(255)]
    public string WxAppOpenId { get; set; }
}
