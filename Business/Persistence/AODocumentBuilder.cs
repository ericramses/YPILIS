using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class AODocumentBuilder : DocumentBuilder
    {        
        private SqlCommand m_SQLCommand;

        public AODocumentBuilder(string masterAccessionNo, bool obtainLock)
        {                        
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;

            this.m_SQLCommand = new SqlCommand();
            m_SQLCommand.CommandText = "AOGWGetByMasterAccessionNo";
            m_SQLCommand.CommandType = CommandType.StoredProcedure;
            m_SQLCommand.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;
            m_SQLCommand.Parameters.Add("@AquireLock", SqlDbType.Bit).Value = obtainLock;
            m_SQLCommand.Parameters.Add("@LockAquiredById", SqlDbType.VarChar).Value = systemIdentity.User.UserId;
            m_SQLCommand.Parameters.Add("@LockAquiredByUserName", SqlDbType.VarChar).Value = systemIdentity.User.UserName;
            m_SQLCommand.Parameters.Add("@LockAquiredByHostName", SqlDbType.VarChar).Value = System.Environment.MachineName;
            m_SQLCommand.Parameters.Add("@TimeLockAquired", SqlDbType.DateTime).Value = DateTime.Now;
        }

        public override object BuildNew()
        {
            Console.WriteLine("AO BuildNew.");            
            YellowstonePathology.Business.Test.AccessionOrder result = new Test.AccessionOrder();
            YellowstonePathology.Business.Gateway.AccessionOrderBuilder builder = new YellowstonePathology.Business.Gateway.AccessionOrderBuilder();
            builder.Build(this.m_SQLCommand, result);
            return result;            
        }

        public override void Refresh(object o)
        {                        
            Console.WriteLine("AO Refreshed.");
            YellowstonePathology.Business.Test.AccessionOrder result = (YellowstonePathology.Business.Test.AccessionOrder)o;
            result.SpecimenOrderCollection = new Specimen.Model.SpecimenOrderCollection();
            result.PanelSetOrderCollection = new Test.PanelSetOrderCollection();
            result.ICD9BillingCodeCollection = new Billing.ICD9BillingCodeCollection();
            result.TaskOrderCollection = new Task.Model.TaskOrderCollection();
            YellowstonePathology.Business.Gateway.AccessionOrderBuilder builder = new YellowstonePathology.Business.Gateway.AccessionOrderBuilder();
            builder.Build(this.m_SQLCommand, result);            
        }
    }
}
