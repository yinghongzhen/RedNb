using RedNb.Auth.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Permissions.Dtos
{
    public class PermissionAllotToRoleInputDto
    {
        public List<long> PermissionIds { get; set; }

        public long RoleId { get; set; }
    }
}
