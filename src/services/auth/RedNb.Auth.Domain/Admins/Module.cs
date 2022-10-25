using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;
using RedNb.Core.Domain.Audit;
using Volo.Abp;

namespace RedNb.Auth.Domain.Admins;

/// <summary>
/// 模块实体类
/// </summary>
[Table("Module")]
public class Module : AuditFullEntity, ISoftDelete
{
    /// <summary>
    /// 类型
    /// </summary>
    [Required]
    public EModuleType Type { get; set; }

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
    /// 版本
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Version { get; set; }

    /// <summary>
    /// 是否必选
    /// </summary>
    [Required]
    public bool IsRequired { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required]
    public decimal Sort { get; set; }

    /// <summary>
    /// 注册回调
    /// </summary>
    [MaxLength(500)]
    public string RegCallback { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public bool IsDeleted { get; set; }
}
