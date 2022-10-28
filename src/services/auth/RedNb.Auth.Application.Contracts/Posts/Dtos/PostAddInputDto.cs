using RedNb.Auth.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Posts.Dtos
{
    public class PostAddInputDto
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
        [MaxLength(100)]
        public string Key { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [Required]
        public EPostType Type { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Required]
        public int Sort { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public long? TenantId { get; set; }

        /// <summary>
        /// 默认
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
