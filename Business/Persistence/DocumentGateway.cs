using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Persistence
{
    public class DocumentGateway
    {
        private static volatile DocumentGateway instance;
        private static object syncRoot = new Object();

        private Stack m_Stack;        

        static DocumentGateway()
        {

        }

        private DocumentGateway() 
        {
            this.m_Stack = new Stack();
        }

        public static DocumentGateway Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DocumentGateway();
                    }
                }

                return instance;
            }
        }

        public void Push(object writer)
        {
            this.m_Stack.Push()
        }

        public void DeleteDocument(object o, object writer)
        {

        }

        public void InsertDocument(object o, Object writer, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_Stack.InsertDocument(o, writer, systemIdentity);
        }        

        public YellowstonePathology.Business.Test.AccessionOrder PullAccessionOrder(string masterAccessionNo, object writer, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Test.AccessionOrder), writer, masterAccessionNo);
            Document document = this.m_Stack.Pull(documentId);

            YellowstonePathology.Business.Test.AccessionOrder result = (YellowstonePathology.Business.Test.AccessionOrder)document.Value;            

            if (documentId.LockAquired == true)
            {
                document.Submit();
            }
            else
            {
                this.BuildAccessionOrder(result, masterAccessionNo);
            }

            return result;
        }

        public YellowstonePathology.Business.Test.AccessionOrder GetAccessionOrderByMasterAccessionNo(string masterAccessionNo)
        {
            YellowstonePathology.Business.Test.AccessionOrder result = new Test.AccessionOrder();
            this.BuildAccessionOrder(result, masterAccessionNo);
            return result;            
        }

        public void BuildAccessionOrder(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string masterAccessionNo)
        {            
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = new YellowstonePathology.Business.User.SystemIdentity(YellowstonePathology.Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AOGWGetByMasterAccessionNo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;
            cmd.Parameters.Add("@AquireLock", SqlDbType.Bit).Value = true;
            cmd.Parameters.Add("@LockAquiredById", SqlDbType.VarChar).Value = systemIdentity.User.UserId;
            cmd.Parameters.Add("@LockAquiredByUserName", SqlDbType.VarChar).Value = systemIdentity.User.UserName;
            cmd.Parameters.Add("@LockAquiredByHostName", SqlDbType.VarChar).Value = System.Environment.MachineName;
            cmd.Parameters.Add("@TimeLockAquired", SqlDbType.DateTime).Value = DateTime.Now;

            YellowstonePathology.Business.Gateway.AccessionOrderBuilder builder = new YellowstonePathology.Business.Gateway.AccessionOrderBuilder();
            builder.Build(cmd, accessionOrder);            
        }

        public void PullTypingShortcut(YellowstonePathology.Business.Typing.TypingShortcut typingShortcut, object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * From tblTypingShortcut where ShortcutId = @ShortcutId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ShortcutId", SqlDbType.Int).Value = typingShortcut.ShortcutId;           

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {                        
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(typingShortcut, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();                        
                    }
                }
            }

            this.m_Stack.Push(typingShortcut, writer);
        }

        public void PullClient(YellowstonePathology.Business.Client.Model.Client client, object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * From tblClient where ClientId = @ClientId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = client.ClientId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(client, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }

            this.m_Stack.Push(client, writer);
        }

        public void PullPhysician(YellowstonePathology.Business.Domain.Physician physician, object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * From tblPhysician where PhysicianId = @PhysicianId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physician.PhysicianId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physician, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }

            this.m_Stack.Push(physician, writer);
        }

        public YellowstonePathology.Business.User.UserPreference PullUserPreference(object writer)
        {
            string hostName = Environment.MachineName;
            SqlCommand cmd = new SqlCommand("Select * from tblUserPreference where HostName = @HostName");
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@HostName", System.Data.SqlDbType.VarChar).Value = hostName;

            YellowstonePathology.Business.User.UserPreference result = null;

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = new User.UserPreference();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();                        
                    }
                }
            }

            this.m_Stack.Push(result, writer);
            return result;
        }

        public YellowstonePathology.Business.ApplicationVersion PullApplicationVersion(object writer)
        {
            YellowstonePathology.Business.ApplicationVersion result = new ApplicationVersion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblApplicationVersion";
            cmd.CommandType = CommandType.Text;
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();                        
                    }
                }
            }

            this.m_Stack.Push(result, writer);
            return result;
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder PullClientOrderByClientOrderId(string clientOrderId, object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "gwGetClientOrderByClientOrderId";

            SqlParameter clientOrderIdParameter = new SqlParameter("@ClientOrderId", SqlDbType.VarChar, 100);
            clientOrderIdParameter.Value = clientOrderId;
            cmd.Parameters.Add(clientOrderIdParameter);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            YellowstonePathology.Business.Gateway.ClientOrderBuilder builder = new Gateway.ClientOrderBuilder(cmd);
            Nullable<int> panelSetId = builder.GetPanelSetId();
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder result = YellowstonePathology.Business.ClientOrder.Model.ClientOrderFactory.GetClientOrder(panelSetId);
            builder.Build(result);  
                          
            this.m_Stack.Push(result, writer);            
            return result;
        }
        
        public void PullMaterialTrackingBatch(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch, object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblMaterialTrackingBatch where MaterialTrackingBatchId = @MaterialTrackingBatchId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@MaterialTrackingBatchId", SqlDbType.VarChar).Value = materialTrackingBatch.MaterialTrackingBatchId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(materialTrackingBatch, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }

            this.m_Stack.Push(materialTrackingBatch, writer);
        }

        public void PullMaterialTrackingLog(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog, object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblMaterialTrackingLog where MaterialTrackingLogId = @MaterialTrackingLogId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@MaterialTrackingLogId", SqlDbType.VarChar).Value = materialTrackingLog.MaterialTrackingLogId;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Business.BaseData.SqlConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(materialTrackingLog, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }

            this.m_Stack.Push(materialTrackingLog, writer);
        }
    }
}
