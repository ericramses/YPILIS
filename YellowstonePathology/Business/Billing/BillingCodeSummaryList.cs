using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Billing
{
    public class BillingCodeSummaryList
    {
        List<BillingCodeSummaryListItem> m_CptCodeList;
        List<BillingCodeSummaryListItem> m_Icd9CodeList;
        YellowstonePathology.Business.Billing.CptCodeList m_CptCodeSortList;

        public BillingCodeSummaryList()
        {
            this.m_CptCodeList = new List<BillingCodeSummaryListItem>();            
            this.m_Icd9CodeList = new List<BillingCodeSummaryListItem>();

            this.m_CptCodeSortList = new CptCodeList();
            this.m_CptCodeSortList.SetFillByAll();
            this.m_CptCodeSortList.Fill();
        }        

        public List<BillingCodeSummaryListItem> CptCodeList
        {
            get { return this.m_CptCodeList; }
        }

        public List<BillingCodeSummaryListItem> Icd9CodeList
        {
            get { return this.m_Icd9CodeList; }
        }

        public void Fill(YellowstonePathology.Business.Billing.BillingSpecimenList billingSpecimenList)
        {
            foreach(YellowstonePathology.Business.Billing.BillingSpecimenListItem billingSpecimenListItem in billingSpecimenList)
            {
                foreach (CptBillingCodeListItem cptItem in billingSpecimenListItem.CptBillingCodeList)
                {
                    bool isFound = false;
                    foreach (BillingCodeSummaryListItem summaryItem in this.m_CptCodeList)
                    {
                        if (cptItem.CptCode == summaryItem.Code)
                        {
                            isFound = true;
                            summaryItem.Quantity = summaryItem.Quantity + cptItem.Quantity;
                            break;
                        }
                    }
                    if (isFound == false)
                    {
                        BillingCodeSummaryListItem summaryListItem = new BillingCodeSummaryListItem();
                        summaryListItem.Quantity = cptItem.Quantity;
                        summaryListItem.Code = cptItem.CptCode;
                        summaryListItem.CodeOrder = cptItem.CodeOrder;
                        this.SetCodeOrder(summaryListItem);
                        this.m_CptCodeList.Add(summaryListItem);
                    }
                }
                
                foreach (Icd9BillingCodeListItem icd9Item in billingSpecimenListItem.Icd9BillingCodeList)
                {
                    bool isFound = false;
                    foreach (BillingCodeSummaryListItem summaryItem in this.Icd9CodeList)
                    {
                        if (icd9Item.Icd9Code == summaryItem.Code)
                        {
                            isFound = true;
                            summaryItem.Quantity = summaryItem.Quantity + icd9Item.Quantity;
                            break;
                        }
                    }
                    if (isFound == false)
                    {
                        BillingCodeSummaryListItem summaryListItem = new BillingCodeSummaryListItem();
                        summaryListItem.Quantity = 1;
                        summaryListItem.Code = icd9Item.Icd9Code;
                        summaryListItem.CodeOrder = 1;
                        this.m_Icd9CodeList.Add(summaryListItem);
                    }
                }                
            }
            this.m_CptCodeList.Sort();            
        }

        private void SetCodeOrder(BillingCodeSummaryListItem summaryListItem)
        {            
            int codeOrder = 1000;
            foreach (YellowstonePathology.Business.Billing.CptCodeListItem item in this.m_CptCodeSortList)
            {                
                if (item.CptCode.Trim() == summaryListItem.Code.Trim())             
                {                    
                    codeOrder = item.BillingReportOrder;
                    break;
                }                
            }
            summaryListItem.CodeOrder = codeOrder;
        }

		public XElement ToXml()
		{
			XElement result = new XElement("BillingCodeSummaryList");
			XElement cptElement = new XElement("CptCodeList");
			foreach (BillingCodeSummaryListItem item in this.m_CptCodeList)
			{
				cptElement.Add(item.ToXml());
			}
			result.Add(cptElement);

			XElement icd9Element = new XElement("Icd9CodeList");
			foreach (BillingCodeSummaryListItem item in this.m_Icd9CodeList)
			{
				icd9Element.Add(item.ToXml());
			}
			result.Add(icd9Element);

			return result;
		}
    }    
}
