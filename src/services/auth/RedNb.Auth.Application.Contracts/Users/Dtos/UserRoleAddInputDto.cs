using RedNb.Auth.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Users.Dtos
{
    public class UserRoleAddInputDto
    {
        public long RoleId { get; set; }

        public long UserId { get; set; }
    }
}
