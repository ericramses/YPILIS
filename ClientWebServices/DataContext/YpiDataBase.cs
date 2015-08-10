using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Reflection;
using System.Xml.Linq;

namespace ClientWebServices.DataContext
{
    public class YpiDataBase : System.Data.Linq.DataContext
	{        
        public YpiDataBase(string connectionString)
            : base(connectionString)
        {                        
            
		}

        public void Insert<T>(object o)
        {
            ITable table = this.GetTable(typeof(T));
            table.InsertOnSubmit(o);            
        }

        public void Delete<T>(object o)
        {
            ITable table = this.GetTable(typeof(T));
            table.DeleteOnSubmit(o);
        }

        /*
        [Function(Name = "dbo.ws_GetDistributionsNotAcknowledged")]
        public ISingleResult<SearchResult> GetDistributionsNotAcknowledged([Parameter(DbType = "VarChar(MAX)")] string clientIdString)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), clientIdString);
            return ((ISingleResult<SearchResult>)(result.ReturnValue));
        }
        */

        [Function(Name = "dbo.ws_AcknowledgeDistributions")]
        public int AcknowledgeDistributions([Parameter(DbType = "VarChar(MAX)")] string reportDistributionLogIdStringList)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), reportDistributionLogIdStringList);
            return 1;
        }                
	}
}
