using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;
using RedNb.Core.Domain.Audit;

namespace RedNb.Auth.Domain.Admins
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    [Table("User")]
    public class User : AuditFullEntity, IHasTenant
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Nickname { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Password { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(300)]
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(100)]
        public string Mobile { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        [MaxLength(100)]
        public string Phone { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public ESex Sex { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(1000)]
        public string Avatar { get; set; }

        /// <summary>
        /// 管理类型
        /// </summary>
        [Required]
        public EManagerType ManagerType { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        [MaxLength(100)]
        public string Type { get; set; }

        /// <summary>
        /// 用户引用编号
        /// </summary>
        public long? ReferenceId { get; set; }

        /// <summary>
        /// 用户引用姓名
        /// </summary>
        [MaxLength(100)]
        public string ReferenceName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public EUsageStatus Status { get; set; }

        /// <summary>
        /// 上次操作时间
        /// </summary>
        public DateTime LastActiveTime { get; set; }

        public long TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
