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

        public void Save(object writer)
        {
            this.m_Stack.Save(writer);
        }

        public void Push(object writer)
        {
            this.m_Stack.Push(writer);
        }

        public void Push(Object o, object writer)
        {
            this.m_Stack.Push(o, writer);
        }

        public void DeleteDocument(object o, object writer)
        {
            this.m_Stack.DeleteDocument(o, writer);
        }

        public void InsertDocument(object o, Object writer, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_Stack.InsertDocument(o, writer, systemIdentity);
        }        

        public YellowstonePathology.Business.Test.AccessionOrder PullAccessionOrder(string masterAccessionNo, object writer)
        {
            AODocumentBuilder documentBuilder = new AODocumentBuilder(masterAccessionNo);
            DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Test.AccessionOrder), writer, masterAccessionNo);
            Document document = this.m_Stack.Pull(documentId, false, documentBuilder);            
            return (YellowstonePathology.Business.Test.AccessionOrder)document.Value;
        }

        public YellowstonePathology.Business.Test.AccessionOrder GetAccessionOrderByMasterAccessionNo(string masterAccessionNo)
        {
            YellowstonePathology.Business.Test.AccessionOrder result = new Test.AccessionOrder();
            AODocumentBuilder documentBuilder = new AODocumentBuilder(masterAccessionNo);
            documentBuilder.Build(result);
            return result;
        }        

        public void PullTypingShortcut(YellowstonePathology.Business.Typing.TypingShortcut typingShortcut, object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * From tblTypingShortcut where ObjectId = @ObjectId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ObjectId", SqlDbType.VarChar).Value = typingShortcut.ObjectId;
            GenericDocumentBuilder builder = new GenericDocumentBuilder(cmd);

            DocumentId documentId = new DocumentId(typingShortcut, writer);
            Document document = this.m_Stack.Pull(documentId, false, typingShortcut, builder);            
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

        public void PullTaskOrder(YellowstonePathology.Business.Task.Model.TaskOrder taskOrder, object writer)
        {
            DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Task.Model.TaskOrder), writer, taskOrder.TaskOrderId);
            //Document document = this.m_Stack.Pull(documentId, false);

            XElement documentElement = new XElement("Document");

            SqlCommand cmd = new SqlCommand(" select tsk.*,  ( select tskd.* from tblTaskOrderDetail tskd where tskd.TaskOrderId = tsk.TaskOrderId " +
                "for xml Path('TaskOrderDetail'), type) [TaskOrderDetailCollection] " +
                "from tblTaskOrder tsk where tsk.TaskOrderId = @TaskOrderId  for xml Path('TaskOrder')");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@TaskOrderId", SqlDbType.VarChar).Value = taskOrder.TaskOrderId;

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (XmlReader xmlReader = cmd.ExecuteXmlReader())
                {
                    if (xmlReader.Read() == true)
                    {
                        documentElement = XElement.Load(xmlReader);
                    }
                }
            }

            YellowstonePathology.Business.Persistence.XmlPropertyWriter taskOrderWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(documentElement, taskOrder);
            taskOrderWriter.Write();

            List<XElement> taskOrderDetailElements = (from item in documentElement.Elements("TaskOrderDetailCollection") select item).ToList<XElement>();
            foreach (XElement taskOrderDetailElement in taskOrderDetailElements.Elements("TaskOrderDetail"))
            {
                YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = new YellowstonePathology.Business.Task.Model.TaskOrderDetail();
                YellowstonePathology.Business.Persistence.XmlPropertyWriter taskOrderDetailWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(taskOrderDetailElement, taskOrderDetail);
                taskOrderDetailWriter.Write();
                taskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
            }            
        }

        public YellowstonePathology.Business.User.UserPreference PullUserPreference(object writer)
        {
            string hostName = Environment.MachineName;
            SqlCommand cmd = new SqlCommand("Select * from tblUserPreference where HostName = @HostName");
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@HostName", System.Data.SqlDbType.VarChar).Value = hostName;
            GenericDocumentBuilder builder = new GenericDocumentBuilder(cmd);

            DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.User.UserPreference), writer, hostName);
            Document document = this.m_Stack.Pull(documentId, true, builder);
          
            return (YellowstonePathology.Business.User.UserPreference)document.Value;
        }

        public YellowstonePathology.Business.ApplicationVersion GetApplicationVersion(object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblApplicationVersion";
            cmd.CommandType = CommandType.Text;
            GenericDocumentBuilder builder = new GenericDocumentBuilder(cmd);
            YellowstonePathology.Business.ApplicationVersion result = new ApplicationVersion();
            builder.Build(result);
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
