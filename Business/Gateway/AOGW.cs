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
	
	//we need to be careful not to change how this single class structure is setup because this structure is thread safe.
    public sealed class AOGW
    {
        //Properties in AO: LockAquiredById, LockAquiredByUserName, LockAquiredByHostName, TimeLockAquired
        //Need a stored procedure that takes the above parameters and a masteraccessionno  and sets them if they are not null.
        //Assume if LockAuqiredById is null then they are all null.

        private static readonly AOGW instance = new AOGW();

        private YellowstonePathology.Business.Test.AccessionOrderCollection m_AccessionOrderCollection;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

        static AOGW()
        {

        }

        private AOGW()
        {
            //This collection will hold all AO's in use by the application.
            //AO's will be removed from the collection when they are released.
            //saving an AO will not cause it to be removed from the collection.
            this.m_AccessionOrderCollection = new Test.AccessionOrderCollection();
            this.m_ObjectTracker = new Persistence.ObjectTracker();
        }

        public AOSaveResult Save(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            //This will be the only place that an AO can be saved.
            //We need to devise a way to make sure AO's are not saved directly throught the OT
            //This function will create the JSON and store it in the JSON property so it will get persisted.
            AOSaveResult result = new AOSaveResult();            
            accessionOrder.JSON = Persistence.JSONObjectWriter.Write(accessionOrder);
            this.m_ObjectTracker.SubmitChanges(accessionOrder);
            result.OK = true;
            return result;
        }

        public AOSaveResult Release(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            //used to release lock, side affect is that all data will be saved as well.
            //If lock is aquired (property in accessionorder) then
            //this method will change the LockedById, LockedByUserName, TimeLockeAquired to null and then call save.            
            //this method will remove the AO from the collection
            //If the AO does not exist in the collection then throw an error.
            AOSaveResult result = new AOSaveResult();
            if(accessionOrder.LockedAquired == true)
            {
                bool wasRemoved = this.m_AccessionOrderCollection.Remove(accessionOrder);
                if (wasRemoved == false) throw new Exception("AOGW:Release. Accession not in collection.  MasterAccessionNo = " + accessionOrder.MasterAccessionNo);

                accessionOrder.LockAquiredById = null;
                accessionOrder.LockAquiredByUserName = null;
                accessionOrder.LockAquiredByHostName = null;
                accessionOrder.TimeLockAquired = null;

                Save(accessionOrder);
                this.m_ObjectTracker.Deregister(accessionOrder);
                result.OK = true;
            }
            else
            {
                result.OK = false;
                result.Message = "Unable to release lock as the lock was not acquired.";
            }
            return result;
        }

        public YellowstonePathology.Business.Test.AccessionOrder Refresh(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, bool aquireLock)
        {
            //If it is in the list remove it from the list, unregister it from the OT and then go get it from the database.  If it's not in the list then throw an error.    
            //Register it with OT if AO.LockAquired == true;
            bool wasRemoved = this.m_AccessionOrderCollection.Remove(accessionOrder);
            if (wasRemoved == false) throw new Exception("AOGW:Refresh. Accession not in collection.  MasterAccessionNo = " + accessionOrder.MasterAccessionNo);

            if(this.m_ObjectTracker.IsRegistered(accessionOrder) == true)
            {
                this.m_ObjectTracker.Deregister(accessionOrder);
            }

            return GetByMasterAccessionNo(accessionOrder.MasterAccessionNo, aquireLock);
        }

        public YellowstonePathology.Business.Test.AccessionOrder GetByMasterAccessionNo(string masterAccessionNo, bool aquireLock)
        {
            //always see if you already have it before you go to the database
            //if it's alread here then return it
            //go get it, put it in the collection and return it
            //If AO.LockAquired == true then register it in the OT
            YellowstonePathology.Business.Test.AccessionOrder result = null;
            if (this.m_AccessionOrderCollection.Exists(masterAccessionNo) == true)
            {
                result = this.m_AccessionOrderCollection.GetAccessionOrder(masterAccessionNo);
            }
            else
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
                result = accessionOrderBuilder.AccessionOrder;
                this.m_AccessionOrderCollection.Add(result);
                if(result.LockedAquired == true)
                {
                    this.m_ObjectTracker.RegisterObject(result);
                }
            }

            return result;
        }

        public YellowstonePathology.Business.Test.AccessionOrder GetByReportNo(string reportNo, bool aquireLock)
        {
            //always see if you already have it before you go to the database
            //if it's alread here then return it
            //go get it, put it in the collection and return it
            //If AO.LockAquired == true then register it in the OT                        
            YellowstonePathology.Business.OrderIdParser orderIdParser = new OrderIdParser(reportNo);
            string masterAccessionNo = orderIdParser.MasterAccessionNo;
            return GetByMasterAccessionNo(masterAccessionNo, aquireLock);
        }

        public static AOGW Instance
        {
            get
            {
                return instance;
            }
        }

        public List<string> GetCasesWithNullJSONString(int numberOfAccessionsToRetrieve, int year)
        {
            List<string> result = new List<string>();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select top (" + numberOfAccessionsToRetrieve.ToString() + ") MasterAccessionNo from tblAccessionOrder where JSON is null and AccessionDate < '1/1/" + year.ToString() + "'";
            cmd.CommandType = CommandType.Text;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(dr[0].ToString());
                    }
                }
            }
            return result;
        }
    }
}
