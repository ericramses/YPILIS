using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Business
{
    public sealed class RedisAppDataConnection
    {
        private static RedisAppDataConnection instance = null;
        private static readonly object padlock = new object();

        private ConnectionMultiplexer m_Connection;
        private IServer m_Server;
        private IDatabase m_DefaultDatabase;
        private IDatabase m_CptCodeDatabase;
        private IDatabase m_IcdCodeDatabase;
        private IDatabase m_StainDatabase;
        private ISubscriber m_Subscriber;

        RedisAppDataConnection()
        {
            this.m_Connection = ConnectionMultiplexer.Connect("10.1.2.70:31578, ConnectTimeout=5000, SyncTimeout=5000");
            this.m_Server = this.m_Connection.GetServer("10.1.2.70:31578");
            this.m_DefaultDatabase = this.m_Connection.GetDatabase((int)RedisDatabaseEnum.Default);
            this.m_CptCodeDatabase = this.m_Connection.GetDatabase((int)RedisDatabaseEnum.CptCodes);
            this.m_IcdCodeDatabase = this.m_Connection.GetDatabase((int)RedisDatabaseEnum.IcdCodes);
            this.m_StainDatabase = this.m_Connection.GetDatabase((int)RedisDatabaseEnum.Stains);
            this.m_Subscriber = this.m_Connection.GetSubscriber();
        }

        public static RedisAppDataConnection Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RedisAppDataConnection();
                    }
                    return instance;
                }
            }
        }        

        public IDatabase DefaultDb
        {
            get { return this.m_DefaultDatabase; }
        }

        public IDatabase CptCodeDb
        {
            get { return this.m_CptCodeDatabase; }
        }

        public IDatabase IcdCodeDb
        {
            get { return this.m_IcdCodeDatabase; }
        }

        public IDatabase StainDb
        {
            get { return this.m_StainDatabase; }
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
