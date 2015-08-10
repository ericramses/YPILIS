using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Contract.Flow
{
	public partial class FlowLeukemia : YellowstonePathology.Business.Domain.Persistence.ITrackable, INotifyPropertyChanged,
		YellowstonePathology.Business.Domain.Persistence.INotifyDBPropertyChanged, YellowstonePathology.Business.Domain.Persistence.IPropertyWritable,
		YellowstonePathology.Business.Domain.Persistence.IPropertyReadable, YellowstonePathology.Business.Domain.Persistence.IPersistable
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public event YellowstonePathology.Business.Domain.Persistence.DBPropertyChangedEventHandler DBPropertyChanged;

		private YellowstonePathology.Business.Domain.Persistence.TrackingStateEnum m_TrackingState;

		public FlowLeukemia()
		{
		}

		public FlowLeukemia(Domain.PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma)
		{
			this.ReportNo = panelSetOrderLeukemiaLymphoma.ReportNo;
			this.GatingPopulationV2 = panelSetOrderLeukemiaLymphoma.GatingPopulationV2;
			this.LymphocyteCount = panelSetOrderLeukemiaLymphoma.LymphocyteCount;
			this.MonocyteCount = panelSetOrderLeukemiaLymphoma.MonocyteCount;
			this.MyeloidCount = panelSetOrderLeukemiaLymphoma.MyeloidCount;
			this.DimCD45ModSSCount = panelSetOrderLeukemiaLymphoma.DimCD45ModSSCount;
			this.OtherCount = panelSetOrderLeukemiaLymphoma.OtherCount;
			this.OtherName = panelSetOrderLeukemiaLymphoma.OtherName;
			this.InterpretiveComment = panelSetOrderLeukemiaLymphoma.InterpretiveComment;
			this.Impression = panelSetOrderLeukemiaLymphoma.Impression;
			this.CellPopulationOfInterest = panelSetOrderLeukemiaLymphoma.CellPopulationOfInterest;
			this.TestResult = panelSetOrderLeukemiaLymphoma.TestResult;
			this.CellDescription = panelSetOrderLeukemiaLymphoma.CellDescription;
			this.BTCellSelection = panelSetOrderLeukemiaLymphoma.BTCellSelection;
			this.EGateCD34Percent = panelSetOrderLeukemiaLymphoma.EGateCD34Percent;
			this.EGateCD117Percent = panelSetOrderLeukemiaLymphoma.EGateCD117Percent;
			this.TrackingState = panelSetOrderLeukemiaLymphoma.TrackingState;
		}

		[DataMember]
		public YellowstonePathology.Business.Domain.Persistence.TrackingStateEnum TrackingState
		{
			get { return this.m_TrackingState; }
			set { this.m_TrackingState = value; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public void NotifyDBPropertyChanged(String info)
		{
			if (DBPropertyChanged != null)
			{
				DBPropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
