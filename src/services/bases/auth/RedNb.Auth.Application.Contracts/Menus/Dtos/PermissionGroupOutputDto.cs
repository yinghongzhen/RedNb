using RedNb.Auth.Domain.Shared.Enums;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Permissions.Dtos
{
    public class PermissionGroupOutputDto
    {
        public List<long> SelectedPermissionIds { get; set; }

        public PagedOutputDto<PermissionOutputDto> PermissionList { get; set; }
    }
}
