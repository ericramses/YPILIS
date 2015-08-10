using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.YpiConnect.Contract.Domain
{
	[DataContract]
	public class SpecimenOrder : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private string m_SpecimenOrderId;
		private int m_SpecimenTypeId;
		private Nullable<DateTime> m_CollectionDate;
		private Nullable<DateTime> m_CollectionTime;
		private string m_SpecimenSite;
		private string m_FixationType;
		private Nullable<DateTime> m_FixationStartTime;
		private Nullable<DateTime> m_FixationEndTime;
		private string m_Description;
		private int m_AliquotRequestCount;
		private Nullable<DateTime> m_AccessionTime;
		private string m_ClientFixation;
		private string m_LabFixation;
		private bool m_CollectionTimeUnknown;
		private bool m_ExactFixationStartTimeUnknown;
		private string m_MasterAccessionNo;
		private string m_FixationComment;
		private int m_SpecimenNumber;
		private bool m_Accepted;
		private string m_SpecialInstructions;
		private string m_ContainerId;
		private bool m_Verified;
		private int m_VerifiedById;
		private Nullable<DateTime> m_VerifiedDate;
		private bool m_RequiresBlindVerification;
		private string m_SystemInitiatingOrder;
		private string m_SpecimenSource;
		private Nullable<DateTime> m_DateReceived;
		private string m_SpecimenAdequacy;

		public SpecimenOrder()
		{
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		[PersistentProperty()]
		public string SpecimenOrderId
		{
			get { return this.m_SpecimenOrderId; }
			set
			{
				if (this.m_SpecimenOrderId != value)
				{
					this.m_SpecimenOrderId = value;
					this.NotifyPropertyChanged("SpecimenOrderId");
				}
			}
		}

		[PersistentProperty()]
		public int SpecimenTypeId
		{
			get { return this.m_SpecimenTypeId; }
			set
			{
				if (this.m_SpecimenTypeId != value)
				{
					this.m_SpecimenTypeId = value;
					this.NotifyPropertyChanged("SpecimenTypeId");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> CollectionDate
		{
			get { return this.m_CollectionDate; }
			set
			{
				if (this.m_CollectionDate != value)
				{
					this.m_CollectionDate = value;
					this.NotifyPropertyChanged("CollectionDate");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> CollectionTime
		{
			get { return this.m_CollectionTime; }
			set
			{
				if (this.m_CollectionTime != value)
				{
					this.m_CollectionTime = value;
					this.NotifyPropertyChanged("CollectionTime");
				}
			}
		}

		[PersistentProperty()]
		public string SpecimenSite
		{
			get { return this.m_SpecimenSite; }
			set
			{
				if (this.m_SpecimenSite != value)
				{
					this.m_SpecimenSite = value;
					this.NotifyPropertyChanged("SpecimenSite");
				}
			}
		}

		[PersistentProperty()]
		public string FixationType
		{
			get { return this.m_FixationType; }
			set
			{
				if (this.m_FixationType != value)
				{
					this.m_FixationType = value;
					this.NotifyPropertyChanged("FixationType");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> FixationStartTime
		{
			get { return this.m_FixationStartTime; }
			set
			{
				if (this.m_FixationStartTime != value)
				{
					this.m_FixationStartTime = value;
					this.NotifyPropertyChanged("FixationStartTime");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> FixationEndTime
		{
			get { return this.m_FixationEndTime; }
			set
			{
				if (this.m_FixationEndTime != value)
				{
					this.m_FixationEndTime = value;
					this.NotifyPropertyChanged("FixationEndTime");
				}
			}
		}

		[PersistentProperty()]
		public string Description
		{
			get { return this.m_Description; }
			set
			{
				if (this.m_Description != value)
				{
					this.m_Description = value;
					this.NotifyPropertyChanged("Description");
				}
			}
		}

		[PersistentProperty()]
		public int AliquotRequestCount
		{
			get { return this.m_AliquotRequestCount; }
			set
			{
				if (this.m_AliquotRequestCount != value)
				{
					this.m_AliquotRequestCount = value;
					this.NotifyPropertyChanged("AliquotRequestCount");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> AccessionTime
		{
			get { return this.m_AccessionTime; }
			set
			{
				if (this.m_AccessionTime != value)
				{
					this.m_AccessionTime = value;
					this.NotifyPropertyChanged("AccessionTime");
				}
			}
		}

		[PersistentProperty()]
		public string ClientFixation
		{
			get { return this.m_ClientFixation; }
			set
			{
				if (this.m_ClientFixation != value)
				{
					this.m_ClientFixation = value;
					this.NotifyPropertyChanged("ClientFixation");
				}
			}
		}

		[PersistentProperty()]
		public string LabFixation
		{
			get { return this.m_LabFixation; }
			set
			{
				if (this.m_LabFixation != value)
				{
					this.m_LabFixation = value;
					this.NotifyPropertyChanged("LabFixation");
				}
			}
		}

		[PersistentProperty()]
		public bool CollectionTimeUnknown
		{
			get { return this.m_CollectionTimeUnknown; }
			set
			{
				if (this.m_CollectionTimeUnknown != value)
				{
					this.m_CollectionTimeUnknown = value;
					this.NotifyPropertyChanged("CollectionTimeUnknown");
				}
			}
		}

		[PersistentProperty()]
		public bool ExactFixationStartTimeUnknown
		{
			get { return this.m_ExactFixationStartTimeUnknown; }
			set
			{
				if (this.m_ExactFixationStartTimeUnknown != value)
				{
					this.m_ExactFixationStartTimeUnknown = value;
					this.NotifyPropertyChanged("ExactFixationStartTimeUnknown");
				}
			}
		}

		[PersistentProperty()]
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

		[PersistentProperty()]
		public string FixationComment
		{
			get { return this.m_FixationComment; }
			set
			{
				if (this.m_FixationComment != value)
				{
					this.m_FixationComment = value;
					this.NotifyPropertyChanged("FixationComment");
				}
			}
		}

		[PersistentProperty()]
		public int SpecimenNumber
		{
			get { return this.m_SpecimenNumber; }
			set
			{
				if (this.m_SpecimenNumber != value)
				{
					this.m_SpecimenNumber = value;
					this.NotifyPropertyChanged("SpecimenNumber");
				}
			}
		}

		[PersistentProperty()]
		public bool Accepted
		{
			get { return this.m_Accepted; }
			set
			{
				if (this.m_Accepted != value)
				{
					this.m_Accepted = value;
					this.NotifyPropertyChanged("Accepted");
				}
			}
		}

		[PersistentProperty()]
		public string SpecialInstructions
		{
			get { return this.m_SpecialInstructions; }
			set
			{
				if (this.m_SpecialInstructions != value)
				{
					this.m_SpecialInstructions = value;
					this.NotifyPropertyChanged("SpecialInstructions");
				}
			}
		}

		[PersistentProperty()]
		public string ContainerId
		{
			get { return this.m_ContainerId; }
			set
			{
				if (this.m_ContainerId != value)
				{
					this.m_ContainerId = value;
					this.NotifyPropertyChanged("ContainerId");
				}
			}
		}

		[PersistentProperty()]
		public bool Verified
		{
			get { return this.m_Verified; }
			set
			{
				if (this.m_Verified != value)
				{
					this.m_Verified = value;
					this.NotifyPropertyChanged("Verified");
				}
			}
		}

		[PersistentProperty()]
		public int VerifiedById
		{
			get { return this.m_VerifiedById; }
			set
			{
				if (this.m_VerifiedById != value)
				{
					this.m_VerifiedById = value;
					this.NotifyPropertyChanged("VerifiedById");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> VerifiedDate
		{
			get { return this.m_VerifiedDate; }
			set
			{
				if (this.m_VerifiedDate != value)
				{
					this.m_VerifiedDate = value;
					this.NotifyPropertyChanged("VerifiedDate");
				}
			}
		}

		[PersistentProperty()]
		public bool RequiresBlindVerification
		{
			get { return this.m_RequiresBlindVerification; }
			set
			{
				if (this.m_RequiresBlindVerification != value)
				{
					this.m_RequiresBlindVerification = value;
					this.NotifyPropertyChanged("RequiresBlindVerification");
				}
			}
		}

		[PersistentProperty()]
		public string SystemInitiatingOrder
		{
			get { return this.m_SystemInitiatingOrder; }
			set
			{
				if (this.m_SystemInitiatingOrder != value)
				{
					this.m_SystemInitiatingOrder = value;
					this.NotifyPropertyChanged("SystemInitiatingOrder");
				}
			}
		}

		[PersistentProperty()]
		public string SpecimenSource
		{
			get { return this.m_SpecimenSource; }
			set
			{
				if (this.m_SpecimenSource != value)
				{
					this.m_SpecimenSource = value;
					this.NotifyPropertyChanged("SpecimenSource");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> DateReceived
		{
			get { return this.m_DateReceived; }
			set
			{
				if (this.m_DateReceived != value)
				{
					this.m_DateReceived = value;
					this.NotifyPropertyChanged("DateReceived");
				}
			}
		}

		[PersistentProperty()]
		public string SpecimenAdequacy
		{
			get { return this.m_SpecimenAdequacy; }
			set
			{
				if (this.m_SpecimenAdequacy != value)
				{
					this.m_SpecimenAdequacy = value;
					this.NotifyPropertyChanged("SpecimenAdequacy");
				}
			}
		}
	}
}
