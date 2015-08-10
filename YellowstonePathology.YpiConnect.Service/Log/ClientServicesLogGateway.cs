using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.YpiConnect.Service.Log
{    
    public class ClientServicesLogGateway
    {                        
        public static void Insert(YellowstonePathology.YpiConnect.Contract.Log.ClientServicesLog clientServicesLog)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Insert tblClientServicesLog (EventId, LoggedBy, Description, Details, IpAddress) values (@EventId, @LoggedBy, @Description, @Details, @IpAddress); ";
            cmd.Parameters.Add("@EventId", SqlDbType.Int).Value = clientServicesLog.EventId;
            cmd.Parameters.Add("@LoggedBy", SqlDbType.VarChar, 100).Value = clientServicesLog.LoggedBy;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = clientServicesLog.Description;
            cmd.Parameters.Add("@Details", SqlDbType.VarChar).Value = clientServicesLog.Details;
            cmd.Parameters.Add("@IpAddress", SqlDbType.VarChar).Value = clientServicesLog.IpAddress;
            cmd.CommandType = System.Data.CommandType.Text;
            YellowstonePathology.Business.Domain.Persistence.SqlXmlPersistence.CrudOperations.ExecuteNonQuery(cmd, Business.Domain.Persistence.DataLocationEnum.ProductionData);            
        }
    }
}
