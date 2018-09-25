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
    public class GethAPI
    {
        string m_BaseJSON = "{\"jsonrpc\":\"2.0\",\"method\":\"method_name\",\"params\":[],\"id\":1}";

        //'{"jsonrpc":"2.0","method":"eth_getBlockTransactionCountByHash","params":["0xc94770007dda54cF92009BFF0dE90c06F603a09f"],"id":1}'
        //'{"jsonrpc":"2.0","method":"eth_getBlockTransactionCountByNumber","params":["0xe8"],"id":1}'
        //'{"jsonrpc":"2.0","method":"eth_getBlockByNumber","params":["0x1b4", true],"id":1}'
        //string result = Business.HTTPPost.Post("{\"jsonrpc\":\"2.0\",\"method\":\"eth_accounts\",\"params\":[],\"id\":1}");                    

        public GethAPI()
        {
            
        }

        public string GetBlockByNumber(int blockNumber)
        {
            string result = null;
            JObject postObject = JObject.Parse(this.m_BaseJSON);
            postObject["method"] = "eth_getBlockByNumber";
            string hexValue = String.Format("0x{0:X}", blockNumber);
            JArray paramList = (JArray)postObject["params"];
            paramList.Add(hexValue);
            paramList.Add(true);

            string postResult = this.Post(postObject);
            return result;
        }

        public int GetBlockTransactionCountByNumber(int blockNumber)
        {
            
            JObject postObject = JObject.Parse(this.m_BaseJSON);
            string hexValue = String.Format("0x{0:X}", blockNumber);
            postObject["method"] = "eth_getBlockTransactionCountByNumber";
            JArray paramList = (JArray)postObject["params"];
            paramList.Add(hexValue);
            string postResult = this.Post(postObject);

            JObject resultObject = JObject.Parse(postResult);
            string hexTranCnt = resultObject["result"].ToString().Remove(0,2);
            int tranCnt = Int32.Parse(hexTranCnt, System.Globalization.NumberStyles.HexNumber);
            return tranCnt;
        }

        public string Post(JObject postObject)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.1.1.52:8545");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = postObject.ToString(Formatting.None);
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