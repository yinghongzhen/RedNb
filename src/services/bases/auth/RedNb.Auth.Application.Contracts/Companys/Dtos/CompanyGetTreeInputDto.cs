using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Auth.Application.Contracts.Companys.Dtos
{
    public class CompanyGetTreeInputDto
    {
        public List<long> ExcludeIds { get; set; }
    }
}
