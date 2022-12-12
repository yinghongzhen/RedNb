using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Roles.Dtos
{
    public class RoleAddUserBatchInputDto
    {
        [Required]
        public List<long> UserIds { get; set; }

        [Required]
        public long RoleId { get; set; }
    }
}
