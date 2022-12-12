using RedNb.Auth.Application.Contracts.Companys.Dtos;
using RedNb.Auth.Application.Contracts.Departments.Dtos;
using RedNb.Auth.Application.Contracts.Posts.Dtos;
using RedNb.Auth.Application.Contracts.Tenants.Dtos;
using RedNb.Auth.Application.Contracts.Users.Dtos;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace RedNb.Auth.Application.Contracts.Employees.Dtos
{
    public class EmployeeOutputDto : AuditFullEntityDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public ESex Sex { get; set; }

        public string SexStr { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime JoinDate { get; set; }

        public long DepartmentId { get; set; }

        public DepartmentOutputDto Department { get; set; }

        public List<PostOutputDto> PostList { get; set; }

        public long UserId { get; set; }

        public UserOutputDto User { get; set; }
    }
}
