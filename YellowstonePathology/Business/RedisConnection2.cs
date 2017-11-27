using System;
using StackExchange.Redis;

namespace YellowstonePathology.Business
{
    public class RedisConnection2
    {
        private static volatile RedisConnection2 instance;
        private static object syncRoot = new Object();

        private ConnectionMultiplexer m_Connection;
        private ConnectionMultiplexer m_LocalConnection;
        private IServer m_Server;
        private IServer m_LocalServer;

        static RedisConnection2()
        {

        }

        private RedisConnection2()
        {
            this.m_Connection = ConnectionMultiplexer.Connect("10.1.2.25:6379, ConnectTimeout=5000, SyncTimeout=5000, AbortConnect=false");
            this.m_Server = this.m_Connection.GetServer("10.1.2.25:6379");
            
            this.m_LocalConnection = ConnectionMultiplexer.Connect("localhost:6379, ConnectTimeout=5000, SyncTimeout=5000, AbortConnect=false");
            if (this.m_LocalConnection.IsConnected == true)
            {
                this.m_LocalServer = this.m_LocalConnection.GetServer("localhost:6379");
            }
            else this.m_LocalServer = this.m_Server;
        }

        public IDatabase GetDatabase()
        {
            return this.m_Connection.GetDatabase();
        }

        public IDatabase GetLocalDatabase()
        {
            if (this.m_LocalConnection.IsConnected == true)
            {
                return this.m_LocalConnection.GetDatabase();
            }
            else return this.m_Connection.GetDatabase();
        }

        public ISubscriber GetSubscriber()
        {
            return this.m_Connection.GetSubscriber();
        }

        public IServer Server
        {
            get { return this.m_Server; }
        }

        public IServer LocalServer
        {
            get { return this.m_LocalServer; }
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
