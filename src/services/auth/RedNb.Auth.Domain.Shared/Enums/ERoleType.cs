using System.ComponentModel;

namespace RedNb.Auth.Domain.Shared.Enums;

/// <summary>
/// 角色类型
/// </summary>
[Description("角色类型")]
public enum ERoleType
{
    /// <summary>
    /// 超级管理员
    /// </summary>
    [Description("超级管理员")]
    SuperAdmin = 0,

    /// <summary>
    /// 普通
    /// </summary>
    [Description("普通")]
    Normal = 100
}
