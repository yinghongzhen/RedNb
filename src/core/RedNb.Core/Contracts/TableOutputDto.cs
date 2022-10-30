using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Core.Contracts
{
    public class TableOutputDto
    {
        public List<TableHeaderOutputDto> HeaderList { get; set; }

        public List<object> ColumnList { get; set; }

        public int Count { get; set; }
    }
}
