﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.ECW
{
    public class ECWResultView : IResultView
    {        
        private int m_ObxCount;        
        private bool m_SendUnsolicited;
        private bool m_Testing;        

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;

        public ECWResultView(string reportNo, Business.Test.AccessionOrder accessionOrder, bool testing)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            this.m_Testing = testing;            

            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

            if (this.m_AccessionOrder.UniversalServiceId.ToUpper() != this.m_PanelSetOrder.UniversalServiceId.ToUpper())
            {
                this.m_SendUnsolicited = true;
            }

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

            ECWHl7Client client = new ECWHl7Client();
            OruR01 messageType = new OruR01();

            string locationCode = "YPIIBILLINGS";            

            ECWMSHView msh = new ECWMSHView(client, messageType, locationCode);
            msh.ToXml(document);

            ECWPIDView pid = new ECWPIDView(this.m_AccessionOrder.SvhMedicalRecord, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PBirthdate,
                this.m_AccessionOrder.PSex, this.m_AccessionOrder.PSSN);
            pid.ToXml(document);

            ECWORCView orc = new ECWORCView(this.m_PanelSetOrder.ExternalOrderId, this.m_OrderingPhysician, this.m_AccessionOrder.MasterAccessionNo, OrderStatusEnum.Complete, this.m_AccessionOrder.SystemInitiatingOrder, this.m_SendUnsolicited);
            orc.ToXml(document);

            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);            

            ResultStatus resultStatus = ResultStatusEnum.Final;
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrder.ReportNo);
            if (amendmentCollection.Count != 0) resultStatus = ResultStatusEnum.Correction;

            YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection universalServiceIdCollection = YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection.GetAll();
            YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService = universalServiceIdCollection.GetByUniversalServiceId(panelSetOrder.UniversalServiceId);

            ECWOBRView obr = new ECWOBRView(this.m_PanelSetOrder.ExternalOrderId, this.m_AccessionOrder.MasterAccessionNo, this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder.SpecimenOrderCollection[0].CollectionDate, this.m_AccessionOrder.SpecimenOrderCollection[0].CollectionTime, this.m_AccessionOrder.AccessionDateTime,
                panelSetOrder.FinalTime, this.m_OrderingPhysician, resultStatus.Value, universalService, this.m_SendUnsolicited);
            obr.ToXml(document);
            
            ECWOBXView ecwOBXView = ECWOBXViewFactory.GetOBXView(panelSetOrder.PanelSetId, this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo, this.m_ObxCount);
            ecwOBXView.ToXml(document);
            this.m_ObxCount = ecwOBXView.ObxCount;                            

            return document;
        }

        private void WriteDocumentToServer(XElement document)
        {
            string fileExtension = ".HL7.xml";

			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			string serverFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + "\\" + this.m_PanelSetOrder.ReportNo + fileExtension;
            string interfaceFileName = @"\\YPIIInterface1\ChannelData\Outgoing\BigSkyDermatology\" + this.m_PanelSetOrder.ReportNo + fileExtension;
            if (this.m_Testing == true) interfaceFileName = @"\\YPIIInterface1\ChannelData\Outgoing\BigSkyDermatology\Test\" + this.m_PanelSetOrder.ReportNo + fileExtension;            
            
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(serverFileName))
            {
                document.Save(sw);
            }

            if (System.IO.File.Exists(interfaceFileName) == false)
            {
                System.IO.File.Copy(serverFileName, interfaceFileName);
            }            
        }

        public void CanSend(YellowstonePathology.Business.Rules.MethodResult result)
        {            
            YellowstonePathology.Business.Test.PanelSetOrder pso = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);

            if (string.IsNullOrEmpty(this.m_OrderingPhysician.Npi) == true)
            {
                result.Message ="The provider NPI is 0.";
                result.Success = false;
            }            
        }
	}
}
