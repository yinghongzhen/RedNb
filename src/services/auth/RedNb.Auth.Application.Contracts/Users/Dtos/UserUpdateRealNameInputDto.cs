using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Users.Dtos
{
    public class UserUpdateRealNameInputDto
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        [MaxLength(40)]
        public string Password { get; set; }
    }
}
