using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Domain.Test.ZAP70LymphoidPanel
{
	public class ZAP70LymphoidPanelResult
	{
		protected string m_Result;
		protected string m_Comment;
		protected string m_References;

		public ZAP70LymphoidPanelResult()
		{

		}

		public void SetResults(ZAP70LymphoidPanelTestOrder panelSetOrder)
		{
			panelSetOrder.Result = this.m_Result;
			panelSetOrder.Comment = this.m_Comment;
			panelSetOrder.References = this.m_References;
		}
	}
}
