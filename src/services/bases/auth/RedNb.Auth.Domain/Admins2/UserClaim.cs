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
    /// 用户信息实体类
    /// </summary>
    [Table("UserClaim")]
    public class UserClaim : AuditFullEntity
    {
        [Required]
        public long UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        [MaxLength(200)]
        public string Type { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
