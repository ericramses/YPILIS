using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.MaterialTracking.Model
{
	[PersistentClass("tblMaterialTrackingLog", "YPIDATA")]
    public class MaterialTrackingLog : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_MaterialTrackingLogId;
		private string m_MaterialTrackingBatchId;
		private string m_MaterialId;
        private string m_MaterialType;
        private string m_MaterialLabel;
        private string m_MasterAccessionNo;
        private string m_FacilityId;
        private string m_FacilityName;
		private string m_LocationId;
		private string m_LocationName;
		private DateTime m_LogDate;
		private int m_LoggedById;
		private string m_LoggedBy;
		private string m_Description;
        private string m_Action;
        private bool m_ClientAccessioned;

        public MaterialTrackingLog()
        {
            this.m_LogDate = DateTime.Now;
        }

        public MaterialTrackingLog(string objectId, string materialId, string materialTrackingBatchId, string facilityId, string facilityName, 
            string locationId, string locationName, string action, string description, string materialType, string masterAccessionNo, string materialLabel, bool clientAccessioned)
        {
            this.m_MaterialTrackingLogId = Guid.NewGuid().ToString();
			this.m_ObjectId = objectId;
            this.m_MaterialId = materialId;
            this.m_MaterialTrackingBatchId = materialTrackingBatchId;
            this.m_FacilityId = facilityId;
            this.m_FacilityName = facilityName;
            this.m_LocationId = locationId;
            this.m_LocationName = locationName;
            this.m_LogDate = DateTime.Now;
            this.m_LoggedById = Business.User.SystemIdentity.Instance.User.UserId;
            this.m_LoggedBy = Business.User.SystemIdentity.Instance.User.UserName;
            this.m_Description = description;
            this.m_Action = action;
            this.m_MaterialType = materialType;
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_MaterialLabel = materialLabel;
            this.m_ClientAccessioned = clientAccessioned;
        }

        public MaterialTrackingLog(string objectId, string materialId, string materialTrackingBatchId, string facilityId, string facilityName, string locationId, string locationName, string materialType)
        {
            this.m_MaterialTrackingLogId = Guid.NewGuid().ToString();
			this.m_ObjectId = objectId;
            this.m_MaterialId = materialId;
            this.m_MaterialTrackingBatchId = materialTrackingBatchId;
            this.m_FacilityId = facilityId;
            this.m_FacilityName = facilityName;
            this.m_LocationId = locationId;
            this.m_LocationName = locationName;
            this.m_LogDate = DateTime.Now;
            this.m_LoggedById = Business.User.SystemIdentity.Instance.User.UserId;
            this.m_LoggedBy = Business.User.SystemIdentity.Instance.User.UserName;
            this.m_MaterialType = materialType;            
        }

		public void Update(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingScannedItemView materialTrackingScannedItemView)
        {                        
            this.m_MasterAccessionNo = materialTrackingScannedItemView.MasterAccessionNo;
            this.m_MaterialLabel = materialTrackingScannedItemView.MaterialLabel;
        }

		[PersistentDocumentIdProperty(50)]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set
			{
				if (this.m_ObjectId != value)
				{
					this.m_ObjectId = value;
					this.NotifyPropertyChanged("ObjectId");
				}
			}
		}

        [PersistentPrimaryKeyProperty(false, 50)]
		public string MaterialTrackingLogId
		{
			get { return this.m_MaterialTrackingLogId; }
			set
			{
				if(this.m_MaterialTrackingLogId != value)
				{
					this.m_MaterialTrackingLogId = value;
					this.NotifyPropertyChanged("MaterialTrackingLogId");					
				}
			}
		}

        [PersistentStringProperty(50)]
		public string MaterialTrackingBatchId
		{
			get { return this.m_MaterialTrackingBatchId; }
			set
			{
				if(this.m_MaterialTrackingBatchId != value)
				{
					this.m_MaterialTrackingBatchId = value;
					this.NotifyPropertyChanged("MaterialTrackingBatchId");					
				}
			}
		}

        [PersistentStringProperty(50)]
		public string MaterialId
		{
			get { return this.m_MaterialId; }
			set
			{
				if(this.m_MaterialId != value)
				{
					this.m_MaterialId = value;
					this.NotifyPropertyChanged("MaterialId");					
				}
			}
		}

        [PersistentStringProperty(50)]
        public string MaterialType
        {
            get { return this.m_MaterialType; }
            set
            {
                if (this.m_MaterialType != value)
                {
                    this.m_MaterialType = value;
                    this.NotifyPropertyChanged("MaterialType");
                }
            }
        }

        [PersistentStringProperty(500)]
        public string MaterialLabel
        {
            get { return this.m_MaterialLabel; }
            set
            {
                if (this.m_MaterialLabel != value)
                {
                    this.m_MaterialLabel = value;
                    this.NotifyPropertyChanged("MaterialLabel");
                }
            }
        }

        [PersistentStringProperty(50)]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
                }
            }
        }

        [PersistentStringProperty(50)]
		public string LocationId
		{
			get { return this.m_LocationId; }
			set
			{
				if(this.m_LocationId != value)
				{
					this.m_LocationId = value;
					this.NotifyPropertyChanged("LocationId");					
				}
			}
		}

        [PersistentStringProperty(500)]
		public string LocationName
		{
			get { return this.m_LocationName; }
			set
			{
				if(this.m_LocationName != value)
				{
					this.m_LocationName = value;
					this.NotifyPropertyChanged("LocationName");					
				}
			}
		}

        [PersistentStringProperty(50)]
        public string FacilityId
        {
            get { return this.m_FacilityId; }
            set
            {
                if (this.m_FacilityId != value)
                {
                    this.m_FacilityId = value;
                    this.NotifyPropertyChanged("FacilityId");
                }
            }
        }

        [PersistentStringProperty(500)]
        public string FacilityName
        {
            get { return this.m_FacilityName; }
            set
            {
                if (this.m_FacilityName != value)
                {
                    this.m_FacilityName = value;
                    this.NotifyPropertyChanged("FacilityName");
                }
            }
        }

        [PersistentProperty()]
		public DateTime LogDate
		{
			get { return this.m_LogDate; }
			set
			{
				if(this.m_LogDate != value)
				{
					this.m_LogDate = value;
					this.NotifyPropertyChanged("LogDate");					
				}
			}
		}

        [PersistentProperty()]
		public int LoggedById
		{
			get { return this.m_LoggedById; }
			set
			{
				if(this.m_LoggedById != value)
				{
					this.m_LoggedById = value;
					this.NotifyPropertyChanged("LoggedById");					
				}
			}
		}

        [PersistentStringProperty(200)]
		public string LoggedBy
		{
			get { return this.m_LoggedBy; }
			set
			{
				if(this.m_LoggedBy != value)
				{
					this.m_LoggedBy = value;
					this.NotifyPropertyChanged("LoggedBy");					
				}
			}
		}

        [PersistentStringProperty(100)]
		public string Description
		{
			get { return this.m_Description; }
			set
			{
				if(this.m_Description != value)
				{
					this.m_Description = value;
					this.NotifyPropertyChanged("Description");
				}
			}
		}

        [PersistentStringProperty(500)]
        public string Action
        {
            get { return this.m_Action; }
            set
            {
                if (this.m_Action != value)
                {
                    this.m_Action = value;
                    this.NotifyPropertyChanged("Action");
                }
            }
        }

        [PersistentProperty("0")]
        public bool ClientAccessioned
        {
            get { return this.m_ClientAccessioned; }
            set
            {
                if (this.m_ClientAccessioned != value)
                {
                    this.m_ClientAccessioned = value;
                    this.NotifyPropertyChanged("ClientAccessioned");
                }
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
	}
}
