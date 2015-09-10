using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPVTWI
{
	public class HPVTWIResult
	{
        public static string OveralResultCodePositive = "HPVTWPSTV";

		public static string PositiveResult = "Positive";
		public static string NegativeResult = "Negative";
		public static string IndeterminateResult = "Indeterminate";
		public static string QnsResult = "QNS";
		public static string LowDnaResult = "Low gDNA";
		public static string HighCVResult = "High %CV";
		public static string LowFamFozResult = "LowFamFoz";
		public static string Unsatisfactory = "Unsatisfactory";
        public static string InsuficientDNA = "Insufficient DNA to perform analysis";        

		public static string CytologyReportNoPlaceHolder = "cytology_accessionno";

        public static string TestInformation = "Testing for high-risk HPV was performed using the Invader technology from Hologic after automated DNA extraction from the " +
            "ThinPrep sample.  The Invader chemistry is a proprietary signal amplification method capable of detecting low levels of target DNA.  Using analyte specific reagents, " +
            "the assay is capable of detecting genotypes 16, 18, 31, 33, 35, 39, 45, 51, 52, 56, 58, 59, 66 and 68.  The assay also evaluates specimen adequacy by measuring the " +
            "amount of normal human DNA present in the sample.  HPV types 16 & 18 are frequently associated with high risk for development of high grade dysplasia and anogenital " +
            "carcinoma.  HPV types 31/33/35/39/45/51/52/56/58/59/68 have also been classified as high-risk for the development of high grade dysplasia and anogenital carcinoma.  " +
            "HPV type 66 has been classified as probable high-risk.  A negative test result does not necessarily imply the absence of HPV infection as this assay targets only the " +
            "most common high-risk genotypes and insufficient sampling can affect results.  These results should be correlated with cytology and clinical exam results.";

        public static string References = "Darragh TM, Colgan TJ, Cox JT et al. The Lower Anogenital Squamous Terminology (LAST) Standardization Project for HPV-Associated Lesions: " +
            "Background and Consensus Recommendations from the College of American Pathologists and the American Society for Colposcopy and Cervical Pathology. Arch Pathol " +
            "Lab Med 2012 Oct; 136(10): 1266-97.";
		
		public static string InsufficientComment = "The quantity of genomic DNA present in the sample is insufficient to perform the analysis, even after an attempt to " +
			"increase DNA content by using more specimen volume.  There is no charge for this specimen.  Consider repeat testing, if clinically indicated.";
		
		public static string IndeterminateComment = "Results are indeterminate due to technical issues with this specific specimen, which may be related to specimen DNA " +
			"quality or interfering substances.  Consider repeat testing, if clinically indicated.";

        protected string m_PreliminaryResultCode;
        protected string m_A5A6Result;
        protected string m_A7Result;
        protected string m_A9Result;
        protected string m_OveralResultCode;
        protected string m_OveralResult;
        protected string m_Comment;

		public HPVTWIResult()
		{
			
		}

        public string PreliminaryResultCode
        {
            get { return this.m_PreliminaryResultCode; }
        }

        public string OveralResultCode
        {
            get { return this.m_OveralResultCode; }
        }

        public string OveralResult
        {
            get { return this.m_OveralResult; }
        }

        public void Clear(YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrder)
        {
            
        }

        public void Clear(YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder)
        {
                     
        }

		public virtual void SetResult(YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrder,
			YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {            			

            panelSetOrder.ResultCode = this.m_OveralResultCode;
            panelSetOrder.Result = this.m_OveralResult;
            panelSetOrder.TestInformation = TestInformation;
            panelSetOrder.References = References;
            panelSetOrder.Comment = this.m_Comment;
        }

        public virtual void AcceptResults(YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {            
            YellowstonePathology.Business.Test.PanelOrder panelOrder = panelSetOrder.PanelOrderCollection.GetUnacceptedPanelOrder();            
            panelOrder.AcceptResults(systemIdentity.User);

			panelSetOrder.Accept(systemIdentity.User);
        }

        public void UnacceptResults(YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrder)
        {                        
			panelSetOrder.Unaccept();
        }

		public static YellowstonePathology.Business.Rules.MethodResult IsOkToAddSecondPanelOrder(YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrder)
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            if (panelSetOrder.PanelOrderCollection.Count > 1)
            {
				result.Success = false;
				result.Message = "A repeat test has already been ordered.";
            }

			return result;
		}

        public void AddSecondPanelOrder(YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string panelOrderId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            HPVTWIPanel HPVTWIPanel = new HPVTWIPanel();
			YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)YellowstonePathology.Business.Test.PanelOrderFactory.GetPanelOrder(panelSetOrder.ReportNo, panelOrderId, panelOrderId, HPVTWIPanel, systemIdentity.User.UserId);                               
            panelSetOrder.PanelOrderCollection.Add(panelOrder);
        }

		protected static YellowstonePathology.Business.Rules.MethodResult IsOkToRemoveSecondPanelOrder(YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrder,
			YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder)
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
			result.Success = false;
			result.Message = "Cannot remove the panel order having results set.";
			if (panelSetOrder.PanelOrderCollection.Count == 2 && panelSetOrder.PanelOrderCollection[1].PanelOrderId != panelOrder.PanelOrderId)
			{
				result.Success = true;
				result.Message = null;
			}
			return result;
		}

		protected void RemoveSecondPanelOrder(YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrder)
		{
			panelSetOrder.PanelOrderCollection.RemoveAt(1);
		}

        public void DeleteUnacceptedPanel(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            int unacceptedPanelOrderCount = panelSetOrder.PanelOrderCollection.GetUnacceptedPanelCount();
            if (unacceptedPanelOrderCount == 1)
            {
                YellowstonePathology.Business.Test.PanelOrder panelOrder = panelSetOrder.PanelOrderCollection.GetUnacceptedPanelOrder();
                panelSetOrder.PanelOrderCollection.Remove(panelOrder);                
            }
        }

        public virtual void FinalizeResults(YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {            
            panelSetOrder.Finalize(systemIdentity.User);                                    
        }

        public virtual void UnFinalizeResults(YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrder)
        {
            panelSetOrder.ResultCode = null;
            panelSetOrder.Result = null;
            panelSetOrder.References = null;
            panelSetOrder.TestInformation = null;
            panelSetOrder.Comment = null;              
            panelSetOrder.Unfinalize();            
        }

        public static YellowstonePathology.Business.Rules.MethodResult IsOkToFinal(YellowstonePathology.Business.Test.PanelOrderCollection panelOrderCollection)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            if (panelOrderCollection.GetUnacceptedPanelCount() > 0)
            {
                result.Success = false;
                result.Message = "This case cannot be finalized because it has panels that are not accepted.";
            }            
            return result;
        }

        public static YellowstonePathology.Business.Rules.MethodResult IsOkToUnFinalize(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            if (panelSetOrder.Final == false)
            {
                result.Success = false;
                result.Message = "This case cannot be unfinalized because it is not final.";
            }
            return result;
        }

        public static YellowstonePathology.Business.Rules.MethodResult IsOkToUnAccept(YellowstonePathology.Business.Test.PanelOrderCollection panelOrderCollection)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
			if (panelOrderCollection.GetAcceptedPanelCount() != 1)
            {
                result.Success = false;
                result.Message = "The results cannot be unaccepted because there are unaccepted panels.";
            }
            return result;
        }

        public static YellowstonePathology.Business.Rules.MethodResult IsOkToAccept(YellowstonePathology.Business.Test.PanelOrderCollection panelOrderCollection)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)panelOrderCollection.GetUnacceptedPanelOrder();

            if (panelOrderCollection.GetUnacceptedPanelCount() != 1)
            {
                result.Success = false;
                result.Message = "The results cannot be accepted because there are no unaccepted panels.";
            }
            else if(string.IsNullOrEmpty(panelOrder.ResultCode) == true)
            {
                result.Success = false;
                result.Message = "The results cannot be accepted because there is no result.";
            }       
                        
            return result;
        }

        public static YellowstonePathology.Business.Rules.MethodResult IsOkToSetResult(YellowstonePathology.Business.Test.PanelOrderCollection panelOrderCollection)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            if (panelOrderCollection.GetUnacceptedPanelCount() == 0)
            {
                result.Success = false;
                result.Message = "The result cannot be set because there are no unaccepted panels.";
            }            
            return result;
        }
	}
}
