using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using RedNb.Core.Domain;

namespace RedNb.Auth.Application.Contracts.Apis.Dtos
{
    public class ApiOutputDto : AuditFullEntityDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 接口方法
        /// </summary>
        public EHttpMethod Method { get; set; }

        public string MethodStr { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// 模块编号
        /// </summary>
        public long ModuleId { get; set; }
    }
}
