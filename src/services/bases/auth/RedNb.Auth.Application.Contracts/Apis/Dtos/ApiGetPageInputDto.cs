using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RedNb.Auth.Application.Contracts.Apis.Dtos
{
    public class ApiGetPageInputDto : PagedInputDto
    {
        public string Tag { get; set; }

        public long ModuleId { get; set; }
    }
}
