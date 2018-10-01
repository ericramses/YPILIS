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
        string m_Contents;        
        string m_Result;        

        public EthBlock(string contents)
        {
            this.m_Contents = contents;
            JObject jObject = JObject.Parse(this.m_Contents);            
            this.m_Result = jObject["result"].ToString();
            JObject jResult = JObject.Parse(this.m_Result);       
                 
            JArray transactions = (JArray)jResult["transactions"];
            JObject tran = (JObject)transactions[0];
            string hash = tran["hash"].ToString();
            
        }        

        public string Result
        {
            get { return this.m_Result; }
            set { this.m_Result = value; }
        }        
    }
}
