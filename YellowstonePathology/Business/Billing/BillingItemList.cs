using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;

namespace YellowstonePathology.Business.Billing
{
	public class BillingItemList : ObservableCollection<BillingItem>
    {
		public BillingItemList()
        {
        }
    }
}
