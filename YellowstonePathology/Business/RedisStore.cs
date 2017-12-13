using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business
{
    public class RedisStore
    {
        private static RedisStore instance = null;
        private static readonly object padlock = new object();
    }
}
