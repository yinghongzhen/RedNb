using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace RedNb.Core.Contracts
{
    public class LoginUserDto
    {
        /// <summary>
        /// 平台标识
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 用户引用编号
        /// </summary>
        public long? ReferenceId { get; set; }

        /// <summary>
        /// 用户引用姓名
        /// </summary>
        public string ReferenceName { get; set; }

        /// <summary>
        /// 租户编号
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public long Exp { get; set; }
    }
}
