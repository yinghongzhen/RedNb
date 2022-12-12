using RedNb.Auth.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Accounts.Dtos
{
    public class ClientRegInputDto
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Key { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Secret { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(100)]
        public string Username { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
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
    }
}
