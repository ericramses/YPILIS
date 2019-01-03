using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICFT1ResultView
    {
        private Business.Test.AccessionOrder m_AccessionOrder;
        private Business.Test.PanelSetOrderCPTCodeBill m_PanelSetOrderCPTCodeBill;                

        public EPICFT1ResultView(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill)
        {                        
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrderCPTCodeBill = panelSetOrderCPTCodeBill;            
        }                       

        public void Publish(string basePath)
        {            
            XElement document = new XElement("HL7Message");            

            EPICHl7Client client = new EPICHl7Client();
            DFTP03 messageType = new DFTP03();
            
            YellowstonePathology.Business.Domain.Physician orderingPhysician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);

            string locationCode = "YPIIBILLINGS";
            if (this.m_PanelSetOrderCPTCodeBill.MedicalRecord.StartsWith("A") == true)
            {
                throw new Exception("Cant send CDM for an A Number: " + this.m_AccessionOrder.SvhMedicalRecord);
            }

            EPICMshView msh = new EPICMshView(client, messageType, locationCode);
            msh.ToXml(document);

            EpicPidView pid = new EpicPidView(this.m_PanelSetOrderCPTCodeBill.MedicalRecord, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PBirthdate,
                this.m_AccessionOrder.PSex, this.m_PanelSetOrderCPTCodeBill.Account, this.m_AccessionOrder.PSSN);
            pid.ToXml(document);
            
            Business.Billing.Model.CptCode cptCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone(this.m_PanelSetOrderCPTCodeBill.CPTCode, this.m_PanelSetOrderCPTCodeBill.Modifier);
                
            DateTime transactionDate = m_AccessionOrder.CollectionDate.Value;
            DateTime transactionPostingDate = this.m_PanelSetOrderCPTCodeBill.PostDate.Value;

            EPICFT1View epicFT1View = new EPICFT1View(cptCode, transactionDate, transactionPostingDate, this.m_PanelSetOrderCPTCodeBill.Quantity.ToString(), orderingPhysician, this.m_AccessionOrder.MasterAccessionNo);
            epicFT1View.ToXml(document, 1);
                        
            string fileName = System.IO.Path.Combine(basePath, this.m_PanelSetOrderCPTCodeBill.PanelSetOrderCPTCodeBillId + ".hl7.xml");
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName))
            {
                document.Save(sw);
            }
        }        
    }
}
