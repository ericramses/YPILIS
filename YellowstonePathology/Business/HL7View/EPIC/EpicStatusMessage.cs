using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICStatusMessage
    {
        private int m_ObxCount;
        private XElement m_Document;
        private YellowstonePathology.Business.Domain.Physician m_OrderingPhysician;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;        
        private OrderStatus m_OrderStatus;
        private YellowstonePathology.Business.ClientOrder.Model.UniversalService m_UniversalService;        
        private string m_ResultMessage;
        private string m_ResultStatus;

        protected string m_ServerFileName;
        protected string m_InterfaceFilename;
        protected DateTime m_DateOfService;

        public EPICStatusMessage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, OrderStatus orderStatus, YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService, string resultMessage, string resultStatus, DateTime dateOfService)
		{
			this.m_ClientOrder = clientOrder;
			this.m_OrderingPhysician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByNpi(this.m_ClientOrder.ProviderId);
			this.m_OrderStatus = orderStatus;
            this.m_UniversalService = universalService;            
            this.m_ResultMessage = resultMessage;
            this.m_ResultStatus = resultStatus;
            this.m_DateOfService = dateOfService;
            this.SetupFileNames();
		}

        public virtual void SetupFileNames()
        {
            string newGuid = Guid.NewGuid().ToString();
            this.m_ServerFileName = @"\\YPIIInterface1\ChannelData\Outgoing\1002\In\" + newGuid + ".xml";
            this.m_InterfaceFilename = @"\\YPIIInterface1\ChannelData\Outgoing\1002\" + newGuid + ".xml";            
        }

        public YellowstonePathology.Business.Rules.MethodResult Send()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            if (result.Success == true)
            {
                this.m_Document = new XElement("HL7Message");
                this.m_ObxCount = 1;

                EPICHl7Client client = new EPICHl7Client();
                OruR01 messageType = new OruR01();

                string locationCode = "YPIIBILLINGS";
                if (this.m_ClientOrder.SvhMedicalRecord.StartsWith("A") == true)
                {
                    locationCode = "SVHNPATH";
                }

                EPICMshView msh = new EPICMshView(client, messageType, locationCode);                
                msh.ToXml(this.m_Document);

                PidView pid = new PidView(this.m_ClientOrder.SvhMedicalRecord, this.m_ClientOrder.PLastName, this.m_ClientOrder.PFirstName, this.m_ClientOrder.PBirthdate,
                    this.m_ClientOrder.PSex, this.m_ClientOrder.SvhAccountNo, this.m_ClientOrder.PSSN);
                pid.ToXml(this.m_Document);

                EPICStatusOrcView orc = new EPICStatusOrcView(this.m_ClientOrder.ExternalOrderId, this.m_OrderingPhysician, this.m_OrderStatus);
                orc.ToXml(this.m_Document);

                EPICStatusObrView obr = new EPICStatusObrView(this.m_ClientOrder.ExternalOrderId, string.Empty, this.m_DateOfService, null, this.m_OrderingPhysician, this.m_ResultStatus, this.m_UniversalService);
                obr.ToXml(this.m_Document);

                EPICStatusObxView obx = new EPICStatusObxView(m_ObxCount, this.m_ResultStatus, this.m_ResultMessage);
                obx.ToXml(this.m_Document);
                this.m_ObxCount = obx.ObxCount;                

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(this.m_ServerFileName))
                {
                    this.m_Document.Save(sw);
                }                
            }
            return result;
        }

        private YellowstonePathology.Business.Rules.MethodResult OkToSend()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();

            if (string.IsNullOrEmpty(this.m_ClientOrder.UniversalServiceId) == true)
            {
                result.Success = false;
                result.Message = "The universal service id is null";
            }

            YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceNone universalServiceNone = new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceNone();
            if (this.m_UniversalService.ApplicationName == universalServiceNone.ApplicationName)
            {
                result.Success = false;
                result.Message = "The universal service is NONE.";
            }

            return result;
        }
    }
}
