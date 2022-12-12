using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Platforms.Dtos
{
    public class PlatformOutputDto : AuditFullEntityDto
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
        /// 默认值
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 排列序号
        /// </summary>
        public int Sort { get; set; }
    }
}
