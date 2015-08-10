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
	public class FlowMarkerCollection : YellowstonePathology.Business.Domain.Persistence.CollectionTrackingBase<FlowMarker>
	{
		private const string PREFIXID = "FM";

		public FlowMarkerCollection()
		{
            
		}

		public FlowMarker GetNextItem(string reportNo, Marker marker)
		{
			FlowMarker flowMarker = new FlowMarker(marker, reportNo);
			flowMarker.FlowMarkerId = this.GetNextId(reportNo);
			return flowMarker;
		}

		public string GetNextId(string reportNo)
		{
			int largestId = 0;
			foreach (FlowMarker flowMarker in this)
			{
				int thisId = this.ParseId(flowMarker.FlowMarkerId, PREFIXID);
				if (thisId > largestId)
				{
					largestId = thisId;
				}
			}
			return reportNo + "." + PREFIXID + (largestId + 1).ToString();
		}

		public int ParseId(string id, string prefix)
		{
			int startIndex = id.IndexOf(prefix) + prefix.Length;
			int result = Convert.ToInt32(id.Substring(startIndex));
			return result;
		}

		public List<YellowstonePathology.Shared.Interface.IFlowMarker> ToList()
		{
			List<YellowstonePathology.Shared.Interface.IFlowMarker> list = new List<Shared.Interface.IFlowMarker>();
			foreach (FlowMarker flowMarker in this)
			{
				list.Add(flowMarker);
			}
			return list;
		}

		public void Add(Marker marker, string reportNo)
		{
			//FlowMarker flowMarker = new FlowMarker();
			//flowMarker.ReportNo = reportNo;
			//flowMarker.MarkerId = marker.MarkerId;
			//flowMarker.Name = marker.MarkerName;
			//flowMarker.Predictive = marker.Predictive;
			FlowMarker flowMarker = this.GetNextItem(reportNo, marker);
			this.Add(flowMarker);
		}
	}
}
