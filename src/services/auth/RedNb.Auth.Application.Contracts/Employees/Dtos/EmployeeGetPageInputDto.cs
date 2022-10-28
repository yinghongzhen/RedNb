using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Employees.Dtos
{
    public class EmployeeGetPageInputDto : PagedInputDto
    {
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
        /// 姓名
        /// </summary>
        [MaxLength(100)]
        public string Name { get; set; }

        public long? DepartmentId { get; set; }
    }
}
