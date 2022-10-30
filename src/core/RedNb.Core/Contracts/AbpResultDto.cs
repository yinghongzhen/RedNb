using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace RedNb.Core.Contracts
{
    public class AbpResultDto
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public string Details { get; set; }

        public string ValidationErrors { get; set; }
    }
}
