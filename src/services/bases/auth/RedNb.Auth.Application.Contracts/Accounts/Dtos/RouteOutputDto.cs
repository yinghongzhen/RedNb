using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Accounts.Dtos
{
    public class RouteOutputDto
    {
        public string Router { get; set; }

        public string Name { get; set; }

        public MetaOutputDto Meta { get; set; }

        public List<RouteOutputDto> Children { get; set; }

    }
}
