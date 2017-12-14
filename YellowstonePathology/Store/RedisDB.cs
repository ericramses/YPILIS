using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Store
{
    public class RedisDB
    {
        protected string m_Name;
        protected int m_Index;
        protected IRedisServer m_Server;

        public RedisDB(AppDBNameEnum dbNameEnum, int index, IRedisServer server)
        {
            this.m_Name = dbNameEnum.ToString();
            this.m_Index = index;
            this.m_Server = server;
        }

        public string Name
        {
            get { return this.Name; }
        }

        public int Index
        {
            get { return this.m_Index; }
        }

        public IRedisServer Server
        {
            get { return this.m_Server; }
        }
    }
}
