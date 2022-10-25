using System.ComponentModel;

namespace RedNb.Auth.Domain.Shared.Enums
{
    /// <summary>
    /// 菜单类型
    /// </summary>
    [Description("员工状态")]
    public enum EEmployeeStatus
    {
        /// <summary>
        /// 在职
        /// </summary>
        [Description("在职")]
        Normal = 0,

        /// <summary>
        /// 离职
        /// </summary>
        [Description("离职")]
        Disabled = 1,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Deleted = 2
    }
}
