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

        public const int DEFAULTDBNUM = 0;
        public const int CPTCODEDBNUM = 1;
        public const int ICDCODEDBNUM = 2;
        public const int STAINDBNUM = 3;
        //public const int PQRSDBNUM = 4;
        public const int LOCKSDBNUM = 5;
        public const int VANTAGESLIDESCANSDBNUM = 6;

        private ConnectionMultiplexer m_Connection;
        private IServer m_Server;        
        private ISubscriber m_Subscriber;

        RedisAppDataConnection()
        {
            //this.m_Connection = ConnectionMultiplexer.Connect("10.1.2.70:31578, ConnectTimeout=5000, SyncTimeout=5000"); //app-data
            //this.m_Server = this.m_Connection.GetServer("10.1.2.70:31578");
            this.m_Connection = ConnectionMultiplexer.Connect("10.1.2.70:30075, ConnectTimeout=5000, SyncTimeout=5000");
            this.m_Server = this.m_Connection.GetServer("10.1.2.70:30075");            
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

        public IDatabase GetDB(int databaseNumber)
        {            
            return this.m_Connection.GetDatabase(databaseNumber);
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
