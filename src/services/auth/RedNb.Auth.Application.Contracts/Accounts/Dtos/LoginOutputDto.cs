using RedNb.Auth.Application.Contracts.Permissions.Dtos;
using RedNb.Auth.Application.Contracts.Roles.Dtos;
using RedNb.Auth.Application.Contracts.Users.Dtos;
using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Accounts.Dtos
{
    public class LoginOutputDto
    {
        public string Token { get; set; }

        public LoginUserDto LoginUser { get; set; }

        public UserOutputDto User { get; set; }

        public List<PermissionOutputDto> PermissionList { get; set; }

        public List<RouteOutputDto> RouteList { get; set; }

        public List<RoleOutputDto> RoleList { get; set; }
    }
}
