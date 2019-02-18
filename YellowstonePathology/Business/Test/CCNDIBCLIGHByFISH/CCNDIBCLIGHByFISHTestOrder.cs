using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.CCNDIBCLIGHByFISH
{
	[PersistentClass("tblCCNDIBCLIGHByFISHTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class CCNDIBCLIGHByFISHTestOrder : PanelSetOrder
	{
		private string m_Result;
		private string m_Interpretation;
		private string m_ProbeSetDetail;
		private string m_NucleiScored;
        private string m_ProbeComment;

        public CCNDIBCLIGHByFISHTestOrder()
		{
		}

		public CCNDIBCLIGHByFISHTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Result
		{
			get { return this.m_Result; }
			set
			{
				if (this.m_Result != value)
				{
					this.m_Result = value;
					this.NotifyPropertyChanged("Result");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
		public string Interpretation
		{
			get { return this.m_Interpretation; }
			set
			{
				if (this.m_Interpretation != value)
				{
					this.m_Interpretation = value;
					this.NotifyPropertyChanged("Interpretation");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
		public string ProbeSetDetail
		{
			get { return this.m_ProbeSetDetail; }
			set
			{
				if (this.m_ProbeSetDetail != value)
				{
					this.m_ProbeSetDetail = value;
					this.NotifyPropertyChanged("ProbeSetDetail");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string NucleiScored
		{
			get { return this.m_NucleiScored; }
			set
			{
				if (this.m_NucleiScored != value)
				{
					this.m_NucleiScored = value;
					this.NotifyPropertyChanged("NucleiScored");
				}
			}
		}

        [PersistentProperty()]
        public string ProbeComment
        {
            get { return this.m_ProbeComment; }
            set
            {
                if (this.m_ProbeComment != value)
                {
                    this.m_ProbeComment = value;
                    this.NotifyPropertyChanged("ProbeComment");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Result: " + this.m_Result);
            result.AppendLine();            

            result.AppendLine("Interpretation: " + this.m_Interpretation);
            result.AppendLine();

            return result.ToString().Trim();
        }
	}
}
