using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Billing
{
    public class BillableObjectTechnicalOnlyAutopsy : BillableObject
    {
        public BillableObjectTechnicalOnlyAutopsy(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo) 
            : base(accessionOrder, reportNo)
        {

        }        

        public override void SetPanelSetOrderCPTCodes()
        {
            YellowstonePathology.Business.Billing.Model.CptCodeDefinition.AutopsyTechnicalOnly autopsy = new Business.Billing.Model.CptCodeDefinition.AutopsyTechnicalOnly();
            if (this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Exists(autopsy.Code, 1) == false)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCode.Quantity = 1;
                panelSetOrderCPTCode.CPTCode = autopsy.Code;
                panelSetOrderCPTCode.Modifier = autopsy.Modifier;
                panelSetOrderCPTCode.CodeableDescription = "Autopsy";
                panelSetOrderCPTCode.CodeableType = "BillableTest";
                panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.SystemGenerated;                
                panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
            }
        }

		public override void PostTechnical(string billTo, string billBy)
		{
            YellowstonePathology.Business.Billing.Model.CptCodeDefinition.AutopsyTechnicalOnly autopsy = new Business.Billing.Model.CptCodeDefinition.AutopsyTechnicalOnly();
            foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection)
            {                                
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill item = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                item.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
                item.BillTo = billTo;
                item.BillBy = billBy;
                item.Modifier = autopsy.Modifier;                    

                this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(item);                
            }
        }

		public override void PostProfessional(string billTo, string billBy)
		{
			//Do nothing
		}

		public override void PostGlobal(string billTo, string billBy)
		{
			// Do nothing
		}

		public override void PostClientGCodes(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent)
		{
            // Do nothing
        }
    }
}
