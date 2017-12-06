using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Business
{    
    public sealed class RedisLocksConnection
    {
        private static RedisLocksConnection instance = null;
        private static readonly object padlock = new object();

        private ConnectionMultiplexer m_Connection;
        private IServer m_Server;
        private IDatabase m_Database;
        private ISubscriber m_Subscriber;

        RedisLocksConnection()
        {            
            this.m_Connection = ConnectionMultiplexer.Connect("10.1.2.25:6379, ConnectTimeout=5000, SyncTimeout=5000, allowAdmin=true");
            this.m_Server = this.m_Connection.GetServer("10.1.2.25:6379");            
            this.m_Database = this.m_Connection.GetDatabase();
            this.m_Subscriber = this.m_Connection.GetSubscriber();

            System.Windows.Application.Current.Exit += Current_Exit;          
        }

        private void Current_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            this.m_Connection.Close();
        }

        public void KillOrphanedConnections()
        {
            //this.m_Server.ClientKill
            ClientInfo[] clientInfo = this.m_Server.ClientList(CommandFlags.None);
            for(int i=0; i< clientInfo.Length; i++)
            {
                if(clientInfo[i].Name.ToUpper() == Environment.MachineName.ToUpper())
                {
                    Console.WriteLine(clientInfo[i].Name);
                }                
            }
        }

        public static RedisLocksConnection Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RedisLocksConnection();
                    }
                    return instance;
                }
            }
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


