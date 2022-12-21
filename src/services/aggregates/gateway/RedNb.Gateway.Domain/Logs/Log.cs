using RedNb.Core.Domain.Shared;
using RedNb.Gateway.Domain.Shared.Enums;
using System.Net;

namespace RedNb.Gateway.Domain.Logs;

[Table("Log")]
public class Log : BaseEntity
{
    /// <summary>
    /// 类型
    /// </summary>
    [Required]
    public ELogType Type { get; set; }

    /// <summary>
    /// 谓词
    /// </summary>
    [Required]
    public EHttpMethod Method { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// 结构
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Scheme { get; set; }

    /// <summary>
    /// 接口主机
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Host { get; set; }

    /// <summary>
    /// 接口地址
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string Path { get; set; }

    /// <summary>
    /// Get参数
    /// </summary>
    [MaxLength(500)]
    public string QueryString { get; set; }

    /// <summary>
    /// Body参数
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// 客户端IP
    /// </summary>
    [MaxLength(100)]
    public string ClientIp { get; set; }

    /// <summary>
    /// 客户端信息
    /// </summary>
    [MaxLength(500)]
    public string UserAgent { get; set; }

    /// <summary>
    /// 设备名称/操作系统
    /// </summary>
    [MaxLength(100)]
    public string DeviceName { get; set; }

    /// <summary>
    /// 浏览器名称
    /// </summary>
    [MaxLength(100)]
    public string BrowserName { get; set; }

    [Required]
    public HttpStatusCode ResultCode { get; set; }

    public string ResultValue { get; set; }

    /// <summary>
    /// 执行时间
    /// </summary>
    [Required]
    public int ExecuteTime { get; set; }

    [Required]
    public long TenantId { get; set; }

    [Required]
    [MaxLength(100)]
    public string TenantName { get; set; }
}