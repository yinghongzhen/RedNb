using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Modules.Dtos
{
    public class InstanceOutputDto : AuditFullEntityDto
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 健康检查-地址
        /// </summary>
        public string CheckHttp { get; set; }

        /// <summary>
        /// 健康检查-心跳间隔
        /// </summary>
        public int CheckInterval { get; set; }

        /// <summary>
        /// 健康检查-超时时间
        /// </summary>
        public int CheckTimeout { get; set; }

        /// <summary>
        /// 健康状态
        /// </summary>
        public EHealthStatus Status { get; set; }

        /// <summary>
        /// 健康状态
        /// </summary>
        public string StatusStr { get; set; }
    }
}
