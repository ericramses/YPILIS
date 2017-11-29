using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business
{
    public class RedisAppDataConnection : RedisConnection
    {
        public RedisAppDataConnection() : base("10.1.2.70", "31607")
        {

        }
    }
}
