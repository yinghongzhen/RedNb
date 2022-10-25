using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;
using RedNb.Auth.Domain.Admins;
using RedNb.Core.Domain.Audit;
using Volo.Abp.Domain.Entities;

namespace RedNb.Auth.Domain.Offices
{
    /// <summary>
    /// 职位实体类
    /// </summary>
    [Table("Post")]
    public class Post : AggregateRoot<long>
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
        public decimal Sort { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [MaxLength(500)]
        public string Desc { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 租户编号
        /// </summary>
        [Required]
        public long TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
