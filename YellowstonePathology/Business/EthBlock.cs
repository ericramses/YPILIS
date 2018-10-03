using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business
{
    public class EthBlock
    {        
        string m_Result;
        int m_Number;
        DateTime m_TimeStamp; 

        public EthBlock(string contents)
        {            
            JObject jObject = JObject.Parse(contents);            
            this.m_Result = jObject["result"].ToString();
            JObject jResult = JObject.Parse(this.m_Result);

            this.m_Number = Convert.ToInt32(jResult["number"].ToString(), 16);
            JArray transactions = (JArray)jResult["transactions"];
            if(transactions.Count > 0)
            {
                JObject tran = (JObject)transactions[0];
                string hash = tran["hash"].ToString();
            }

            int seconds = Convert.ToInt32(jResult["timestamp"].ToString(), 16);
            DateTime beginningDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            this.m_TimeStamp = beginningDate.AddSeconds(seconds).ToLocalTime();
        }

        public int Number
        {
            get { return this.m_Number; }
            set { this.m_Number = value; }
        }

        public string Result
        {
            get { return this.m_Result; }
            set { this.m_Result = value; }
        } 
        
        public DateTime TimeStamp
        {
            get { return this.m_TimeStamp; }
            set { this.m_TimeStamp = value; }
        }                
    }
}
