using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
	[PersistentClass("tblPanelOrderCytology", "tblPanelOrder", "YPIDATA")]
	public class PanelOrderCytology : YellowstonePathology.Business.Test.PanelOrder
	{
		private int m_ScreenedById;
		private string m_ScreenedByName;		
		private string m_SpecimenAdequacy;
		private string m_ScreeningImpression;
		private string m_ScreeningImpressionComment;
		private string m_OtherConditions;
		private string m_ReportComment;
		private string m_InternalComment;
		private string m_ScreenerComment;
		private string m_OrderComment;
		private string m_ScreeningType;
		private bool m_QC;
		private bool m_ImagerError;
		private bool m_NoCharge;
		private bool m_PhysicianInterpretation;
		private int m_SlideCount;
		private bool m_ECCCheckPerformed;
		private bool m_ScreeningError;
        private bool m_Reconciled;

		public PanelOrderCytology()
		{

		}

        public PanelOrderCytology(string reportNo, string objectId, string panelOrderId, YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapScreeningPanel thinPrepPapScreeningPanel, int orderedById, string orderedByInitials)
            : base(reportNo, objectId, panelOrderId, thinPrepPapScreeningPanel, orderedById, orderedByInitials)
		{
            this.m_ScreeningType = thinPrepPapScreeningPanel.ScreeningType;
            this.m_Accepted = false;
            this.m_AcceptedById = 0;
            this.m_AssignedToId = 0;

            this.m_Acknowledged = true;
            this.m_AcknowledgedById = orderedById;
            this.m_AcknowledgedDate = DateTime.Today;
            this.m_AcknowledgedTime = DateTime.Now;

            this.m_ECCCheckPerformed = false;
            this.m_ScreeningError = false;

            this.m_QC = thinPrepPapScreeningPanel.IsQC;
            this.NotifyPropertyChanged(string.Empty);
		}

        public void FromExistingPanelOrder(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrder, string screeningType, bool isQC, int orderingUserId, string orderingUserInitials) 
        {
			this.m_PanelOrderId = base.PanelOrderId;
            this.m_PanelId = 38;
            this.m_ResultCode = "59999";
            this.m_PanelName = "Cytology Screening";
            this.m_ScreeningType = screeningType;
            this.m_Accepted = false;
            this.m_AcceptedById = 0;
            this.m_AssignedToId = 0;

            this.m_OrderedById = orderingUserId;
            this.m_OrderedByInitials = orderingUserInitials;
            this.m_OrderDate = DateTime.Today;
            this.m_OrderTime = DateTime.Now;

            this.m_Acknowledged = true;
            this.m_AcknowledgedById = orderingUserId;
            this.m_AcknowledgedDate = DateTime.Today;
            this.m_AcknowledgedTime = DateTime.Now;

            this.m_ECCCheckPerformed = false;
            this.m_ScreeningError = false;

            this.m_ScreeningImpression = panelOrder.ScreeningImpression;
            this.m_SpecimenAdequacy = panelOrder.SpecimenAdequacy;
            this.m_OtherConditions = panelOrder.OtherConditions;
            this.m_ReportComment = panelOrder.ReportComment;
            this.m_ResultCode = panelOrder.ResultCode;

            this.m_QC = isQC;
            this.NotifyPropertyChanged(string.Empty);
        }		

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "11", "null", "int")]
		public int ScreenedById
		{
			get { return this.m_ScreenedById; }
			set
			{
				if (this.m_ScreenedById != value)
				{
					this.m_ScreenedById = value;
					this.NotifyPropertyChanged("ScreenedById");
				}
			}
		}		

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
		public string ScreeningType
		{
			get { return this.m_ScreeningType; }
			set
			{
				if (this.m_ScreeningType != value)
				{
					this.m_ScreeningType = value;
					this.NotifyPropertyChanged("ScreeningType");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "150", "null", "varchar")]
		public string ScreenedByName
		{
			get { return this.m_ScreenedByName; }
			set
			{
				if (this.m_ScreenedByName != value)
				{
					this.m_ScreenedByName = value;
					this.NotifyPropertyChanged("ScreenedByName");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string ScreeningImpression
		{
			get { return this.m_ScreeningImpression; }
			set
			{
				if (this.m_ScreeningImpression != value)
				{
					this.m_ScreeningImpression = value;
					this.NotifyPropertyChanged("ScreeningImpression");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string ScreeningImpressionComment
		{
			get { return this.m_ScreeningImpressionComment; }
			set
			{
				if (this.m_ScreeningImpressionComment != value)
				{
					this.m_ScreeningImpressionComment = value;
					this.NotifyPropertyChanged("ScreeningImpressionComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string OtherConditions
		{
			get { return this.m_OtherConditions; }
			set
			{
				if (this.m_OtherConditions != value)
				{
					this.m_OtherConditions = value;
					this.NotifyPropertyChanged("OtherConditions");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
		public string ReportComment
		{
			get { return this.m_ReportComment; }
			set
			{
				if (this.m_ReportComment != value)
				{
					this.m_ReportComment = value;
					this.NotifyPropertyChanged("ReportComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
		public string InternalComment
		{
			get { return this.m_InternalComment; }
			set
			{
				if (this.m_InternalComment != value)
				{
					this.m_InternalComment = value;
					this.NotifyPropertyChanged("InternalComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
		public string ScreenerComment
		{
			get { return this.m_ScreenerComment; }
			set
			{
				if (this.m_ScreenerComment != value)
				{
					this.m_ScreenerComment = value;
					this.NotifyPropertyChanged("ScreenerComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string OrderComment
		{
			get { return this.m_OrderComment; }
			set
			{
				if (this.m_OrderComment != value)
				{
					this.m_OrderComment = value;
					this.NotifyPropertyChanged("ProfessionalComponentFacilityId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "tinyint")]
		public bool QC
		{
			get { return this.m_QC; }
			set
			{
				if (this.m_QC != value)
				{
					this.m_QC = value;
					this.NotifyPropertyChanged("QC");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "tinyint")]
		public bool ImagerError
		{
			get { return this.m_ImagerError; }
			set
			{
				if (this.m_ImagerError != value)
				{
					this.m_ImagerError = value;
					this.NotifyPropertyChanged("ImagerError");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "tinyint")]
		public bool NoCharge
		{
			get { return this.m_NoCharge; }
			set
			{
				if (this.m_NoCharge != value)
				{
					this.m_NoCharge = value;
					this.NotifyPropertyChanged("NoCharge");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "tinyint")]
		public bool PhysicianInterpretation
		{
			get { return this.m_PhysicianInterpretation; }
			set
			{
				if (this.m_PhysicianInterpretation != value)
				{
					this.m_PhysicianInterpretation = value;
					this.NotifyPropertyChanged("PhysicianInterpretation");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "11", "null", "int")]
		public int SlideCount
		{
			get { return this.m_SlideCount; }
			set
			{
				if (this.m_SlideCount != value)
				{
					this.m_SlideCount = value;
					this.NotifyPropertyChanged("SlideCount");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "tinyint")]
		public bool ECCCheckPerformed
		{
			get { return this.m_ECCCheckPerformed; }
			set
			{
				if (this.m_ECCCheckPerformed != value)
				{
					this.m_ECCCheckPerformed = value;
					this.NotifyPropertyChanged("ECCCheckPerformed");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "tinyint")]
		public bool ScreeningError
		{
			get { return this.m_ScreeningError; }
			set
			{
				if (this.m_ScreeningError != value)
				{
					this.m_ScreeningError = value;
					this.NotifyPropertyChanged("ScreeningError");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool Reconciled
        {
            get { return this.m_Reconciled; }
            set
            {
                if (this.m_Reconciled != value)
                {
                    this.m_Reconciled = value;
                    this.NotifyPropertyChanged("Reconciled");
                }
            }
        }

        public void AppendReportComment(string comment)
        {
            if (string.IsNullOrEmpty(this.m_ReportComment) == true)
            {
                this.m_ReportComment = comment;
            }
            else
            {
                this.m_ReportComment += "  ";
                this.m_ReportComment += comment;
            }
        }
	}
}
