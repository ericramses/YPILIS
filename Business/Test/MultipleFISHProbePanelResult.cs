using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
	public class MultipleFISHProbePanelResult
	{
		public static string NucleiScored = "100";
		protected string m_Result;
		protected string m_Interpretation;
		protected string m_ProbeSetDetail;

		public MultipleFISHProbePanelResult()
		{
		}

		public void SetResults(PanelSetOrderMultipleFISHProbePanel panelSetOrder)
		{
			panelSetOrder.Result = this.m_Result;
			panelSetOrder.Interpretation = this.m_Interpretation;
			panelSetOrder.ProbeSetDetail = this.m_ProbeSetDetail;
			panelSetOrder.NucleiScored = MultipleFISHProbePanelResult.NucleiScored;
		}
	}
}
