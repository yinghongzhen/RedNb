using RedNb.Auth.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace RedNb.Auth.Application.Contracts.Users.Dtos
{
    public class UserUpdateInputDto : EntityDto<long>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(100)]
        public string Nickname { get; set; }
    }
}
