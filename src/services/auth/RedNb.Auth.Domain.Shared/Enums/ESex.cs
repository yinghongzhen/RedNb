using System.ComponentModel;

namespace RedNb.Auth.Domain.Shared.Enums;

/// <summary>
/// 性别
/// </summary>
[Description("性别")]
public enum ESex
{
    /// <summary>
    /// 未设置
    /// </summary>
    [Description("未设置")]
    None = 0,

    /// <summary>
    /// 男性
    /// </summary>
    [Description("男性")]
    Male = 1,

    /// <summary>
    /// 女性
    /// </summary>
    [Description("女性")]
    Female = 2
}
