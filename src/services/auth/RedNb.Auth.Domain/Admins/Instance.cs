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
/// 服务实例实体类
/// </summary>
[Table("Instance")]
public class Instance : AuditFullEntity
{
    /// <summary>
    /// 地址
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Host { get; set; }

    /// <summary>
    /// 端口
    /// </summary>
    [Required]
    public int Port { get; set; }

    /// <summary>
    /// 健康检查-地址
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string CheckHttp { get; set; }

    /// <summary>
    /// 健康检查-心跳间隔
    /// </summary>
    [Required]
    public int CheckInterval { get; set; }

    /// <summary>
    /// 健康检查-超时时间
    /// </summary>
    [Required]
    public int CheckTimeout { get; set; }

    /// <summary>
    /// 健康状态
    /// </summary>
    [Required]
    public EHealthStatus Status { get; set; }

    public long ModuleId { get; set; }

    public virtual Module Module { get; set; }
}
