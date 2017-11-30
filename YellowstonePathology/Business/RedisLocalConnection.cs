using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business
{
    public class RedisLocalConnection : RedisConnection
    {
        public RedisLocalConnection(string database) : base("localhost", "6379", database)
        { }
    }
}
