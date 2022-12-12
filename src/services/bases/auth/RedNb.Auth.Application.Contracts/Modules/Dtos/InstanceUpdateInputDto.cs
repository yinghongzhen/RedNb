using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace RedNb.Auth.Application.Contracts.Modules.Dtos
{
    public class InstanceUpdateInputDto : EntityDto<long>
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
    }
}
