using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Billing
{
	[CollectionDataContract]
	public class PanelSetOrderCPTCodeBillCollection : ObservableCollection<PanelSetOrderCPTCodeBill>
    {
        public PanelSetOrderCPTCodeBillCollection()
        {

        }

		public PanelSetOrderCPTCodeBillCollection GetNonClientCollection(string billto)
		{
			PanelSetOrderCPTCodeBillCollection result = new PanelSetOrderCPTCodeBillCollection();
			foreach (PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this)
			{
				if(panelSetOrderCPTCodeBill.BillBy != "CLNT" && panelSetOrderCPTCodeBill.BillTo == billto) result.Add(panelSetOrderCPTCodeBill);
			}
			return result;
		}

		public PanelSetOrderCPTCodeBillCollection GetMedicareCollection()
		{
			PanelSetOrderCPTCodeBillCollection result = new PanelSetOrderCPTCodeBillCollection();
			foreach (PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in this)
			{
				if (panelSetOrderCPTCodeBill.BillBy == "CLNT") result.Add(panelSetOrderCPTCodeBill);
			}
			return result;
		}
    }
}
