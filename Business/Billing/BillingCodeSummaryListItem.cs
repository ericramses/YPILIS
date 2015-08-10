using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Billing
{
    public class BillingCodeSummaryListItem : IComparable<BillingCodeSummaryListItem>
    {
        private int m_Quantity;
        private string m_Code;
        private int m_CodeOrder;

        public BillingCodeSummaryListItem()
        {

        }

        public int CompareTo(BillingCodeSummaryListItem listItem)
        {
            return this.m_CodeOrder.CompareTo(listItem.m_CodeOrder);
        }

        public int Quantity
        {
            get { return this.m_Quantity; }
            set { this.m_Quantity = value; }
        }

        public string Code
        {
            get { return this.m_Code; }
            set { this.m_Code = value; }
        }

        public int CodeOrder
        {
            get { return this.m_CodeOrder; }
            set { this.m_CodeOrder = value; }
        }

		public XElement ToXml()
		{
			XElement result = new XElement("BillingCodeSummaryListItem",
				new XElement("Quantity", this.m_Quantity.ToString()),
				new XElement("Code", this.m_Code),
				new XElement("CodeOrder", this.m_CodeOrder.ToString()));
			return result;
		}
    }
}
