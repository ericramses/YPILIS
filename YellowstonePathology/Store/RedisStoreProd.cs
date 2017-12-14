using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Store
{
    public class RedisStoreProd : RedisStore
    {               
        public RedisStoreProd()
        {
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.CPTCode, 1, RedisServerKub.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.ICDCode, 2, RedisServerKub.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.Stain, 3, RedisServerKub.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.PQRS, 4, RedisServerKub.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.Lock, 5, RedisServerKub.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.VantageSlideScan, 5, RedisServerKub.Instance));
        }
    }
}
