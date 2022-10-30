using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace RedNb.Core.Contracts
{
    public class DeleteBatchInputDto
    {
        public List<long> Ids { get; set; }
    }
}
