using RedNb.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RedNb.Auth.Application.Contracts.Apis.Dtos
{
    public class ApiGetTagListInputDto : PagedInputDto
    {
        public long ModuleId { get; set; }
    }
}
