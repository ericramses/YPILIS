using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Store
{
    public class RedisStore
    {
        protected List<RedisDB> m_Databases;

        public RedisStore()
        {
            this.m_Databases = new List<RedisDB>();
        }

        public IDatabase GetDB(AppDBNameEnum dbName)
        {
            RedisDB redisDb = this.m_Databases.Find(x => x.Name == dbName.ToString());
            return redisDb.Server.GetDB((int)dbName);            
        }
    }
}
