using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Users.Dtos
{
    public class UserAddInputDto
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
        [MaxLength(255)]
        public string Password { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(1000)]
        public string Avatar { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public ESex Sex { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        [MaxLength(100)]
        public string Phone { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [MaxLength(255)]
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(500)]
        public string Email { get; set; }

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

        [Required]
        public long TenantId { get; set; }
    }
}
