using System;
using System.Collections.Generic;
using System.Text;

namespace RedNb.Core.Contracts
{
    public class HealthServiceDto
    {
        public string Ip { get; set; }

        public int Port { get; set; }

        public string Name { get; set; }

        public string Key { get; set; }
    }
}
