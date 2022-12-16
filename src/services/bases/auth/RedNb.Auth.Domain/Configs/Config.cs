using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RedNb.Core.Domain;
using RedNb.Core.Domain.Audit;

namespace RedNb.Auth.Domain.Configs
{
    /// <summary>
    /// 参数配置实体类
    /// </summary>
    [Table("Config")]
    public class Config : AuditFullEntity
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
        /// 值
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Value { get; set; }

        /// <summary>
        /// 是否系统
        /// </summary>
        [Required]
        public bool IsSystem { get; set; }
    }
}
