using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Flow
{
	[CollectionDataContract]
	public class FlowLeukemiaCollection : YellowstonePathology.Business.Domain.Persistence.CollectionTrackingBase<FlowLeukemia>
	{
		public FlowLeukemiaCollection()
		{
		}
	}
}
