using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICFT1ResultView : IResultView
    {
        private Business.Test.AccessionOrder m_AccessionOrder;
        private Business.Test.PanelSetOrderCPTCodeBill m_PanelSetOrderCPTCodeBill;        

        private bool m_Testing;

        public EPICFT1ResultView(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill, bool testing)
        {            
            this.m_Testing = testing;
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrderCPTCodeBill = panelSetOrderCPTCodeBill;            
        }

        public XElement GetDocument()
        {
            return CreateDocument();
        }

        public void Send(YellowstonePathology.Business.Rules.MethodResult result)
        {
            XElement detailDocument = CreateDocument();
            this.WriteDocumentToServer(detailDocument);

            result.Success = true;
            result.Message = "An HL7 message was created and sent to the interface.";
        }

        public void Save(string path)
        {
            XElement detailDocument = CreateDocument();
            string id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            string fileExtension = "." + this.m_AccessionOrder.MasterAccessionNo + ".HL7.xml";
            string interfaceFileName = System.IO.Path.Combine(path, id + fileExtension);
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(interfaceFileName))
            {
                detailDocument.Save(sw);
            }
        }

        private XElement CreateDocument()
        {
            XElement document = new XElement("HL7Message");            

            EPICHl7Client client = new EPICHl7Client();
            DFTP03 messageType = new DFTP03();
            
            YellowstonePathology.Business.Domain.Physician orderingPhysician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);

            string locationCode = "YPIIBILLINGS";
            if (this.m_AccessionOrder.SvhMedicalRecord.StartsWith("A") == true)
            {
                throw new Exception("Cant send CDM for an A Number: " + this.m_AccessionOrder.SvhMedicalRecord);
            }

            EPICMshView msh = new EPICMshView(client, messageType, locationCode);
            msh.ToXml(document);

            EpicPidView pid = new EpicPidView(this.m_AccessionOrder.SvhMedicalRecord, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PBirthdate,
                this.m_AccessionOrder.PSex, this.m_AccessionOrder.SvhAccount, this.m_AccessionOrder.PSSN);
            pid.ToXml(document);
            
            Business.Billing.Model.CptCode cptCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone(this.m_PanelSetOrderCPTCodeBill.CPTCode, this.m_PanelSetOrderCPTCodeBill.Modifier);
                
            DateTime transactionDate = m_AccessionOrder.CollectionDate.Value;
            DateTime transactionPostingDate = this.m_PanelSetOrderCPTCodeBill.PostDate.Value;

            EPICFT1View epicFT1View = new EPICFT1View(cptCode, transactionDate, transactionPostingDate, this.m_PanelSetOrderCPTCodeBill.Quantity.ToString(), orderingPhysician, this.m_AccessionOrder.MasterAccessionNo);
            epicFT1View.ToXml(document, 1);

            return document;
        }

        private void WriteDocumentToServer(XElement document)
        {
            string id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            string fileExtension = ".HL7.xml";
            string interfaceFileName = @"\\YPIIInterface1\ChannelData\Outgoing\1002\Test\ft1\" + id + fileExtension;            
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(interfaceFileName))
            {
                document.Save(sw);
            }
        }

        public void CanSend(YellowstonePathology.Business.Rules.MethodResult result)
        {         
            if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhAccount) == true)
            {
                result.Message = "The SVH Account is blank.";
                result.Success = false;
            }
            else if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhMedicalRecord) == true)
            {
                result.Message = "The SVH MRN is blank.";
                result.Success = false;
            }
            else if (this.m_AccessionOrder.SvhMedicalRecord.ToUpper().Contains("W") == true)
            {
                result.Message = "The Medical Record Number has a W in it.";
                result.Success = false;
            }
            else if (this.m_AccessionOrder.SvhMedicalRecord.ToUpper().Contains("W") == true)
            {
                result.Message = "The Medical Record Number has a W in it.";
                result.Success = false;
            }           
        }
    }
}
