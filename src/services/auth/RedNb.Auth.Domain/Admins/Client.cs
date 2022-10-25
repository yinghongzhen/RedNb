using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;
using RedNb.Core.Domain.Audit;

namespace RedNb.Auth.Domain.Admins;

/// <summary>
/// 第三方登录客户端实体类
/// </summary>
[Table("Client")]
public class Client : AuditFullEntity, IHasTenant
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Key { get; set; }

    /// <summary>
    /// 密钥
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Secret { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public long TenantId { get; set; }

    public virtual Tenant Tenant { get; set; }
}
