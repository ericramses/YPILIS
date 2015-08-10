using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Domain
{
	[CollectionDataContract]
	public class PanelSetOrderCollection : ObservableCollection<PanelSetOrder>
	{
		public PanelSetOrderCollection()
        {

        }
	}
}
