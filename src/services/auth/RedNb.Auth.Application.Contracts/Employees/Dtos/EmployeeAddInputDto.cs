using RedNb.Auth.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Employees.Dtos
{
    public class EmployeeAddInputDto
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
        /// 姓名
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        [MaxLength(100)]
        public string EnName { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [MaxLength(100)]
        public string Number { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime? JoinDate { get; set; }

        public long DepartmentId { get; set; }

        public long? CompanyId { get; set; }

        public List<long> PostIds { get; set; }
    }
}
