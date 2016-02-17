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

        public DocumentCollection Documents
        {
            get { return this.m_Stack.Documents; }
        }

        public void Save(object writer)
        {
            this.m_Stack.Save(writer);
        }

        public void Push(object writer)
        {
            this.m_Stack.Push(writer);
        }

        public void ReleaseLock(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, object writer)
        {
            this.m_Stack.ReleaseLock(accessionOrder, writer);
        }

        public void Flush()
        {
            this.m_Stack.Flush();
        }

        public void Clear(object writer)
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

        public void InsertDocument(object o, Object writer)
        {
            this.m_Stack.InsertDocument(o, writer);
        }        

        public YellowstonePathology.Business.Test.AccessionOrder PullAccessionOrder(string masterAccessionNo, object writer)
        {
            AODocumentBuilder documentBuilder = new AODocumentBuilder(masterAccessionNo);
            DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.Test.AccessionOrder), writer, masterAccessionNo);
            Document document = this.m_Stack.Pull(documentId, documentBuilder);            
            return (YellowstonePathology.Business.Test.AccessionOrder)document.Value;
        }

        public YellowstonePathology.Business.Test.AccessionOrder GetAccessionOrderByMasterAccessionNo(string masterAccessionNo)
        {             
            AODocumentBuilder documentBuilder = new AODocumentBuilder(masterAccessionNo);
            YellowstonePathology.Business.Test.AccessionOrder result = (YellowstonePathology.Business.Test.AccessionOrder)documentBuilder.BuildNew();
            return result;
        }        

        public void PullTypingShortcut(YellowstonePathology.Business.Typing.TypingShortcut typingShortcut, object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * From tblTypingShortcut where ObjectId = @ObjectId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ObjectId", SqlDbType.VarChar).Value = typingShortcut.ObjectId;
            GenericDocumentBuilder builder = new GenericDocumentBuilder(cmd, typeof(YellowstonePathology.Business.Typing.TypingShortcut));

            DocumentId documentId = new DocumentId(typingShortcut, writer);
            Document document = this.m_Stack.Pull(documentId, builder);            
        }

        public void PullClient(YellowstonePathology.Business.Client.Model.Client client, object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT c.*, (SELECT * from tblClientLocation where ClientId = c.ClientId order by Location for xml path('ClientLocation'), type) ClientLocationCollection " +
                "FROM tblClient c where c.ClientId = @ClientId for xml Path('Client'), type";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = client.ClientId;
            ClientDocumentBuilder builder = new ClientDocumentBuilder(cmd);

            DocumentId documentId = new DocumentId(client, writer);
            Document document = this.m_Stack.Pull(documentId, builder);
        }

        public void PullPhysician(YellowstonePathology.Business.Domain.Physician physician, object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * From tblPhysician where PhysicianId = @PhysicianId";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physician.PhysicianId;
            GenericDocumentBuilder builder = new GenericDocumentBuilder(cmd, typeof(YellowstonePathology.Business.Domain.Physician));

            DocumentId documentId = new DocumentId(physician, writer);
            Document document = this.m_Stack.Pull(documentId, builder);
        }

        public void PullClientSupplyOrder(YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder, object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT cso.*, (SELECT * from tblClientSupplyOrderDetail where clientsupplyorderid = cso.ClientSupplyOrderId for xml path('ClientSupplyOrderDetail'), type) ClientSupplyOrderDetailCollection " +
                "FROM tblClientSupplyOrder cso where cso.ClientSupplyOrderId = @ClientSupplyOrderId for xml Path('ClientSupplyOrder'), type";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ClientSupplyOrderId", SqlDbType.VarChar).Value = clientSupplyOrder.ClientSupplyOrderId;
            ClientSupplyOrderDocumentBuilder builder = new ClientSupplyOrderDocumentBuilder(cmd);

            DocumentId documentId = new DocumentId(clientSupplyOrder, writer);
            Document document = this.m_Stack.Pull(documentId, builder);
        }

        public void PullTaskOrder(YellowstonePathology.Business.Task.Model.TaskOrder taskOrder, object writer)
        {
            /*
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
            */          
        }

        public YellowstonePathology.Business.User.UserPreference PullUserPreference(object writer)
        {
            string hostName = Environment.MachineName;
            SqlCommand cmd = new SqlCommand("Select * from tblUserPreference where HostName = @HostName");
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@HostName", System.Data.SqlDbType.VarChar).Value = hostName;
            GenericDocumentBuilder builder = new GenericDocumentBuilder(cmd, typeof(YellowstonePathology.Business.User.UserPreference));

            DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.User.UserPreference), writer, hostName);
            documentId.IsGlobal = true;
            Document document = this.m_Stack.Pull(documentId, builder);
          
            return (YellowstonePathology.Business.User.UserPreference)document.Value;
        }

        public YellowstonePathology.Business.ApplicationVersion GetApplicationVersion(object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from tblApplicationVersion";
            cmd.CommandType = CommandType.Text;
            GenericDocumentBuilder builder = new GenericDocumentBuilder(cmd, typeof(YellowstonePathology.Business.ApplicationVersion));
            YellowstonePathology.Business.ApplicationVersion result = (YellowstonePathology.Business.ApplicationVersion)builder.BuildNew();
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
            ClientOrderDocumentBuilder builder = new ClientOrderDocumentBuilder(cmd);

            DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.ClientOrder.Model.ClientOrder), writer, clientOrderId);
            Document document = this.m_Stack.Pull(documentId, builder);

            return (YellowstonePathology.Business.ClientOrder.Model.ClientOrder)document.Value;
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder PullClientOrder(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "gwGetClientOrderByClientOrderId";
            SqlParameter clientOrderIdParameter = new SqlParameter("@ClientOrderId", SqlDbType.VarChar, 100);
            clientOrderIdParameter.Value = clientOrder.ClientOrderId;
            cmd.Parameters.Add(clientOrderIdParameter);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            ClientOrderDocumentBuilder builder = new ClientOrderDocumentBuilder(cmd);

            DocumentId documentId = new DocumentId(clientOrder, writer);
            Document document = this.m_Stack.Pull(documentId, builder);

            return (YellowstonePathology.Business.ClientOrder.Model.ClientOrder)document.Value;
        }        

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder PullClientOrderByExternalOrderId(string clientOrderId, object writer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "gwGetClientOrderByExternalOrderId";
            SqlParameter clientOrderIdParameter = new SqlParameter("@ExternalOrderId", SqlDbType.VarChar, 100);
            clientOrderIdParameter.Value = clientOrderId;
            cmd.Parameters.Add(clientOrderIdParameter);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            ClientOrderDocumentBuilder builder = new ClientOrderDocumentBuilder(cmd);

            DocumentId documentId = new DocumentId(typeof(YellowstonePathology.Business.User.UserPreference), writer, clientOrderId);
            Document document = this.m_Stack.Pull(documentId, builder);

            return (YellowstonePathology.Business.ClientOrder.Model.ClientOrder)document.Value;
        }

        public void PullMaterialTrackingBatch(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch, object writer)
        {
            /*
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
            */
        }

        public void PullMaterialTrackingLog(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog, object writer)
        {
            /*
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
            */
        }
    }
}
