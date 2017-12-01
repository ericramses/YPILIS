using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Business
{
    public sealed class RedisSingleton
    {
        private static RedisSingleton instance = null;
        private static readonly object padlock = new object();

        private ConnectionMultiplexer m_Connection;
        private IServer m_Server;
        private IDatabase m_Database;
        private ISubscriber m_Subscriber;

        RedisSingleton()
        {
            this.m_Connection = ConnectionMultiplexer.Connect("10.1.2.70:31578, ConnectTimeout=5000, SyncTimeout=5000");
            this.m_Server = this.m_Connection.GetServer("10.1.2.70:31578");
            this.m_Database = this.m_Connection.GetDatabase();
            this.m_Subscriber = this.m_Connection.GetSubscriber();
        }

        public static RedisSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RedisSingleton();
                    }
                    return instance;
                }
            }
        }

        public void CloseConnection()
        {
            this.m_Connection.Close();
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
