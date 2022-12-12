using System.ComponentModel;

namespace RedNb.Auth.Domain.Shared.Enums;

/// <summary>
/// 权限类型
/// </summary>
[Description("权限类型")]
public enum EPermissionType
{
    /// <summary>
    /// 分组
    /// </summary>
    [Description("分组")]
    Group = 0,

    /// <summary>
    /// 菜单
    /// </summary>
    [Description("菜单")]
    Menu = 1,

    /// <summary>
    /// 功能
    /// </summary>
    [Description("功能")]
    Element = 2
}
