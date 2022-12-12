using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;
using RedNb.Core.Domain.Audit;

namespace RedNb.Auth.Domain.Admins
{
    /// <summary>
    /// 平台实体类
    /// </summary>
    [Table("Platform")]
    public class Platform : AuditFullEntity
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
        public decimal Sort { get; set; }
    }
}
