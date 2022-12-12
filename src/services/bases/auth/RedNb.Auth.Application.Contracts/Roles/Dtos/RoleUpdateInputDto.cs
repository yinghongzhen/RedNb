using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace RedNb.Auth.Application.Contracts.Roles.Dtos
{
    public class RoleUpdateInputDto : EntityDto<long>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Key { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Required]
        public int Sort { get; set; }
    }
}
