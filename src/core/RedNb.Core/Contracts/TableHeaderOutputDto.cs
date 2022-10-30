using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Core.Contracts
{
    public class TableHeaderOutputDto
    {
        public string Title { get; set; }

        public string Key { get; set; }

        public List<TableHeaderOutputDto> Children { get; set; }

        public int Rowspan { get; set; } = 1;

        public int Colspan { get; set; } = 1;
    }
}
