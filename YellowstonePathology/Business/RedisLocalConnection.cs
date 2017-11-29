using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business
{
    public class RedisLocalConnection : RedisConnection
    {
        public RedisLocalConnection() : base("localhost", "6379")
        { }
    }
}
