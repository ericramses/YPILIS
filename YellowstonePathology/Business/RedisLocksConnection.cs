using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business
{
    public class RedisLocksConnection : RedisConnection
    {
        public RedisLocksConnection() : base("10.1.2.25", "6379")
        {

        }
    }
}
