using System.ComponentModel;

namespace RedNb.Auth.Domain.Shared.Enums
{
    /// <summary>
    /// 岗位分类
    /// </summary>
    [Description("岗位分类")]
    public enum EPostType
    {
        /// <summary>
        /// 高管
        /// </summary>
        [Description("高管")]
        T1 = 0,

        /// <summary>
        /// 中层
        /// </summary>
        [Description("中层")]
        T2 = 1,

        /// <summary>
        /// 基层
        /// </summary>
        [Description("基层")]
        T3 = 2,

        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        T4 = 3
    }
}
