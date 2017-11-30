using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Business
{
    public class RedisConnection
    {
        private ConnectionMultiplexer m_Connection;
        private IServer m_Server;
        private IDatabase m_Database;
        private ISubscriber m_Subscriber;        

        public RedisConnection(string ipAddress, string port, RedisDatabaseEnum redisDb)
        {            
            this.m_Connection = ConnectionMultiplexer.Connect(ipAddress + ":" + port + ", ConnectTimeout=5000, SyncTimeout=5000");
            this.m_Server = this.m_Connection.GetServer(ipAddress + ":" + port);
            this.m_Database = this.m_Connection.GetDatabase((int)redisDb);
            this.m_Subscriber = this.m_Connection.GetSubscriber();
        }               

        public IDatabase Db
        {
            get { return this.m_Database; }
        }

        public ISubscriber Subscriber
        {
            get { return this.m_Subscriber; }
        }

        public IServer Server
        {
            get { return this.m_Server; }
        }
    }
}
