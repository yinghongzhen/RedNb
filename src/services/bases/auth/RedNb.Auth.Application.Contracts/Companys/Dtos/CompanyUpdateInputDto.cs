using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace RedNb.Auth.Application.Contracts.Companys.Dtos
{
    public class CompanyUpdateInputDto : TreeUpdateInputDto
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
    }
}
