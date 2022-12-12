using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace RedNb.Auth.Application.Contracts.Users.Dtos
{
    public class UserRefInfoDto : EntityDto<long>
    {
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
    }
}
