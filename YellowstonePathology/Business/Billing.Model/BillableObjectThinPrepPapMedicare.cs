﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillableObjectThinPrepPapMedicare : BillableObjectThinPrepPap
    {
        public BillableObjectThinPrepPapMedicare(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
            : base(accessionOrder, reportNo)
        {
            
        }

		public override void PostTechnical(string billTo, string billBy)
		{
			if (this.IsOkToPost() == true)
			{
				foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection)
				{
					YellowstonePathology.Business.Billing.Model.CptCode cptCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone(panelSetOrderCPTCode.CPTCode, panelSetOrderCPTCode.Modifier);
					if (cptCode.IsBillable == true)
					{
						if (cptCode.HasTechnicalComponent == true)
						{
							YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill bill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
							bill.ClientId = this.m_AccessionOrder.ClientId;
							bill.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
							bill.BillTo = billTo;
							bill.BillBy = billBy;
                            
                            if (panelSetOrderCPTCode.Modifier == null)
                            {
                                bill.Modifier = cptCode.GetModifier(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical);
                            }
                            else
                            {
                                bill.Modifier = panelSetOrderCPTCode.Modifier;
                            }

                            if (billTo == BillToEnum.Patient.ToString())
                            {
                                if (this.m_AccessionOrder.ICD9BillingCodeCollection.PapMedicareCodesExist() == true)
                                {
                                    bill.CPTCode = cptCode.GCode;
                                }
                            }

							if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Exists(bill.CPTCode, bill.Modifier) == false)
							{
								this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(bill);
							}                            
						}
					}
				}
			}
		}

        public override void PostGlobal(string billTo, string billBy)
        {
			if (this.IsOkToPost() == true)
			{
				foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection)
				{
					YellowstonePathology.Business.Billing.Model.CptCode cptCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone(panelSetOrderCPTCode.CPTCode, panelSetOrderCPTCode.Modifier);
					if (cptCode.IsBillable == true)
					{
						bool okToPost = true;
						if (cptCode.HasProfessionalComponent == true) okToPost = this.CanPostProfessionalCode();
						if (okToPost == true)
						{
							YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill bill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
							bill.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
							bill.BillTo = billTo;
							bill.BillBy = billBy;
							if (bill.Modifier == null) bill.Modifier = cptCode.GetModifier(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global);
							if (this.m_AccessionOrder.ICD9BillingCodeCollection.PapMedicareCodesExist() == true) bill.CPTCode = cptCode.GCode;

							if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Exists(bill.CPTCode, bill.Modifier) == false)
							{
								this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(bill);
							}
						}
					}
				}
			}
        }

        public override void PostProfessional(string billTo, string billBy)
        {
			if (this.IsOkToPost() == true)
			{
				if (this.CanPostProfessionalCode() == true)
				{
					foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection)
					{
						YellowstonePathology.Business.Billing.Model.CptCode cptCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone(panelSetOrderCPTCode.CPTCode, panelSetOrderCPTCode.Modifier);
						if (cptCode.IsBillable == true)
						{
							if (cptCode.HasProfessionalComponent == true)
							{
								YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill bill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
								bill.FromPanelSetOrderCPTCode(panelSetOrderCPTCode);
								bill.BillTo = billTo;
								bill.BillBy = billBy;
								bill.Modifier = cptCode.GetModifier(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Professional);
								if (this.m_AccessionOrder.ICD9BillingCodeCollection.PapMedicareCodesExist() == true) bill.CPTCode = cptCode.GCode;

								if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Exists(bill.CPTCode, bill.Modifier) == false)
								{
									this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(bill);
								}
							}
						}
					}
				}
			}
        }

		public override void PostClientGCodes(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent)
		{
			if (this.IsOkToPost() == true)
			{
                CptCode cpt88175 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88175", null);
                if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.CPTCodeExists(cpt88175.Code) == true)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetByCPTCode(cpt88175.Code);
                    if (panelSetOrderCPTCodeBill.BillTo == "Client")
                    {
                        this.SetG0145(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, "Patient", "CLNT", panelSetOrderCPTCodeBill.Modifier);
                    }
                }

                CptCode cpt88142 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88142", null);
                if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.CPTCodeExists(cpt88142.Code) == true)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetByCPTCode(cpt88142.Code);
                    if (panelSetOrderCPTCodeBill.BillTo == "Client")
                    {
                        this.SetG0123(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical, "Patient", "CLNT", panelSetOrderCPTCodeBill.Modifier);
                    }
                }                
            }
		}
        
        private void SetGCode(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy, string gCode, string modifier)
		{
			if (this.m_AccessionOrder.ICD9BillingCodeCollection.PapMedicareCodesExist() == true)
			{
				switch (gCode)
				{
					case "G0145":
						this.SetG0145(billingComponent, billTo, billBy, modifier);
						break;
					case "G0123":
						this.SetG0123(billingComponent, billTo, billBy, modifier);
						break;
					case "G0124":
						this.SetG0124(billingComponent, billTo, billBy, modifier);
						break;
				}
			}
		}

		private void SetG0145(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy, string modifier)
		{
            CptCode cptG0145 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("G0145", null);
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
			panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
			panelSetOrderCPTCodeBill.BillTo = billTo;
			panelSetOrderCPTCodeBill.BillBy = billBy;
			panelSetOrderCPTCodeBill.CPTCode = cptG0145.Code;
            panelSetOrderCPTCodeBill.CodeType = cptG0145.CodeType.ToString();
			panelSetOrderCPTCodeBill.Quantity = 1;
            panelSetOrderCPTCodeBill.Modifier = modifier;

            if (panelSetOrderCPTCodeBill.Modifier == null)
            {
                panelSetOrderCPTCodeBill.Modifier = cptG0145.GetModifier(billingComponent);
            }

			if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Exists(panelSetOrderCPTCodeBill.CPTCode, panelSetOrderCPTCodeBill.Modifier) == false)
			{
				this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
			}
		}

        private void SetG0123(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy, string modifier)
		{
            CptCode cptG0123 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("G0123", null);
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
			panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
			panelSetOrderCPTCodeBill.BillTo = billTo;
			panelSetOrderCPTCodeBill.BillBy = billBy;
			panelSetOrderCPTCodeBill.CPTCode = cptG0123.Code;
            panelSetOrderCPTCodeBill.CodeType = cptG0123.CodeType.ToString();
			panelSetOrderCPTCodeBill.Quantity = 1;
            panelSetOrderCPTCodeBill.Modifier = modifier;
			if (panelSetOrderCPTCodeBill.Modifier == null) panelSetOrderCPTCodeBill.Modifier = cptG0123.GetModifier(billingComponent);
			if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Exists(panelSetOrderCPTCodeBill.CPTCode, panelSetOrderCPTCodeBill.Modifier) == false)
			{
				this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
			}
		}

        private void SetG0124(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent, string billTo, string billBy, string modifier)
		{
            CptCode cptG0124 = Store.AppDataStore.Instance.CPTCodeCollection.GetClone("G0124", null);
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
			panelSetOrderCPTCodeBill.ClientId = this.m_AccessionOrder.ClientId;
			panelSetOrderCPTCodeBill.BillTo = billTo;
			panelSetOrderCPTCodeBill.BillBy = billBy;
			panelSetOrderCPTCodeBill.CPTCode = cptG0124.Code;
            panelSetOrderCPTCodeBill.CodeType = cptG0124.CodeType.ToString();
			panelSetOrderCPTCodeBill.Quantity = 1;
            panelSetOrderCPTCodeBill.Modifier = modifier;
			if (panelSetOrderCPTCodeBill.Modifier == null) panelSetOrderCPTCodeBill.Modifier = cptG0124.GetModifier(billingComponent);
			if (this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Exists(panelSetOrderCPTCodeBill.CPTCode, panelSetOrderCPTCodeBill.Modifier) == false)
			{
				this.m_PanelSetOrder.PanelSetOrderCPTCodeBillCollection.Add(panelSetOrderCPTCodeBill);
			}
		}
	}
}
