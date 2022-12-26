using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Companys.Dtos
{
    public class CompanyAddInputDto : TreeAddInputDto
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Key { get; set; }

        /// <summary>
        /// 公司全称
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string FullName { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string AreaCode { get; set; }

        /// <summary>
        /// 租户编号
        /// </summary>
        [Required]
        public long TenantId { get; set; }
    }
}
