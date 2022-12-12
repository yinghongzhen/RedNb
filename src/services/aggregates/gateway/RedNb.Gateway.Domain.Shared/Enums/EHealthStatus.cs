using System.ComponentModel;

namespace RedNb.Gateway.Domain.Shared.Enums;

/// <summary>
/// 健康状态
/// </summary>
[Description("健康状态")]
public enum EHealthStatus
{
    /// <summary>
    /// 离线
    /// </summary>
    [Description("离线")]
    Offline = 0,

    /// <summary>
    /// 在线
    /// </summary>
    [Description("在线")]
    Online = 1
}