using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.NGCT
{
	[PersistentClass("tblNGCTTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class NGCTTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_NeisseriaGonorrhoeaeResult;
		private string m_ChlamydiaTrachomatisResult;
		private string m_NGResultCode;
		private string m_CTResultCode;
		private string m_Comment;
		private string m_Method;
		private string m_References;
		private string m_TestDevelopment;

		public NGCTTestOrder()
		{

		}

		public NGCTTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute, systemIdentity)
		{
            this.m_TechnicalComponentInstrumentId = Instrument.HOLOGICPANTHERID;
        }		

		public override YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
		{
			YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToAccept();
			if (result.Success == true)
			{
				if (string.IsNullOrEmpty(this.m_NGResultCode) == true)
				{
					result.Success = false;
					result.Message = "The NG result must be set before results may be accepted.";
				}
				else if (string.IsNullOrEmpty(this.m_CTResultCode) == true)
				{
					result.Success = false;
					result.Message = "The CT result must be set before results may be accepted.";
				}
			}
			return result;
		}

		public void OrderRetest(Business.User.SystemUser systemUser)
		{			
            string panelOrderId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            NGCTConfirmatoryPanel ngctConfirmatoryPanel = new NGCTConfirmatoryPanel();
            YellowstonePathology.Business.Test.PanelOrder panelOrder = new Business.Test.PanelOrder(this.m_ReportNo, panelOrderId, panelOrderId, ngctConfirmatoryPanel, systemUser.UserId);

            this.m_PanelOrderCollection.Add(panelOrder);

			TimeSpan timeSpanDelay = new TimeSpan(5, 0, 0, 0);
			this.Delayed = true;
            this.DelayedBy = systemUser.DisplayName;
			this.DelayedDate = DateTime.Now;
			this.ExpectedFinalTime = YellowstonePathology.Business.Helper.DateTimeExtensions.GetEndDateConsideringWeekends(this.m_ExpectedFinalTime.Value, timeSpanDelay);
		}

		public override string GetResultWithTestName()
		{
			StringBuilder result = new StringBuilder();
			result.Append("Chlamydia trachomatis: ");
			result.Append(this.m_ChlamydiaTrachomatisResult);
			result.Append(" Neisseria gonorrhoeae: ");
			result.Append(this.m_NeisseriaGonorrhoeaeResult);
			return result.ToString();
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.Append("Chlamydia trachomatis Result: ");
			result.AppendLine(this.m_ChlamydiaTrachomatisResult);
			result.Append("Neisseria gonorrhoeae Result: ");
			result.AppendLine(this.m_NeisseriaGonorrhoeaeResult);
			return result.ToString();
		}


		public YellowstonePathology.Business.Rules.MethodResult IsOkToSetResults()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new YellowstonePathology.Business.Rules.MethodResult();
			if (this.m_Accepted == true)
			{
				result.Success = false;
				result.Message = "Results may not be set because the results already have been accepted.";
			}
			else if (string.IsNullOrEmpty(this.m_NGResultCode) == true)
			{
				result.Success = false;
				result.Message = "The NG Result must be selected before the results can be set.";
			}
			else if (string.IsNullOrEmpty(this.m_CTResultCode) == true)
			{
				result.Success = false;
				result.Message = "The CT Result must be selected before the results can be set.";
			}
			return result;
		}

		[PersistentProperty()]
		public string NeisseriaGonorrhoeaeResult
		{
			get { return this.m_NeisseriaGonorrhoeaeResult; }
			set
			{
				if (this.m_NeisseriaGonorrhoeaeResult != value)
				{
					this.m_NeisseriaGonorrhoeaeResult = value;
					this.NotifyPropertyChanged("NeisseriaGonorrhoeaeResult");
				}
			}
		}

		[PersistentProperty()]
		public string ChlamydiaTrachomatisResult
		{
			get { return this.m_ChlamydiaTrachomatisResult; }
			set
			{
				if (this.m_ChlamydiaTrachomatisResult != value)
				{
					this.m_ChlamydiaTrachomatisResult = value;
					this.NotifyPropertyChanged("ChlamydiaTrachomatisResult");
				}
			}
		}

		[PersistentProperty()]
		public string NGResultCode
		{
			get { return this.m_NGResultCode; }
			set
			{
				if (this.m_NGResultCode != value)
				{
					this.m_NGResultCode = value;
					this.NotifyPropertyChanged("NGResultCode");
				}
			}
		}

		[PersistentProperty()]
		public string CTResultCode
		{
			get { return this.m_CTResultCode; }
			set
			{
				if (this.m_CTResultCode != value)
				{
					this.m_CTResultCode = value;
					this.NotifyPropertyChanged("CTResultCode");
				}
			}
		}

		[PersistentProperty()]
		public string Comment
		{
			get { return this.m_Comment; }
			set
			{
				if (this.m_Comment != value)
				{
					this.m_Comment = value;
					this.NotifyPropertyChanged("Comment");
				}
			}
		}

		[PersistentProperty()]
		public string Method
		{
			get { return this.m_Method; }
			set
			{
				if (this.m_Method != value)
				{
					this.m_Method = value;
					this.NotifyPropertyChanged("Method");
				}
			}
		}

		[PersistentProperty()]
		public string References
		{
			get { return this.m_References; }
			set
			{
				if (this.m_References != value)
				{
					this.m_References = value;
					this.NotifyPropertyChanged("References");
				}
			}
		}

		[PersistentProperty()]
		public string TestDevelopment
		{
			get { return this.m_TestDevelopment; }
			set
			{
				if (this.m_TestDevelopment != value)
				{
					this.m_TestDevelopment = value;
					this.NotifyPropertyChanged("TestDevelopment");
				}
			}
		}
	}
}

