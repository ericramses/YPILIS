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
        private IDatabase m_PqrsCodeDatabase;
        private ISubscriber m_Subscriber;

        RedisAppDataConnection()
        {
            //this.m_Connection = ConnectionMultiplexer.Connect("localhost:6379, ConnectTimeout=5000, SyncTimeout=5000");
            //this.m_Server = this.m_Connection.GetServer("localhost:6379");
            //this.m_Connection = ConnectionMultiplexer.Connect("10.1.2.70:30075, ConnectTimeout=5000, SyncTimeout=5000");
            //this.m_Server = this.m_Connection.GetServer("10.1.2.70:30075");
            this.m_Connection = ConnectionMultiplexer.Connect("10.1.2.70:31578, ConnectTimeout=5000, SyncTimeout=5000");
            this.m_Server = this.m_Connection.GetServer("10.1.2.70:31578");
            this.m_DefaultDatabase = this.m_Connection.GetDatabase((int)RedisDatabaseEnum.Default);
            this.m_CptCodeDatabase = this.m_Connection.GetDatabase((int)RedisDatabaseEnum.CptCodes);
            this.m_IcdCodeDatabase = this.m_Connection.GetDatabase((int)RedisDatabaseEnum.IcdCodes);
            this.m_StainDatabase = this.m_Connection.GetDatabase((int)RedisDatabaseEnum.Stains);
            this.m_PqrsCodeDatabase = this.m_Connection.GetDatabase((int)RedisDatabaseEnum.PqrsCodes);
            this.m_Subscriber = this.m_Connection.GetSubscriber();

            System.Windows.Application.Current.Exit += Current_Exit;
        }

        private void Current_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            this.m_Connection.Close();
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

        public IDatabase PqrsCodeDb
        {
            get { return this.m_PqrsCodeDatabase; }
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
