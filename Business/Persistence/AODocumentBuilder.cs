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
        private string m_MasterAccessionNo;

        public AODocumentBuilder(string masterAccessionNo)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
        }

        public override void Build(object o)
        {
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = (YellowstonePathology.Business.Test.AccessionOrder)o;
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = new YellowstonePathology.Business.User.SystemIdentity(YellowstonePathology.Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AOGWGetByMasterAccessionNo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = this.m_MasterAccessionNo;
            cmd.Parameters.Add("@AquireLock", SqlDbType.Bit).Value = true;
            cmd.Parameters.Add("@LockAquiredById", SqlDbType.VarChar).Value = systemIdentity.User.UserId;
            cmd.Parameters.Add("@LockAquiredByUserName", SqlDbType.VarChar).Value = systemIdentity.User.UserName;
            cmd.Parameters.Add("@LockAquiredByHostName", SqlDbType.VarChar).Value = System.Environment.MachineName;
            cmd.Parameters.Add("@TimeLockAquired", SqlDbType.DateTime).Value = DateTime.Now;

            YellowstonePathology.Business.Gateway.AccessionOrderBuilder builder = new YellowstonePathology.Business.Gateway.AccessionOrderBuilder();
            builder.Build(cmd, accessionOrder);
        }
    }
}
