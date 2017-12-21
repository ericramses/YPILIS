using System;
using System.Collections.ObjectModel;
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

        public static LuaScript LuaScriptJsonGetKeys(Collection<YellowstonePathology.Business.Billing.Model.CPTCodeWithModifier> codesAndModifiers)
        {
            StringBuilder script = new StringBuilder();
            script.Append("local ids = {} ");
            int idx = 1;
            foreach(YellowstonePathology.Business.Billing.Model.CPTCodeWithModifier codeWithModifier in codesAndModifiers)
            {
                string value = "ids[" + idx.ToString() + "] = '" + codeWithModifier.Code + "' ";
                script.Append(value);
                idx++;
            }
            script.Append("local result = {} ");
            script.Append("for i, item in ipairs(ids) do ");
            script.Append("result[i] = redis.call('json.get', ids[i]) ");
            script.Append("end ");
            script.Append("return result ");
            LuaScript result = LuaScript.Prepare(script.ToString());
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
