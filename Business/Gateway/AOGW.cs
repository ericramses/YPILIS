using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Gateway
{    
    public sealed class AOGW
    {
        private static volatile AOGW instance;
        private static object syncRoot = new Object();
        
        private bool USEMONGO = false;

        private List<object> m_Objects;
        private List<object> m_OriginalValues;

        static AOGW()
        {

        }

        private AOGW()
        {
            this.m_Objects = new List<object>();
            this.m_OriginalValues = new List<object>();
        }

        public YellowstonePathology.Business.Test.AccessionOrder GetByMasterAccessionNo(string masterAccessionNo, bool aquireLock)
        {
            YellowstonePathology.Business.Test.AccessionOrder result = null;
            
            /*
	        if (this.m_AccessionOrderCollection.Exists(masterAccessionNo) == true)
            {
	        	result = this.m_AccessionOrderCollection.GetAccessionOrder(masterAccessionNo);
	        	if(result.LockedAquired == false)
	        	{
		        	this.m_AccessionOrderCollection.Remove(masterAccessionNo);
		        	result = null;
	        	}
            }

            if(result == null)
            {
	            if (USEMONGO == false)
	            {
	                result = this.BuildFromSQL(masterAccessionNo, aquireLock);
	            }
	            else
	            {
	                result = this.BuildFromMongo(masterAccessionNo, aquireLock);
	            }
	            
	            this.m_AccessionOrderCollection.Add(result);                

                YellowstonePathology.Business.Persistence.ObjectCloner objectCloner = new YellowstonePathology.Business.Persistence.ObjectCloner();
                YellowstonePathology.Business.Test.AccessionOrder clonedAccessionOrder = (YellowstonePathology.Business.Test.AccessionOrder)objectCloner.Clone(result);
                this.m_AccessionOrderOriginalValuesCollection.Add(clonedAccessionOrder);
            }
	        */

            return result;
        }

        private YellowstonePathology.Business.Test.AccessionOrder BuildFromMongo(string masterAccessionNo, bool aquireLock)
        {
            YellowstonePathology.Business.Test.AccessionOrder result = null;
            return result;
        }

        private YellowstonePathology.Business.Test.AccessionOrder BuildFromSQL(string masterAccessionNo, bool aquireLock)
        {
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = new YellowstonePathology.Business.User.SystemIdentity(YellowstonePathology.Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
            AccessionOrderBuilder accessionOrderBuilder = new AccessionOrderBuilder();
            XElement document = null;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AOGWGetByMasterAccessionNo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;
            cmd.Parameters.Add("@AquireLock", SqlDbType.Bit).Value = aquireLock;
            cmd.Parameters.Add("@LockAquiredById", SqlDbType.VarChar).Value = systemIdentity.User.UserId;
            cmd.Parameters.Add("@LockAquiredByUserName", SqlDbType.VarChar).Value = systemIdentity.User.UserName;
            cmd.Parameters.Add("@LockAquiredByHostName", SqlDbType.VarChar).Value = System.Environment.MachineName;
            cmd.Parameters.Add("@TimeLockAquired", SqlDbType.DateTime).Value = DateTime.Now;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (XmlReader xmlReader = cmd.ExecuteXmlReader())
                {
                    if (xmlReader.Read() == true)
                    {
                        document = XElement.Load(xmlReader, LoadOptions.PreserveWhitespace);
                    }
                }
            }

            accessionOrderBuilder.Build(document);            
            return accessionOrderBuilder.AccessionOrder;
        }

        public YellowstonePathology.Business.Persistence.SubmissionResult SubmitChanges(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, bool releaseLock)
        {
            YellowstonePathology.Business.Persistence.SubmissionResult result = new YellowstonePathology.Business.Persistence.SubmissionResult();
            if (releaseLock == true)
            {
                accessionOrder.LockAquiredByHostName = null;
                accessionOrder.LockAquiredById = null;
                accessionOrder.LockAquiredByUserName = null;
                accessionOrder.TimeLockAquired = null;
            }

            //YellowstonePathology.Business.Persistence.SqlCommandSubmitter sqlCommandSubmitter = this.GetSqlCommands(accessionOrder);
            //sqlCommandSubmitter.SubmitChanges();

            return result;
        }                

        public static AOGW Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new AOGW();
                    }
                }

                return instance;
            }
        }
    }
}
