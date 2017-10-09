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
        private static volatile RedisConnection instance;
        private static object syncRoot = new Object();

        private ConnectionMultiplexer m_Connection;
        private IServer m_Server;

        static RedisConnection()
        {
            
        }

        private RedisConnection()
        {
            //this.m_Connection = ConnectionMultiplexer.Connect("10.1.2.25, ConnectTimeout=5000, SyncTimeout=5000");
            //this.m_Server = this.m_Connection.GetServer("10.1.2.25:6379");            
            //this.m_Connection = ConnectionMultiplexer.Connect("localhost, ConnectTimeout=5000, SyncTimeout=5000, AbortOnConnectFail=false");

            ConfigurationOptions co = new ConfigurationOptions()
            {
                SyncTimeout = 5000,
                EndPoints =
            {
                {"localhost", 6379 }
            },
                AbortOnConnectFail = false // this prevents that error
            };

            this.m_Connection = ConnectionMultiplexer.Connect(co);
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

        public static RedisConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new RedisConnection();
                    }
                }

                return instance;
            }
        }
    }
}
