using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;
using RedNb.Core.Domain.Audit;
using Volo.Abp;

namespace RedNb.Auth.Domain.Admins
{
    /// <summary>
    /// 模块回调调用记录实体类
    /// </summary>
    [Table("ModuleCallback")]
    public class ModuleCallback : EntityBase
    {
        [Required]
        [MaxLength(500)]
        public string Url { get; set; }

        [Required]
        public HttpStatusCode ResultCode { get; set; }

        public string ResultValue { get; set; }

        public long ModuleId { get; set; }

        public virtual Module Module { get; set; }
    }
}
