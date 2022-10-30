using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace RedNb.Core.Contracts
{
    public class AntdTreeOutputDto
    {
        public string Title { get; set; }

        public string Value { get; set; }

        public string Key { get; set; }

        public bool IsLeaf { get; set; }

        public List<AntdTreeOutputDto> Children { get; set; }
    }
}