using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RedNb.Core.Contracts
{
    public class PagedOutputDto<T>
    {
        public int Total { get; set; }

        public List<T> Items { get; set; }

        public PagedOutputDto(int total, List<T> items)
        {
            Total = total;
            Items = items;
        }
    }
}
