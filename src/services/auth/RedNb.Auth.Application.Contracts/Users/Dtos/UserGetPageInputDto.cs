using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using RedNb.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Users.Dtos
{
    public class UserGetPageInputDto : PagedInputDto
    {
        public EManagerType ManagerType { get; set; }

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
        /// 用户类型
        /// </summary>
        [MaxLength(100)]
        public string Type { get; set; }

        /// <summary>
        /// 用户引用姓名
        /// </summary>
        [MaxLength(100)]
        public string ReferenceName { get; set; }

        public long? TenantId { get; set; }

        public long? DepartmentId { get; set; }
    }
}
