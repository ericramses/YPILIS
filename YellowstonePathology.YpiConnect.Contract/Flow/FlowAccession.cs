using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace YellowstonePathology.YpiConnect.Contract.Flow
{
	[DataContract]
	public partial class FlowAccession : YellowstonePathology.Business.Domain.Persistence.ITrackable, INotifyPropertyChanged,
		YellowstonePathology.Business.Domain.Persistence.INotifyDBPropertyChanged, YellowstonePathology.Business.Domain.Persistence.IPropertyWritable,
		YellowstonePathology.Business.Domain.Persistence.IPropertyReadable, YellowstonePathology.Business.Domain.Persistence.IPersistable, IAccessionGeneralData, IPatientData
	{
		protected delegate void PropertyChangedNotificationHandler(String info);
		protected delegate void DBPropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;
		public event YellowstonePathology.Business.Domain.Persistence.DBPropertyChangedEventHandler DBPropertyChanged;

		private YellowstonePathology.Business.Domain.Persistence.TrackingStateEnum m_TrackingState;

        private RemoteFileList m_CaseDocumentList;
		private YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrderCollection m_PanelSetOrderCollection;
		private YellowstonePathology.YpiConnect.Contract.Domain.SpecimenOrderCollection m_SpecimenOrderCollection;

		public FlowAccession()
		{
			this.m_PanelSetOrderCollection = new Domain.PanelSetOrderCollection();
		}

		[DataMember]
		public Domain.PanelSetOrderCollection PanelSetOrderCollection
		{
			get { return this.m_PanelSetOrderCollection; }
			set { this.m_PanelSetOrderCollection = value; }
		}

		[DataMember]
		public Domain.SpecimenOrderCollection SpecimenOrderCollection
		{
			get { return this.m_SpecimenOrderCollection; }
			set { this.m_SpecimenOrderCollection = value; }
		}

		[DataMember]
		public RemoteFileList CaseDocumentList
		{
			get { return this.m_CaseDocumentList; }
			set { this.m_CaseDocumentList = value; }
		}

        public string PatientDisplayName
        {
            get 
            {
				return YellowstonePathology.Shared.Helper.PatientHelper.GetPatientDisplayName(this.m_PLastName, this.m_PFirstName, this.m_PMiddleInitial); 
            }
        }

		public void SetOriginalValues()
		{
			foreach (YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrder panelSetOrder in this.m_PanelSetOrderCollection)
			{
				((YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrderLeukemiaLymphoma)panelSetOrder).SetOriginalValues();
			}
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
