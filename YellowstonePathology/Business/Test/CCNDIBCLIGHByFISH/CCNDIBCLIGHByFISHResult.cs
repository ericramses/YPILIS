﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CCNDIBCLIGHByFISH
{
	public class CCNDIBCLIGHByFISHResult
	{
		public static string NucleiScored = "200";
		protected string m_Result;
		protected string m_Interpretation;
		protected string m_ProbeSetDetail;
		protected string m_References;

		public CCNDIBCLIGHByFISHResult()
		{
		}

		public void SetResults(CCNDIBCLIGHByFISHTestOrder panelSetOrder)
		{
			panelSetOrder.Result = this.m_Result;
			panelSetOrder.Interpretation = this.m_Interpretation;
			panelSetOrder.ProbeSetDetail = this.m_ProbeSetDetail;
			panelSetOrder.ReportReferences = this.m_References;
			panelSetOrder.NucleiScored = CCNDIBCLIGHByFISHResult.NucleiScored;
		}
	}
}
