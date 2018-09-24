using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business
{
    public class HTTPPost
    {
        //'{"jsonrpc":"2.0","method":"eth_getBlockTransactionCountByHash","params":["0xc94770007dda54cF92009BFF0dE90c06F603a09f"],"id":1}'
        //'{"jsonrpc":"2.0","method":"eth_getBlockTransactionCountByNumber","params":["0xe8"],"id":1}'
        //'{"jsonrpc":"2.0","method":"eth_getBlockByNumber","params":["0x1b4", true],"id":1}'
        //string result = Business.HTTPPost.Post("{\"jsonrpc\":\"2.0\",\"method\":\"eth_accounts\",\"params\":[],\"id\":1}");            

        private string m_BaseJSON = "{\"jsonrpc\":\"2.0\",\"method\":\"method_name\",\"params\":[],\"id\":1}";
        private JObject m_JOjbect;

        public HTTPPost()
        {
            this.m_JOjbect = JObject.Parse(this.m_BaseJSON);
        }

        public int GetBlockTransactionCountByNumber(int blockNumber)
        {            
            string hexValue = String.Format("0x{0:X}", blockNumber);
            this.m_JOjbect["method"] = "eth_getBlockTransactionCountByNumber";
            JArray paramList = (JArray)this.m_JOjbect["params"];
            paramList.Add(hexValue);
            string postResult = this.Post();

            JObject resultObject = JObject.Parse(postResult);
            return Convert.ToInt32(resultObject["Result"]);
        }

        public string Post()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.1.1.52:8545");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = this.m_JOjbect.ToString(Formatting.None);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }
    }
}

/*
using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                writer.Formatting = Newtonsoft.Json.Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("objectName");
                writer.WriteValue(types[i].Name);
                writer.WritePropertyName("tableName");
                writer.WriteValue("tbl" + types[i].Name);
            }
            */