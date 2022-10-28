using RedNb.Auth.Application.Contracts.Tenants.Dtos;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using RedNb.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Users.Dtos
{
    public class UserOutputDto : AuditFullEntityDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public ESex Sex { get; set; }

        public string SexStr { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 手机验证码
        /// </summary>
        public string MobileCode { get; set; }

        /// <summary>
        /// 手机是否验证
        /// </summary>
        public bool IsMobileConfirmed { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 邮箱验证码
        /// </summary>
        public string EmailCode { get; set; }

        /// <summary>
        /// 邮箱是否验证
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// 微信唯一id
        /// </summary>
        public string WxUnionId { get; set; }

        /// <summary>
        /// 微信公众号
        /// </summary>
        public string WxMpOpenId { get; set; }

        /// <summary>
        /// 微信小程序
        /// </summary>
        public string WxAppOpenId { get; set; }

        /// <summary>
        /// 管理类型
        /// </summary>
        public EManagerType ManagerType { get; set; }

        public string ManagerTypeStr { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 用户引用编号
        /// </summary>
        public long? ReferenceId { get; set; }

        /// <summary>
        /// 用户引用姓名
        /// </summary>
        public string ReferenceName { get; set; }

        public bool IsActive { get; set; }

        public long TenantId { get; set; }

        public TenantOutputDto Tenant { get; set; }
    }
}
