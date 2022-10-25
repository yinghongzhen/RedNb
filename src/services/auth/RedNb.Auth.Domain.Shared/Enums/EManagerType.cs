using System.ComponentModel;

namespace RedNb.Auth.Domain.Shared.Enums
{
    /// <summary>
    /// 管理类型
    /// </summary>
    [Description("管理类型")]
    public enum EManagerType
    {
        /// <summary>
        /// 租户用户、非管理员
        /// </summary>
        [Description("非管理员")]
        TenantUser = 0,

        /// <summary>
        /// 二级管理员
        /// </summary>
        [Description("二级管理员")]
        SecondAdmin = 1,

        /// <summary>
        /// 租户管理员
        /// </summary>
        [Description("租户管理员")]
        TenantAdmin = 2,

        /// <summary>
        /// 超级管理员
        /// </summary>
        [Description("超级管理员")]
        SuperAdmin = 3
    }
}
