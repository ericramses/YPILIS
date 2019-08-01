using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Specimen.Model
{
	[PersistentClass("tblSpecimenOrder", "YPIDATA")]
	public partial class SpecimenOrder : INotifyPropertyChanged, YellowstonePathology.Business.Interface.IOrderTarget
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_SpecimenOrderId;
        private string m_SpecimenId;
        private string m_SpecimenType;
        private Nullable<DateTime> m_CollectionDate;
        private Nullable<DateTime> m_CollectionTime;
        private string m_SpecimenSite;
        private string m_FixationType;        
        
        private string m_Description;
        private int m_AliquotRequestCount;
        private Nullable<DateTime> m_AccessionTime;
        private string m_ClientFixation;
        private string m_LabFixation;
        private bool m_CollectionTimeUnknown;        
        private string m_MasterAccessionNo;
        
        private int m_SpecimenNumber;        
        private Nullable<int> m_ClientSpecimenNumber;
        private bool m_ClientAccessioned;
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
        private string m_LocationFacilityId;
        private string m_OwnerFacilityId;
        private bool m_RequiresGrossExamination;
        private string m_Location;
        private string m_FacilityId;
                
        private Nullable<DateTime> m_ProcessorStartTime;
        private Nullable<int> m_ProcessorFixationTime;        
        private bool m_FixationStartTimeManuallyEntered;
        private Nullable<int> m_TimeToFixation;
        private Nullable<DateTime> m_FixationStartTime;
        private Nullable<DateTime> m_FixationEndTime;
        private Nullable<int> m_FixationDuration;
        private string m_FixationComment;
        private string m_TimeToFixationHourString;

		private YellowstonePathology.Business.Test.AliquotOrderCollection m_AliquotOrderCollection;		
		private List<YellowstonePathology.Business.Test.Model.TestOrder> m_PeerReviewTestOrders;		
		private bool m_DeletePending = false;
		private bool m_ApplyFixation = true;                

		public SpecimenOrder()
		{
			this.m_AliquotOrderCollection = new YellowstonePathology.Business.Test.AliquotOrderCollection();			
			this.m_PeerReviewTestOrders = new List<YellowstonePathology.Business.Test.Model.TestOrder>();			
		}

		public SpecimenOrder(string specimenOrderId, string objectId, string masterAccessionNo)
		{
			this.m_SpecimenOrderId = specimenOrderId;
			this.m_ObjectId = objectId;
			this.m_MasterAccessionNo = masterAccessionNo;
			this.AccessionTime = DateTime.Now;
			this.Accepted = true;

			this.m_AliquotOrderCollection = new YellowstonePathology.Business.Test.AliquotOrderCollection();			
			this.m_PeerReviewTestOrders = new List<YellowstonePathology.Business.Test.Model.TestOrder>();
		}        

		public void FromClientOrderDetail(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
		{
			this.m_AliquotRequestCount = 1;

            this.m_ClientAccessioned = clientOrderDetail.ClientAccessioned;
			this.m_ContainerId = clientOrderDetail.ContainerId;
			this.m_Description = clientOrderDetail.DescriptionToAccession;
			this.m_SpecialInstructions = clientOrderDetail.SpecialInstructions;

			YellowstonePathology.Business.Helper.DateTimeSplitter dateTimeSplitter = new YellowstonePathology.Business.Helper.DateTimeSplitter(clientOrderDetail.CollectionDate);
            this.m_CollectionDate = dateTimeSplitter.GetDate();
            this.m_CollectionTime = dateTimeSplitter.GetDateWithTime();

            this.m_FixationStartTimeManuallyEntered = clientOrderDetail.FixationStartTimeManuallyEntered;
            this.m_DateReceived = clientOrderDetail.DateReceived;            
			this.m_ClientFixation = clientOrderDetail.ClientFixation;
            this.m_FixationStartTime = clientOrderDetail.FixationStartTime;
            this.m_TimeToFixation = clientOrderDetail.TimeToFixation;
            this.m_TimeToFixationHourString = clientOrderDetail.TimeToFixationHourString;
            this.m_FixationComment = clientOrderDetail.FixationComment;            
			this.m_LabFixation = clientOrderDetail.LabFixation;
            this.m_SpecimenSource = clientOrderDetail.SpecimenSource;
            this.m_RequiresGrossExamination = clientOrderDetail.RequiresGrossExamination;            

            this.SetTimeToFixation();
		}

		public void SetAliquotRequestCount()
		{
			this.AliquotRequestCount = this.AliquotOrderCollection.Count;
		}

		public void AcceptResults()
		{

		}

		public void AccessionNGCTFromCytology()
		{

		}

		public string GetCollectionDateTimeString()
		{
			string result = string.Empty;
			if (this.m_CollectionDate.HasValue == true)
			{
				if (this.m_CollectionTime.HasValue == true)
				{
					result = this.m_CollectionDate.Value.ToShortDateString() + " " + this.m_CollectionTime.Value.ToString("HH:mm");
				}
				else
				{
					result = this.m_CollectionDate.Value.ToShortDateString() + " : Time not given";
				}
			}
			return result;
		}		

        public string GetGrossSubmittedInString()
        {
            string result = null;
            if (this.m_AliquotOrderCollection.Count != 0)
            {
                result = "entirely submitted into ";
                if (this.m_AliquotOrderCollection.Count == 1)
                {
                	result += "cassette \"" + this.m_AliquotOrderCollection[0].Label + "\"";
                }
                else
                {
                    result += "cassettes \"" + this.m_AliquotOrderCollection[0].Label + "\" - \"" + this.m_AliquotOrderCollection[this.m_AliquotOrderCollection.Count - 1].Label + "\"";
                }                                              
            }
            return result;
        }

        public string GetSummarySubmissionString()
        { 
            StringBuilder result = new StringBuilder("CASSETTE SUMMARY: ");
            result.AppendLine();
            for(int i=0; i<this.m_AliquotOrderCollection.Count; i++)
            {
                string text = "\"" + this.m_AliquotOrderCollection[i].Label + "\" - [???]";
                if(i < this.m_AliquotOrderCollection.Count - 1)
                {
                    result.AppendLine(text);
                }
                else
                {
                    result.Append(text);                    
                }                
            }                        
            return result.ToString();
        }

        public string GetRepresentativeSectionsSubmittedIn()
        {
            string result = null;
            if (this.m_AliquotOrderCollection.Count != 0)
            {
                result = "Representative sections are submitted to ";               
                if (this.m_AliquotOrderCollection.Count == 1)
                {
                    result += "cassette \"" + this.m_AliquotOrderCollection[0].Label + "\"";
                }
                else
                {
                    result += "cassettes \"" + this.m_AliquotOrderCollection[0].Label + "\" - \"" + this.m_AliquotOrderCollection[this.m_AliquotOrderCollection.Count - 1].Label + "\"";
                }               
            }
            return result;
        }

        public string GetGrossRemainderSubmittedInString()
        {
            string result = null;
            if (this.m_AliquotOrderCollection.Count != 0)
            {
                result = "entirely submitted into ";
                if (this.m_AliquotOrderCollection.Count == 2)
                {
                    result += "cassette \"" + this.m_AliquotOrderCollection[1].Label + "\"";
                }
                else if (this.m_AliquotOrderCollection.Count > 2)
                {
                    result += "cassettes \"" + this.m_AliquotOrderCollection[1].Label + "\" - \"" + this.m_AliquotOrderCollection[this.m_AliquotOrderCollection.Count - 1].Label + "\"";
                }                
            }
            return result;
        }

        public string GetGrossMiddleCassettesSubmittedInString()
        {
            string result = null;
            if (this.m_AliquotOrderCollection.Count == 3)
            {
                result += " \"" + this.m_AliquotOrderCollection[1].Label + "\" ";
            }
            else if (this.m_AliquotOrderCollection.Count > 3)
            {                                
                result += " \"" + this.m_AliquotOrderCollection[1].Label + "\" - \"" + this.m_AliquotOrderCollection[this.m_AliquotOrderCollection.Count - 2].Label + "\" ";                
            }
            return result;
        }

        public string GetSpecimenDescriptionString()
		{
			string result = this.SpecimenNumber.ToString() + ".) " + this.Description;
			return result;
		}

        public string SpecimenDescriptionString
        {
            get 
            {
                return this.SpecimenNumber.ToString() + ".) " + this.Description;
            }            
        }               				

		[PersistentCollection()]
		public YellowstonePathology.Business.Test.AliquotOrderCollection AliquotOrderCollection
		{
			get { return m_AliquotOrderCollection; }
            set { this.m_AliquotOrderCollection = value; }
		}		

		public bool DeletePending
		{
			get { return m_DeletePending; }
			set
			{
				m_DeletePending = value;
				NotifyPropertyChanged("DeletePending");
			}
		}        

		public bool ApplyFixation
		{
			get { return m_ApplyFixation; }
			set { m_ApplyFixation = value; }
		}        

		public string DescriptionWithCount
		{
			get
			{
				string retval = Description + "  ";
				string firstDisplay = string.Empty;
				string lastDisplay = string.Empty;
				int count = 0;
				foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in this.AliquotOrderCollection)
				{
					if (aliquotOrder.AliquotType == "Block" || aliquotOrder.AliquotType == "FrozenBlock" || aliquotOrder.AliquotType == "CellBlock")
					{
						count++;
						if (firstDisplay.Length == 0)
						{
							firstDisplay = aliquotOrder.Display;
						}
						if (count > 1)
						{
							lastDisplay = aliquotOrder.Display;
						}
					}
				}

				retval += firstDisplay;

				if (count > 1)
				{
					retval += " - " + lastDisplay;
				}
				retval += "  (" + count.ToString() + ")";
				return retval;
			}
		}		

        public string GetId()
        {
            return this.m_SpecimenOrderId;
        }

        public YellowstonePathology.Business.Interface.IOrderTargetType GetTargetType()
        {
            return YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen(this.m_SpecimenId);
        }

        public string GetOrderedOnType()
        {
            return YellowstonePathology.Business.OrderedOn.Specimen;
        }

        public string GetDescription()
        {
            return this.m_Description;
        }

		public bool ReceivedFresh
		{
			get { return this.m_ClientFixation == "Fresh"; }
		}

		public YellowstonePathology.Business.Test.Model.TestOrderCollection GetTestOrders(YellowstonePathology.Business.Test.Model.TestOrderCollection testForPanelSetCollection)
		{
			YellowstonePathology.Business.Test.Model.TestOrderCollection result = new YellowstonePathology.Business.Test.Model.TestOrderCollection();
			foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in testForPanelSetCollection)
			{				
                if (testOrder.AliquotOrder.SpecimenOrderId == this.SpecimenOrderId)
                {
                    result.Add(testOrder);
                }
			}

			return result;
		}

		[PersistentDocumentIdProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		
		[PersistentPrimaryKeyProperty(false)]
		[PersistentDataColumnProperty(false, "100", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string SpecimenId
        {
            get { return this.m_SpecimenId; }
            set
            {
                if (this.m_SpecimenId != value)
                {
                    this.m_SpecimenId = value;
                    this.NotifyPropertyChanged("SpecimenId");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string SpecimenType
		{
			get { return this.m_SpecimenType; }
			set
			{
				if (this.m_SpecimenType != value)
				{
					this.m_SpecimenType = value;
					this.NotifyPropertyChanged("SpecimenType");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
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
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public Nullable<int> FixationDuration
        {
            get { return this.m_FixationDuration; }
            set
            {
                if (this.m_FixationDuration != value)
                {
                    this.m_FixationDuration = value;
                    this.NotifyPropertyChanged("FixationDuration");
                }
            }
        }

        public void SetFixationDuration()
        {
            if (this.m_FixationEndTime.HasValue == true && this.m_FixationStartTime.HasValue == true)
            {
                TimeSpan timespan = new TimeSpan(this.m_FixationEndTime.Value.Ticks - this.m_FixationStartTime.Value.Ticks);
                this.m_FixationDuration = Convert.ToInt32(timespan.TotalHours);
            }
            else
            {
                this.m_FixationDuration = null;
            }
            this.NotifyPropertyChanged("FixationDuration");
        }

        public string GetExpectedFixationDuration()
        {
            string result = null;
            if(this.m_FixationStartTime.HasValue == true)
            {
                DateTime todaydayAt500 = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd") + "T17:00");
                Business.Surgical.ProcessorRun run = new Surgical.ProcessorRun("Tonight", todaydayAt500, new TimeSpan(2, 30, 0));
                DateTime expectedFixationEndTime = run.GetFixationEndTime(this.m_FixationStartTime.Value);
                TimeSpan expectedDurationTS = expectedFixationEndTime.Subtract(this.m_FixationStartTime.Value);
                result = "~" + Math.Round(expectedDurationTS.TotalHours, 0).ToString() + "hrs";             
            }
            else
            {
                result = "Unknown";
            }
            return result;
        }
        
        public string FixationDurationString
        {
            get 
            {
                string result = null;
                if (this.FixationDuration.HasValue == true)
                {
                    if (this.FixationDuration != 0)
                    {
                        result = "~" + this.FixationDuration + " hrs";
                    }
                    else
                    {
                        result = "Unknown";
                    }
                }
                else
                {
                    result = "Unknown";
                }
                return result;
            }            
        }          

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
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
		[PersistentDataColumnProperty(false, "11", "0", "int")]
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
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
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
		[PersistentDataColumnProperty(true, "25", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "25", "null", "varchar")]
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
		[PersistentDataColumnProperty(false, "1", "1", "tinyint")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
		[PersistentDataColumnProperty(false, "11", "0", "int")]
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
        [PersistentDataColumnProperty(false, "11", "0", "int")]
        public Nullable<int> ClientSpecimenNumber
        {
            get { return this.m_ClientSpecimenNumber; }
            set
            {
                if (this.m_ClientSpecimenNumber != value)
                {
                    this.m_ClientSpecimenNumber = value;
                    this.NotifyPropertyChanged("ClientSpecimenNumber");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
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

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
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
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
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
		[PersistentDataColumnProperty(false, "11", "0", "int")]
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
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
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
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string LocationFacilityId
        {
            get { return this.m_LocationFacilityId; }
            set
            {
                if (this.m_LocationFacilityId != value)
                {
                    this.m_LocationFacilityId = value;
                    this.NotifyPropertyChanged("LocationFacilityId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string OwnerFacilityId
        {
            get { return this.m_OwnerFacilityId; }
            set
            {
                if (this.m_OwnerFacilityId != value)
                {
                    this.m_OwnerFacilityId = value;
                    this.NotifyPropertyChanged("OwnerFacilityId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "1", "tinyint")]
        public bool RequiresGrossExamination
        {
            get { return this.m_RequiresGrossExamination; }
            set
            {
                if (this.m_RequiresGrossExamination != value)
                {
                    this.m_RequiresGrossExamination = value;
                    this.NotifyPropertyChanged("RequiresGrossExamination");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Location
        {
            get { return this.m_Location; }
            set
            {
                if (this.m_Location != value)
                {
                    this.m_Location = value;
                    this.NotifyPropertyChanged("Location");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> ProcessorStartTime
        {
            get { return this.m_ProcessorStartTime; }
            set
            {
                if (this.m_ProcessorStartTime != value)
                {
                    this.m_ProcessorStartTime = value;
                    this.NotifyPropertyChanged("ProcessorStartTime");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public Nullable<int> ProcessorFixationTime
        {
            get { return this.m_ProcessorFixationTime; }
            set
            {
                if (this.m_ProcessorFixationTime != value)
                {
                    this.m_ProcessorFixationTime = value;
                    this.NotifyPropertyChanged("ProcessorFixationTime");
                }
            }
        }        

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool FixationStartTimeManuallyEntered
        {
            get { return this.m_FixationStartTimeManuallyEntered; }
            set
            {
                if (this.m_FixationStartTimeManuallyEntered != value)
                {
                    this.m_FixationStartTimeManuallyEntered = value;
                    this.NotifyPropertyChanged("FixationStartTimeManuallyEntered");
                }
            }
        }        

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
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
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
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
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public Nullable<int> TimeToFixation
        {
            get { return this.m_TimeToFixation; }
            set
            {
                if (this.m_TimeToFixation != value)
                {
                    this.m_TimeToFixation = value;
                    this.NotifyPropertyChanged("TimeToFixation");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string TimeToFixationHourString
        {
            get { return this.m_TimeToFixationHourString; }
            set
            {
                if (this.m_TimeToFixationHourString != value)
                {
                    this.m_TimeToFixationHourString = value;
                    this.NotifyPropertyChanged("TimeToFixationHourString");
                }
            }
        }

        public void SetFixationStartTime()
        {
            if (this.m_ClientAccessioned == false)
            {
                if (this.m_FixationStartTimeManuallyEntered == false)
                {
                    if (this.m_ClientFixation == YellowstonePathology.Business.Specimen.Model.FixationType.Fresh)
                    {
                        this.m_FixationStartTime = this.m_DateReceived;
                    }
                    else if (this.m_ClientFixation != YellowstonePathology.Business.Specimen.Model.FixationType.NotApplicable)
                    {
                        this.m_FixationStartTime = this.m_CollectionTime;
                    }
                }
            }
            else
            {
                this.m_FixationStartTime = null;                
            }
            this.NotifyPropertyChanged("FixationStartTime");
        }

        public void SetFixationEndTime()
        {
            if (this.m_FixationStartTime.HasValue == true && this.m_ProcessorStartTime.HasValue == true)
            {
                TimeSpan processorFixationTime = new TimeSpan(0, this.m_ProcessorFixationTime.Value, 0);
                this.m_FixationEndTime = this.m_ProcessorStartTime.Value + processorFixationTime;
                this.NotifyPropertyChanged("FixationEndTime");
            }
        }

        public void SetTimeToFixation()
        {            
            if (this.m_CollectionTime.HasValue == true && this.m_FixationStartTime.HasValue == true)
            {
                TimeSpan timeSpan = new TimeSpan(this.m_FixationStartTime.Value.Ticks - this.m_CollectionTime.Value.Ticks);
                this.m_TimeToFixation = Convert.ToInt32(timeSpan.TotalMinutes);
                
                double ttf = Math.Round(timeSpan.TotalMinutes, 0);
                if (ttf <= 60) this.m_TimeToFixationHourString = YellowstonePathology.Business.Specimen.Model.TimeToFixationType.LessThanOneHour;
                else if (ttf > 60) this.m_TimeToFixationHourString = YellowstonePathology.Business.Specimen.Model.TimeToFixationType.GreaterThanOneHour;
            }
            else
            {
                this.m_TimeToFixation = null;
				this.m_TimeToFixationHourString = YellowstonePathology.Business.Specimen.Model.TimeToFixationType.LessThanOneHour;
            }  
          
            this.NotifyPropertyChanged("TimeToFixation");
            this.NotifyPropertyChanged("TimeToFixationString");
            this.NotifyPropertyChanged("TimeToFixationHourString");            
        }        

        public string TimeToFixationString
        {
            get
            {
                string result = null;
                if (this.m_TimeToFixation.HasValue == true)
                {
                    TimeSpan timeSpan = new TimeSpan(0, this.m_TimeToFixation.Value, 0);
                    if (timeSpan.TotalMinutes == 0)
                    {
                        result = "Immediate";
                    }
                    else if (timeSpan.TotalMinutes < 60)
                    {
                        result = timeSpan.TotalMinutes.ToString() + "min";
                    }
                    else
                    {
                        result = timeSpan.Hours + "hrs ";
                        if (timeSpan.Minutes > 0)
                        {
                            result += timeSpan.Minutes + "min";
                        }
                    }
                }
                else
                {
                    if (this.m_ClientAccessioned == false)
                    {
                        result = "Unknown";
                    }                    
                }
                return result;
            }                   
        }        

        public string ProcessorFixationTimeString
        {
            get
            {
                string result = null;
                if (this.m_ProcessorFixationTime.HasValue == true)
                {
                    TimeSpan timeSpan = new TimeSpan(0, this.m_ProcessorFixationTime.Value, 0);
                    if (timeSpan.TotalMinutes == 0)
                    {
                        result = "Unknown";
                    }
                    else if (timeSpan.TotalMinutes < 60)
                    {
                        result = timeSpan.TotalMinutes.ToString() + "min";
                    }
                    else
                    {
                        result = timeSpan.Hours + "hrs ";
                        if (timeSpan.Minutes > 0)
                        {
                            result += timeSpan.Minutes + "min";
                        }
                    }
                }
                else
                {
                    result = "Unknown";
                }
                return result;
            }
        }

        public virtual void PullOver(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            accessionTreeVisitor.Visit(this);
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        
        public bool OkToSetProcessorTimes(DateTime? processorStartTime)
        {
            bool result = false;
            if (this.ProcessorStartTime.HasValue == false)
            {
                result = true;
            }
            else if(processorStartTime.HasValue)
            {
                if(this.ProcessorStartTime.Value.AddHours(12) >= processorStartTime.Value)
                {
                    result = true;
                }
            }
            return result;
        }
	}
}
