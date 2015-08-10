using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Domain
{
	[XmlType("LockItemCollection")]
	public class LockItemCollection : ObservableCollection<LockItem>
	{
		public LockItemCollection()
		{
		}
	}
}
