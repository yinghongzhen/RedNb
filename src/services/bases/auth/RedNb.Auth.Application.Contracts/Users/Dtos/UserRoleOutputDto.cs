using RedNb.Auth.Application.Contracts.Roles.Dtos;
using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Users.Dtos
{
    public class UserRoleOutputDto : EntityBaseDto
    {
        public UserOutputDto User { get; set; }

        public RoleOutputDto Role { get; set; }
    }
}
