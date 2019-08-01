﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.WPH
{
    public class WPHResultView : IResultView
    {        
        private int m_ObxCount;        
        private bool m_SendUnsolicited;
        private bool m_Testing;        

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;
        private YellowstonePathology.Business.User.SystemUser m_SigningPathologist;
        private Business.ClientOrder.Model.ClientOrder m_ClientOrder;

        public WPHResultView(string reportNo, Business.Test.AccessionOrder accessionOrder, bool testing)
        {
            this.m_Testing = testing;
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);            
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrders = Business.Gateway.ClientOrderGateway.GetClientOrdersByExternalOrderId(this.m_AccessionOrder.ExternalOrderId);
            if (clientOrders.Count > 0)
            {
                this.m_ClientOrder = clientOrders[0];
            }  

            if (string.IsNullOrEmpty(this.m_AccessionOrder.IncomingHL7) == true)
            {
                this.m_SendUnsolicited = true;
            }

			this.m_OrderingPhysician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);
            this.m_SigningPathologist = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(this.m_PanelSetOrder.FinaledById);
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

            WPHHl7Client client = new WPHHl7Client();
            OruR01 messageType = new OruR01();

            string locationCode = "YPIIBILLINGS";            
            WPHMSHView msh = new WPHMSHView(client, messageType, locationCode);
            msh.ToXml(document);

            WPHPIDView pid = new WPHPIDView(this.m_AccessionOrder.SvhMedicalRecord, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PBirthdate,
                this.m_AccessionOrder.PSex, this.m_AccessionOrder.SvhAccount, this.m_AccessionOrder.PSSN);
            pid.ToXml(document);

            WPHORCView orc = new WPHORCView(this.m_AccessionOrder.ExternalOrderId, this.m_OrderingPhysician, this.m_AccessionOrder.MasterAccessionNo, OrderStatusEnum.Complete, this.m_AccessionOrder.SystemInitiatingOrder, this.m_SendUnsolicited);
            orc.ToXml(document);

            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);                                   

            YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection universalServiceIdCollection = YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection.GetAll();
            YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService = universalServiceIdCollection.GetByUniversalServiceId(panelSetOrder.UniversalServiceId);            

            WPHOBRView obr = new WPHOBRView(this.m_AccessionOrder.ExternalOrderId, this.m_AccessionOrder.MasterAccessionNo, this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder.SpecimenOrderCollection[0].CollectionDate, this.m_AccessionOrder.SpecimenOrderCollection[0].CollectionTime, this.m_AccessionOrder.AccessionDateTime,
                panelSetOrder.FinalTime, this.m_OrderingPhysician, this.m_SigningPathologist, this.GetResultStatus(), universalService, this.m_SendUnsolicited);
            obr.ToXml(document);

            WPHOBXView wphObxView = WPHOBXViewFactory.GetObxView(panelSetOrder.PanelSetId, this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo, this.m_ObxCount);
            wphObxView.ToXml(document);
            this.m_ObxCount = wphObxView.ObxCount;

            if(this.m_ClientOrder != null)
            {
                WPHOBXCCView wphOBXCCView = new WPHOBXCCView(this.m_ClientOrder);
                wphOBXCCView.ToXml(document);
            }            

            return document;
        }

        private string GetResultStatus()
        {            
            string resultStatus = WPHResultStatsusEnum.S.ToString();
            if(this.m_PanelSetOrder.PanelSetId == 13 || this.m_PanelSetOrder.PanelSetId == 15)
            {
                YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
                if (amendmentCollection.Count != 0) resultStatus = WPHResultStatsusEnum.A.ToString();
            }
            else
            {
                if(this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
                {
                    resultStatus = WPHResultStatsusEnum.A.ToString();
                }
            }
            
            return resultStatus;
        }

        private void WriteDocumentToServer(XElement document)
        {
            string fileExtension = ".HL7.xml";

            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			string serverFileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + "\\" + this.m_PanelSetOrder.ReportNo + fileExtension;
            string interfaceFileName = @"\\YPIIInterface1\ChannelData\Outgoing\WestParkHospital\Prod\" + this.m_PanelSetOrder.ReportNo + fileExtension;
            if (this.m_Testing == true) interfaceFileName = @"\\YPIIInterface1\ChannelData\Outgoing\WestParkHospital\Testing\" + this.m_PanelSetOrder.ReportNo + fileExtension;            
            
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
            else if (string.IsNullOrEmpty(this.m_AccessionOrder.UniversalServiceId) == true)
            {
                result.Message = "The Universal Serivce Id in the AccessionOrder is blank.";
                result.Success = false;
            }
            else if (string.IsNullOrEmpty(pso.UniversalServiceId) == true)
            {
                result.Message = "The Universal Serivce Id in the PanelSetOrder is blank.";
                result.Success = false;
            }
        }
	}
}
