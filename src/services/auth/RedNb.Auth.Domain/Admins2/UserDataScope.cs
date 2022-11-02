using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;

namespace RedNb.Auth.Domain.Admins
{
    /// <summary>
    /// 用户数据权限实体类
    /// </summary>
    [Table("UserDataScope")]
    public class UserDataScope : EntityBase
    {
        /// <summary>
        /// 管理类型
        /// </summary>
        [Required]
        public EManagerType ManagerType { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Nickname { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Avatar { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public EUsageStatus Status { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        [MaxLength(100)]
        public string Type { get; set; }

        /// <summary>
        /// 用户引用编号
        /// </summary>
        [MaxLength(100)]
        public long ReferenceId { get; set; }

        /// <summary>
        /// 用户引用姓名
        /// </summary>
        [MaxLength(100)]
        public string ReferenceName { get; set; }

        public long? TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
