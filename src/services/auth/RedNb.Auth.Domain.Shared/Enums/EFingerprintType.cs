using System.ComponentModel;

namespace RedNb.Auth.Domain.Shared.Enums;

/// <summary>
/// 指纹类型
/// </summary>
[Description("指纹类型")]
public enum EFingerprintType
{
    /// <summary>
    /// 左手大拇指
    /// </summary>
    [Description("左手大拇指")]
    Left1 = 0,

    /// <summary>
    /// 2.0菜单
    /// </summary>
    [Description("右手大拇指")]
    Right1 = 5
}
