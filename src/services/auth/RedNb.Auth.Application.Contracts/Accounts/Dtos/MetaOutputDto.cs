using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Accounts.Dtos
{
    public class MetaOutputDto
    {
        public string Icon { get; set; }

        public string Link { get; set; }

        public bool Invisible { get; set; }

        public dynamic Query { get; set; }

        public PageOutputDto Page { get; set; }
    }
}
