using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace RedNb.Core.Contracts
{
    public class GetDetailInputDto
    {
        public long Id { get; set; }

        public long? TenantId { get; set; }
    }
}
