using System;
using StackExchange.Redis;

namespace YellowstonePathology.Business
{
    public class RedisConnection2
    {
        private static volatile RedisConnection2 instance;
        private static object syncRoot = new Object();

        private ConnectionMultiplexer m_Connection;
        private IServer m_Server;

        static RedisConnection2()
        {

        }

        private RedisConnection2()
        {
            this.m_Connection = ConnectionMultiplexer.Connect("localhost, ConnectTimeout=5000, SyncTimeout=5000, AbortConnect=false");
            this.m_Server = this.m_Connection.GetServer("localhost:6379");
        }

        public IDatabase GetDatabase()
        {
            return this.m_Connection.GetDatabase();
        }

        public ISubscriber GetSubscriber()
        {
            return this.m_Connection.GetSubscriber();
        }

        public IServer Server
        {
            get { return this.m_Server; }
        }

        public static RedisConnection2 Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new RedisConnection2();
                    }
                }

                return instance;
            }
        }
    }
}
