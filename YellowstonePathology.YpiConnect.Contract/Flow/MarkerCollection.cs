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
	public class MarkerCollection : YellowstonePathology.Business.Domain.Persistence.CollectionTrackingBase<Marker>
	{
		public MarkerCollection()
		{
		}
	}
}
