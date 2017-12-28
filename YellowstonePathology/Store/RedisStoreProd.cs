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
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.EmbeddingScan, 0, RedisServerDeprecated.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.CPTCode, 1, RedisServerProd1.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.ICDCode, 2, RedisServerProd1.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.Stain, 3, RedisServerProd1.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.PQRS, 4, RedisServerProd1.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.Lock, 1, RedisServerDeprecated.Instance));                        
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.VantageSlide, 6, RedisServerProd1.Instance));
        }
    }
}
