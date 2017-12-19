using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Store
{
    public class RedisDB
    {
        protected string m_Name;
        protected int m_Index;
        protected IRedisServer m_Server;

        public RedisDB(AppDBNameEnum dbNameEnum, int index, IRedisServer server)
        {
            this.m_Name = dbNameEnum.ToString();
            this.m_Index = index;
            this.m_Server = server;
        }

        public string Name
        {
            get { return this.m_Name; }
        }

        public int Index
        {
            get { return this.m_Index; }
        }

        public IRedisServer Server
        {
            get { return this.m_Server; }
        }

        public static LuaScript LuaScriptJsonGet(string keys)
        {
            string script = "local data = redis.call('keys', '" + keys + "') " +
                            "local result = {} " +
                            "for i, item in ipairs(data) do " +
                            "result[i] = redis.call('json.get', data[i]) " +
                            "end " +
                            "return result ";
            LuaScript result = LuaScript.Prepare(script);
            return result;
        }

        public static LuaScript LuaScriptHGetAll(string keys)
        {
            string script = "local data = redis.call('keys', '" + keys + "') " +
                            "local result = {} " +
                            "for i, item in ipairs(data) do " +
                            "result[i] = redis.call('HGetAll', data[i]) " +
                            "end " +
                            "return result ";
            LuaScript result = LuaScript.Prepare(script);
            return result;
        }
    }
}
