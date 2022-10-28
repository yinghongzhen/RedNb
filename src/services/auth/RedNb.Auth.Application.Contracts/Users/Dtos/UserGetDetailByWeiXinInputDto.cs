using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Users.Dtos
{
    public class UserGetDetailByWeiXinInputDto
    {
        [Required]
        public long TenantId { get; set; }

        [Required]
        public string OpenId { get; set; }
    }
}
