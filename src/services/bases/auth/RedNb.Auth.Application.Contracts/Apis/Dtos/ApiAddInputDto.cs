using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RedNb.Auth.Application.Contracts.Apis.Dtos
{
    public class ApiAddInputDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Path { get; set; }

        /// <summary>
        /// 接口方法
        /// </summary>
        [Required]
        public EHttpMethod Method { get; set; }

        /// <summary>
        /// 使用状态
        /// </summary>
        [Required]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Tags { get; set; }

        /// <summary>
        /// 模块编号
        /// </summary>
        public long ModuleId { get; set; }
    }
}
