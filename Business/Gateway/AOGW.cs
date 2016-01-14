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
        private static readonly AOGW instance = new AOGW();

        private bool USEMONGO = false;
        private YellowstonePathology.Business.Test.AccessionOrderCollection m_AccessionOrderCollection;

        static AOGW()
        {

        }

        private AOGW()
        {            
            this.m_AccessionOrderCollection = new Test.AccessionOrderCollection();
        }

        public void Save(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, bool releaseLock, object registeredBy)
        {            
            if(accessionOrder.LockedAquired == true)
            {
            	if(releaseLock == true)
            	{
            		accessionOrder.LockAquiredByHostName = null;
            		accessionOrder.LockAquiredById = null;
            		accessionOrder.LockAquiredByUserName = null;
            		accessionOrder.TimeLockAquired = null;
            	}
            	
            	YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.SubmitChanges(accessionOrder, registeredBy);
            }
            
           	if(releaseLock == true)
        	{
        		YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.CleanUp(registeredBy);
        	}
     }

        public YellowstonePathology.Business.Test.AccessionOrder Refresh(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, bool aquireLock, object registeredBy)
        {            
            if(this.m_AccessionOrderCollection.Remove(accessionOrder) == false)
            {
            	throw new Exception("AccessionOrder - " + accessionOrder.MasterAccessionNo + " not in AOGW AccessinOrderCollection");
            }
            
            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.CleanUp(registeredBy);            
            YellowstonePathology.Business.Test.AccessionOrder result = GetByMasterAccessionNo(accessionOrder.MasterAccessionNo, aquireLock, registeredBy);
            
            return result;
        }

        public YellowstonePathology.Business.Test.AccessionOrder GetByMasterAccessionNo(string masterAccessionNo, bool aquireLock, object registeredBy)
        {
            YellowstonePathology.Business.Test.AccessionOrder result = null;
            if (USEMONGO == false)
            {
                result = this.BuildFromSQL(masterAccessionNo, aquireLock);
            }
            else
            {
                result = this.BuildFromMongo(masterAccessionNo, aquireLock);
            }
            
	        if (this.m_AccessionOrderCollection.Exists(masterAccessionNo) == true)
            {
                YellowstonePathology.Business.Test.AccessionOrder accessionToRemove = this.m_AccessionOrderCollection.GetAccessionOrder(masterAccessionNo);
                this.m_AccessionOrderCollection.Remove(accessionToRemove);
	            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.CleanUp(registeredBy);            
            }

            this.m_AccessionOrderCollection.Add(result);

            if (result.LockedAquired == true)
            {
                YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.RegisterObject(result, registeredBy);
            }

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

        public static AOGW Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
