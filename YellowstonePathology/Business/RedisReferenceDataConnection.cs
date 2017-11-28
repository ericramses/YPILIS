using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business
{
    public class RedisReferenceDataConnection : RedisConnection
    {
        public RedisReferenceDataConnection() : base("localhost", "6379")
        { }
    }
}
