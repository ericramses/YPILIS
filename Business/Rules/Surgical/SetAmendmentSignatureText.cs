using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Surgical
{
	public class SetAmendmentSignatureText
	{
		protected YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
		protected YellowstonePathology.Business.Amendment.Model.Amendment m_Amendment;
        protected YellowstonePathology.Business.Domain.Lock m_Lock;
		protected YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
		protected YellowstonePathology.Business.Rules.Rule m_Rule;

		public SetAmendmentSignatureText()
		{
			this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
			this.m_Rule.ActionList.Add(IsNotFinal);
			this.m_Rule.ActionList.Add(IsFinalAndNotDistributed);
			this.m_Rule.ActionList.Add(IsFinalAndDistributed);
			this.m_Rule.ActionList.Add(IsNotLocked);

		}

		public void IsNotFinal()
		{
			if (!this.m_Amendment.Final)
			{
				this.m_Amendment.SignatureButtonText = "Sign Amendment";
				this.m_Amendment.SignatureButtonIsEnabled = true;
				this.m_Amendment.DeleteButtonIsEnabled = true;
			}
		}

		public void IsFinalAndNotDistributed()
		{
			if (this.m_Amendment.Final)
			{
				if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.HasDistributionAfter(this.m_Amendment.FinalTime.Value) == false)
				{
					this.m_Amendment.SignatureButtonText = "Unsign Amendment";
					this.m_Amendment.SignatureButtonIsEnabled = true;
					this.m_Amendment.DeleteButtonIsEnabled = false;
				}
			}
		}

		public void IsFinalAndDistributed()
		{
			if (this.m_Amendment.Final)
			{
                if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.HasDistributionAfter(this.m_Amendment.FinalTime.Value) == true)
				{
					this.m_Amendment.SignatureButtonText = "Electronic Signature";
					this.m_Amendment.SignatureButtonIsEnabled = false;
					this.m_Amendment.DeleteButtonIsEnabled = false;
				}
			}
		}

		public void IsNotLocked()
		{
			if (!this.m_Lock.LockAquired)
			{
				this.m_Amendment.SignatureButtonIsEnabled = false;
				this.m_Amendment.DeleteButtonIsEnabled = false;
			}
		}

		public void Execute(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Amendment.Model.Amendment amendment, YellowstonePathology.Business.Domain.Lock theLock)
		{
            this.m_PanelSetOrder = panelSetOrder;
			this.m_Amendment = amendment;
			this.m_Lock = theLock;
			this.m_ExecutionStatus = new ExecutionStatus();
			this.m_Rule.Execute(m_ExecutionStatus);
		}
	}
}
