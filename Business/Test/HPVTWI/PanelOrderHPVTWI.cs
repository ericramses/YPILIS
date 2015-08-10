using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.HPVTWI
{
	[PersistentClass("tblPanelOrderHPVTWI", "tblPanelOrder", "YPIDATA")]
	public class PanelOrderHPVTWI : PanelOrder
	{
		private string m_A5A6Result;
		private string m_A7Result;
		private string m_A9Result;

        public PanelOrderHPVTWI()
        {

        }

		public PanelOrderHPVTWI(string reportNo, string objectId, string panelOrderId, YellowstonePathology.Business.Panel.Model.Panel panel, int orderedById)
			: base(reportNo, objectId, panelOrderId, panel, orderedById)
		{

		}       		

		public override void AcceptResults(Rules.RuleExecutionStatus ruleExecutionStatus, Test.AccessionOrder accessionOrder, Business.User.SystemUser orderingUser)
		{
            throw new Exception("Not implemented here.");
		}

		public override void UnacceptResults()
		{
            throw new Exception("Not implemented here.");
		}

		[PersistentProperty()]
		public string A5A6Result
		{
			get { return this.m_A5A6Result; }
			set
			{
				if (this.m_A5A6Result != value)
				{
					this.m_A5A6Result = value;
					this.NotifyPropertyChanged("A5A6Result");
				}
			}
		}

		[PersistentProperty()]
		public string A7Result
		{
			get { return this.m_A7Result; }
			set
			{
				if (this.m_A7Result != value)
				{
					this.m_A7Result = value;
					this.NotifyPropertyChanged("A7Result");
				}
			}
		}

		[PersistentProperty()]
		public string A9Result
		{
			get { return this.m_A9Result; }
			set
			{
				if (this.m_A9Result != value)
				{
					this.m_A9Result = value;
					this.NotifyPropertyChanged("A9Result");
				}
			}
		}
	}
}
