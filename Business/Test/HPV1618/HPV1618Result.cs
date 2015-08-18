using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618
{
	public class HPV1618Result
	{
        public static string PositiveResult = "Positive";
        public static string NegativeResult = "Negative";
        public static string IndeterminateResult = "Indeterminate";		

        protected string m_ResultCode;
        protected string m_HPV16Result;
        protected string m_HPV18Result;        
        protected string m_SquamousCellCarcinomaInterpretation;

        protected string m_Method = "DNA was extracted from the patient’s specimen using an automated method.  Real-time PCR amplification was " +
			"performed using primers and hydrolysis probes specific for HPV types 16 and 18. The beta-actin gene was used as an internal control. " +
			"The real-time PCR curves were analyzed to determine the presence of HPV types 16 and 18 in the specimen.";

        protected string m_References = "Highly Effective Detection of Human Papillomavirus 16 and 18 DNA by a Testing Algorithm Combining Broad-Spectrum " +
            "and Type-Specific PCR J Clin Microbiol. 2006 September; 44(9): 3292–3298.";        

		public HPV1618Result()
		{
            
		}

		public string ResultCode
		{
			get { return this.m_ResultCode; }
		}

		public void SetResult(YellowstonePathology.Business.Test.HPV1618.HPV1618TestOrder panelSetOrder)
		{            
			YellowstonePathology.Business.Test.HPV1618.PanelOrderHPV1618 panelOrder = (YellowstonePathology.Business.Test.HPV1618.PanelOrderHPV1618)panelSetOrder.PanelOrderCollection.GetUnacceptedPanelOrder();
			panelOrder.HPV16Result = this.m_HPV16Result;
            panelOrder.HPV18Result = this.m_HPV18Result;
			panelOrder.ResultCode = this.m_ResultCode;            

            panelSetOrder.ResultCode = this.m_ResultCode;
            panelSetOrder.HPV16Result = this.m_HPV16Result;
            panelSetOrder.HPV18Result = this.m_HPV18Result;
            panelSetOrder.Method = this.m_Method;
            panelSetOrder.References = this.m_References;

            if (panelSetOrder.Indication == HPV1618Indication.SquamousCellCarcinoma)
            {
                panelSetOrder.Interpretation = this.m_SquamousCellCarcinomaInterpretation;
            }
            else
            {
                panelSetOrder.Interpretation = null;
            }
		}

		public virtual void AcceptResults(YellowstonePathology.Business.Test.HPV1618.HPV1618TestOrder panelSetOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			YellowstonePathology.Business.Test.PanelOrder panelOrder = panelSetOrder.PanelOrderCollection.GetUnacceptedPanelOrder();
			panelOrder.AcceptResults(systemIdentity.User);
			panelSetOrder.Accept(systemIdentity.User);
		}

		public virtual void UnacceptResults(YellowstonePathology.Business.Test.HPV1618.HPV1618TestOrder panelSetOrder)
		{
			YellowstonePathology.Business.Test.PanelOrder acceptedPanelOrder = panelSetOrder.PanelOrderCollection.GetLastAcceptedPanelOrder();
			acceptedPanelOrder.UnacceptResults();

            panelSetOrder.Unaccept();
		}

		public void Clear(HPV1618TestOrder panelSetOrderHPV1618)
        {
            panelSetOrderHPV1618.Method = null;
            panelSetOrderHPV1618.References = null;
            panelSetOrderHPV1618.Interpretation = null;
            panelSetOrderHPV1618.HPV16Result = null;
            panelSetOrderHPV1618.HPV18Result = null;
        }

		public virtual void FinalizeResults(YellowstonePathology.Business.Test.HPV1618.HPV1618TestOrder panelSetOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			panelSetOrder.Finalize(systemIdentity.User);
		}

		public virtual void UnFinalizeResults(YellowstonePathology.Business.Test.HPV1618.HPV1618TestOrder panelSetOrder)
		{
			panelSetOrder.Unfinalize();
		}

		public static YellowstonePathology.Business.Rules.MethodResult IsOkToAccept(YellowstonePathology.Business.Test.HPV1618.HPV1618TestOrder panelSetOrder)
		{
			YellowstonePathology.Business.Rules.MethodResult result = panelSetOrder.IsOkToAccept();

			if (result.Success == true)
            {
				if (string.IsNullOrEmpty(panelSetOrder.ResultCode) == true)
				{
					result.Success = false;
					result.Message = "The results cannot be accepted because there is no result.";
				}
            }
			return result;
		}

		public static YellowstonePathology.Business.Rules.MethodResult IsOkToSetResult(YellowstonePathology.Business.Test.HPV1618.HPV1618TestOrder panelSetOrder)
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();

			if (string.IsNullOrEmpty(panelSetOrder.Indication) == true)
			{
				result.Success = false;
				result.Message = "You must choose an indication first.";
			}
			else if (panelSetOrder.Accepted == true)
			{
				result.Success = false;
				result.Message = "The results cannot be set because they have already been accepted.";
			}
			return result;
		}
	}
}
