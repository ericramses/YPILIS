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
	public class YpiData : YpiDataBase
    {
		public YpiData() : base(@"Data Source=TESTSQL;Initial Catalog=YPIData;Integrated Security=True")
		{            
		
		}

        [Function(Name = "dbo.ws_GetClientCasesByPSSN")]
        public ISingleResult<ClientWebServices.SearchResult> GetClientCasesByPSSN([Parameter(DbType = "VarChar(max)")] string clientIdString, [Parameter(DbType = "VarChar(max)")] string pssn)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), new object [] {clientIdString, pssn} );
            return ((ISingleResult<ClientWebServices.SearchResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.ws_GetPhysicianCasesByPSSN")]
        public ISingleResult<ClientWebServices.SearchResult> GetPhysicianCasesByPSSN([Parameter(DbType = "VarChar(max)")] string physicianIdString, [Parameter(DbType = "VarChar(max)")] string pssn)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), new object [] {physicianIdString, pssn});
            return ((ISingleResult<ClientWebServices.SearchResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.ws_GetClientCasesByPBirthDate")]
        public ISingleResult<ClientWebServices.SearchResult> GetClientCasesByPBirthdate([Parameter(DbType = "VarChar(max)")] string clientIdString, [Parameter(DbType = "DateTime")] DateTime pbirthdate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), new object[] { clientIdString, pbirthdate });
            return ((ISingleResult<ClientWebServices.SearchResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.ws_GetPhysicianCasesByPBirthDate")]
        public ISingleResult<ClientWebServices.SearchResult> GetPhysicianCasesByPBirthdate([Parameter(DbType = "VarChar(max)")] string physicianIdString, [Parameter(DbType = "DateTime")] DateTime pbirthdate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), new object[] { physicianIdString, pbirthdate });
            return ((ISingleResult<ClientWebServices.SearchResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.ws_GetClientCasesByPatientLastName")]
        public ISingleResult<ClientWebServices.SearchResult> GetClientCasesByPatientLastName([Parameter(DbType = "VarChar(max)")] string clientIdString, [Parameter(DbType = "Varchar(max)")] string pLastName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), new object[] { clientIdString, pLastName });
            return ((ISingleResult<ClientWebServices.SearchResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.ws_GetPhysicianCasesByPatientLastName")]
        public ISingleResult<ClientWebServices.SearchResult> GetPhysicianCasesByPatientLastName([Parameter(DbType = "VarChar(max)")] string physicianIdString, [Parameter(DbType = "Varchar(max)")] string pLastName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), new object[] { physicianIdString, pLastName });
            return ((ISingleResult<ClientWebServices.SearchResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.ws_GetClientCasesByPatientLastNameAndFirstName")]
        public ISingleResult<ClientWebServices.SearchResult> GetClientCasesByPatientLastNameAndFirstName([Parameter(DbType = "VarChar(max)")] string clientIdString, [Parameter(DbType = "Varchar(max)")] string pLastName, [Parameter(DbType = "Varchar(max)")] string pFirstName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), new object[] { clientIdString, pLastName, pFirstName });
            return ((ISingleResult<ClientWebServices.SearchResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.ws_GetPhysicianCasesByPatientLastNameAndFirstName")]
        public ISingleResult<ClientWebServices.SearchResult> GetPhysicianCasesByPatientLastNameAndFirstName([Parameter(DbType = "VarChar(max)")] string physicianIdString, [Parameter(DbType = "Varchar(max)")] string pLastName, [Parameter(DbType = "Varchar(max)")] string pFirstName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), new object[] { physicianIdString, pLastName, pFirstName });
            return ((ISingleResult<ClientWebServices.SearchResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.ws_GetClientRecentCases")]
        public ISingleResult<ClientWebServices.SearchResult> GetClientRecentCases([Parameter(DbType = "VarChar(max)")] string clientIdString, [Parameter(DbType = "DateTime")] DateTime startDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), new object[] { clientIdString, startDate });
            return ((ISingleResult<ClientWebServices.SearchResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.ws_GetPhysicianRecentCases")]
        public ISingleResult<ClientWebServices.SearchResult> GetPhysicianRecentCases([Parameter(DbType = "Varchar(max)")] string physicianIdString, [Parameter(DbType = "DateTime")] DateTime startDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), new object[] { physicianIdString, startDate });
            return ((ISingleResult<ClientWebServices.SearchResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.ws_GetClientCasesNotAcknowledged")]
        public ISingleResult<ClientWebServices.SearchResult> GetClientCasesNotAcknowledged([Parameter(DbType = "VarChar(max)")] string clientIdString)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), new object[] { clientIdString });
            return ((ISingleResult<ClientWebServices.SearchResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.ws_GetPhysicianCasesNotAcknowledged")]
        public ISingleResult<ClientWebServices.SearchResult> GetPhysicianCasesNotDistributed([Parameter(DbType = "VarChar(max)")] string physicianIdString)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), new object[] { physicianIdString });
            return ((ISingleResult<ClientWebServices.SearchResult>)(result.ReturnValue));
        }   
	}
}
