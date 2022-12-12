using System.ComponentModel;

namespace RedNb.Gateway.Domain.Shared.Enums;

/// <summary>
/// 日志类型
/// </summary>
[Description("日志类型")]
public enum ELogType
{
    /// <summary>
    /// 登录登出
    /// </summary>
    [Description("登录登出")]
    LoginOrLogout = 0,

    /// <summary>
    /// 增删改查
    /// </summary>
    [Description("增删改查")]
    Crud = 1
}
