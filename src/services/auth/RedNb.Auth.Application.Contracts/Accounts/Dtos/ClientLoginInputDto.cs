using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Accounts.Dtos
{
    public class ClientLoginInputDto
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Key { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Secret { get; set; }

        [Required]
        public long UserId { get; set; }
    }
}
