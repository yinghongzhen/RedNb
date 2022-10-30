using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedNb.Core.Contracts
{
    public class KeyValueDto
    {
        public string Key { get; set; }

        public object Value { get; set; }

        public string Type { get; set; }
    }
}
