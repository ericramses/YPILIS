﻿using System;
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

        private bool USEMONGO = false;
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

        public void Save(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, bool releaseLock)
        {
            //This will be the only place that an AO can be saved.
            //We need to devise a way to make sure AO's are not saved directly throught the OT
            //This function will create the JSON and store it in the JSON property so it will get persisted.
            //The app will call save without regard to wether a lock is aquired. 
            //if ReleaseLock is true then the lock properties will be set to null before saving.			
        }

        public YellowstonePathology.Business.Test.AccessionOrder Refresh(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, bool aquireLock)
        {
            //If it is in the list remove it from the list, unregister it from the OT and then go get it from the database.  If it's not in the list then throw an error.    
            //Register it with OT if AO.LockAquired == true;        
            YellowstonePathology.Business.Test.AccessionOrder result = new Test.AccessionOrder();
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
                    this.m_ObjectTracker.RegisterObject(result);
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

        public YellowstonePathology.Business.Test.AccessionOrder GetByReportNo(string reportNo, bool aquireLock)
        {
            //If it is a legacy reportno then make a trip to the database to get the masteraccessionno                        
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
    }
}
