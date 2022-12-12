using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Users.Dtos
{
    public class UserIdGetInputDto
    {
        public long TenantId { get; set; }

        public long UserId { get; set; }

        public BatchSelectInputDto Data { get; set; }
    }
}
