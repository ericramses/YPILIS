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
        private Dictionary<string, int> m_DatabaseMap;

        public RedisConnection(string ipAddress, string port, string database)
        {            
            this.m_Connection = ConnectionMultiplexer.Connect(ipAddress + ":" + port + ", ConnectTimeout=5000, SyncTimeout=5000");
            this.m_Server = this.m_Connection.GetServer(ipAddress + ":" + port);
            this.m_Database = this.m_Connection.GetDatabase(this.m_DatabaseMap[database]);
            this.m_Subscriber = this.m_Connection.GetSubscriber();
        }
        
        private void SetupMap()
        {
            this.m_DatabaseMap = new Dictionary<string, int>();
            this.m_DatabaseMap.Add("default", 0);
            this.m_DatabaseMap.Add("cptCodes", 1);
            this.m_DatabaseMap.Add("icdCodes", 2);
            this.m_DatabaseMap.Add("stains", 3);
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
