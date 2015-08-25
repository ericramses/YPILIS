using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.Panther
{
    public class PantherOrder
    {        
        private int m_ObxCount;
        private string m_ReportNo;
        private bool m_SendUnsolicited;        

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;

        public PantherOrder(string reportNo)
        {
            this.m_ReportNo = reportNo;                        
			
			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(reportNo);
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);            

			this.m_OrderingPhysician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);           
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

        private XElement CreateDocument()
        {
            XElement document = new XElement("HL7Message");
            this.m_ObxCount = 1;

            PantherHL7Client client = new PantherHL7Client();
            OruR01 messageType = new OruR01();

            string locationCode = "YPIIBILLINGS";
            if (this.m_AccessionOrder.SvhMedicalRecord.StartsWith("A") == true)
            {
                locationCode = "SVHNPATH";
            }

            PantherMSH msh = new PantherMSH(client, messageType, locationCode);
            msh.ToXml(document);

            PantherPID pid = new PantherPID(this.m_AccessionOrder.SvhMedicalRecord, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PBirthdate,
                this.m_AccessionOrder.PSex, this.m_AccessionOrder.SvhAccount, this.m_AccessionOrder.PSSN);
            pid.ToXml(document);

            PantherORC orc = new PantherORC(this.m_AccessionOrder.ExternalOrderId, this.m_OrderingPhysician, this.m_AccessionOrder.MasterAccessionNo, OrderStatusEnum.Complete, this.m_AccessionOrder.SystemInitiatingOrder, this.m_SendUnsolicited);
            orc.ToXml(document);

            //PantherOBR obr = new PantherOBR(this.m_AccessionOrder.ExternalOrderId, this.m_AccessionOrder.MasterAccessionNo, this.m_ReportNo, this.m_AccessionOrder.SpecimenOrderCollection[0].CollectionDate, this.m_AccessionOrder.SpecimenOrderCollection[0].CollectionTime, this.m_AccessionOrder.AccessionDateTime,
            //    this.m_PanelSetOrder.FinalTime, this.m_OrderingPhysician, resultStatus.Value, universalService, this.m_SendUnsolicited);
            //obr.ToXml(document);                                   

            return document;
        }

        private void WriteDocumentToServer(XElement document)
        {
            string fileExtension = ".HL7.xml";

			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_ReportNo);
			string serverFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + "\\" + this.m_ReportNo + fileExtension;
            string interfaceFileName = @"\\ypiiinterface1\ChannelData\Outgoing\" + this.m_ReportNo + fileExtension;            
            
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(serverFileName))
            {
                document.Save(sw);
            }

            if (System.IO.File.Exists(interfaceFileName) == false)
            {
                System.IO.File.Copy(serverFileName, interfaceFileName);
            }            
        }        
	}
}
