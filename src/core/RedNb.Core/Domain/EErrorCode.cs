namespace RedNb.Core.Domain;

/// <summary>
/// 错误码
/// </summary>
[Description("错误码")]
public enum EErrorCode
{
    /// <summary>
    /// 无
    /// </summary>
    [Description("无")]
    None = 0,

    /// <summary>
    /// 用户未登录
    /// </summary>
    [Description("用户未登录")]
    NotLogin = 101,

    /// <summary>
    /// 登录超时
    /// </summary>
    [Description("登录超时,请重新登录")]
    Timeout = 102,

    /// <summary>
    /// 操作异常
    /// </summary>
    [Description("操作异常")]
    Exception = 103,

    /// <summary>
    /// 没有权限
    /// </summary>
    [Description("没有权限操作")]
    NoPermission = 104,

    /// <summary>
    /// KEY无效
    /// </summary>
    [Description("KEY无效")]
    InvalidKey = 105,

    /// <summary>
    /// Secret无效
    /// </summary>
    [Description("Secret无效")]
    InvalidSecret = 106
}
