using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;
using RedNb.Core.Domain.Audit;

namespace RedNb.Auth.Domain.Admins
{
    /// <summary>
    /// 接口实体类
    /// </summary>
    [Table("Api")]
    public class Api : AuditFullEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Path { get; set; }

        /// <summary>
        /// 谓词
        /// </summary>
        [Required]
        public EHttpMethod Method { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Tags { get; set; }

        /// <summary>
        /// 模块编号
        /// </summary>
        public long ModuleId { get; set; }

        public virtual Module Module { get; set; }
    }
}
