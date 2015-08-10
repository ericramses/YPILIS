using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Billing
{
	public class BillingSpecimenList : ObservableCollection<BillingSpecimenListItem>
	{

		public BillingSpecimenList()
		{
		}

		public XElement ToXml()
		{
			XElement result = new XElement("BillingSpecimenList");
			foreach (BillingSpecimenListItem item in this)
			{
				result.Add(item.ToXml());
			}

			return result;
		}
	}
}