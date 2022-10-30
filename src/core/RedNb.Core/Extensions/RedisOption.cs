using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RedNb.Core.Extensions
{
    public class RedisOption
    {
        public RedisOption()
        {

        }

        public string Host { get; set; }

        public int Port { get; set; }

        public string Password { get; set; }

        public int Database { get; set; }
    }
}