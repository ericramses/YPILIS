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

        public void Save(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, bool releaseLock)
        {
            //This will be the only place that an AO can be saved.
            //We need to devise a way to make sure AO's are not saved directly throught the OT
            //This function will create the JSON and store it in the JSON property so it will get persisted.
            //The app will call save without regard to wether a lock is aquired. 
            //if ReleaseLock is true then the lock properties will be set to null before saving.
            if(accessionOrder.LockedAquired == true)
            {
            	if(releaseLock == true)
            	{
            		accessionOrder.LockAquiredByHostName = null;
            		accessionOrder.LockAquiredById = null;
            		accessionOrder.LockAquiredByUserName = null;
            		accessionOrder.TimeLockAquired = null;
            	}
            	
            	YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.SubmitChanges(accessionOrder);
            }
            
           	if(releaseLock == true)
        	{
        		YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.Deregister(accessionOrder);
        	}
     }

        public YellowstonePathology.Business.Test.AccessionOrder Refresh(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, bool aquireLock)
        {
            //If it is in the list remove it from the list, unregister it from the OT and then go get it from the database.  If it's not in the list then throw an error.    
            //Register it with OT if AO.LockAquired == true;
            if(this.m_AccessionOrderCollection.Remove(accessionOrder) == false)
            {
            	throw new Exception("AccessionOrder - " + accessionOrder.MasterAccessionNo + " not in AOGW AccessinOrderCollection");
            }
            
            if(YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.IsRegistered(accessionOrder) == true)
            {
	            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.Deregister(accessionOrder);
            }
            
            YellowstonePathology.Business.Test.AccessionOrder result = GetByMasterAccessionNo(accessionOrder.MasterAccessionNo, aquireLock);
            
            return result;
        }

        public YellowstonePathology.Business.Test.AccessionOrder GetByMasterAccessionNo(string masterAccessionNo, bool aquireLock)
        {
            YellowstonePathology.Business.Test.AccessionOrder result = null;
            if (this.m_AccessionOrderCollection.Exists(masterAccessionNo) == true)
            {
                result = this.m_AccessionOrderCollection.GetAccessionOrder(masterAccessionNo);
            }
            else
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
                if (result.LockedAquired == true)
                {
                    YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.RegisterObject(result);
                }
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
