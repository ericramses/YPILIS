using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
	public class SurgicalOrderTypeCollection : ObservableCollection<SurgicalOrderType>
	{
		public SurgicalOrderTypeCollection()
		{
			SurgicalOrderType surgicalOrderTypeYes = new SurgicalOrderType("Yes", true);
			SurgicalOrderType surgicalOrderTypeNo = new SurgicalOrderType("No", false);
			this.Add(surgicalOrderTypeYes);
			this.Add(surgicalOrderTypeNo);
		}
	}
}
