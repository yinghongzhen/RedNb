using RedNb.Core.Domain.Audit;
using RedNb.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace RedNb.Core.Domain.Audit
{
    public class AuditFullEntity : EntityBase, IAuditObject
    {
        /// <summary>
        /// 创建者编号
        /// </summary>
        [Required]
        public long CreateId { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string CreateName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者编号
        /// </summary>
        [Required]
        public long UpdateId { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string UpdateName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Required]
        public DateTime UpdateTime { get; set; }
    }
}
