using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;
using RedNb.Core.Domain.Audit;
using Volo.Abp;

namespace RedNb.Auth.Domain.Tenants
{
    /// <summary>
    /// 租户实体类
    /// </summary>
    [Table("Tenant")]
    public class Tenant : AuditFullEntity, ISoftDelete
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
        /// 过期时间
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 是否系统
        /// </summary>
        [Required]
        public bool IsSystem { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
