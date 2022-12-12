using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using RedNb.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Roles.Dtos
{
    public class RoleOutputDto : AuditFullEntityDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }

        public bool IsDeleted { get; set; }
    }
}
