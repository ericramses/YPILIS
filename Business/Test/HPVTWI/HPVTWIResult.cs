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

        public static string TestInformation = "The Aptima HPV assay is an in vitro nucleic acid amplification test for the qualitative detection of E6/E7 viral " +
            "messenger RNA (mRNA) from 14 high-risk types of human papillomavirus (HPV) in cervical specimens. The high-risk HPV types detected by the assay " +
            "include: 16, 18, 31, 33, 35, 39, 45, 51, 52, 56, 58, 59, 66, and 68. The Aptima HPV assay does not discriminate between the 14 high-risk types. " +
            "Cervical specimens in ThinPrep Pap Test vials containing PreservCyt Solution and collected with broom-type or cytobrush/spatula collection devices* " +
            "may be tested with the Aptima HPV assay. The assay is used with the Panther System. The use of the test is indicated: 1. To screen women 21 years and " +
            "older with atypical squamous cells of undetermined significance (ASC-US) cervical cytology results to determine the need for referral to colposcopy. The " +
            "results of this test are not intended to prevent women from proceeding to colposcopy. 2. In women 30 years and older, the Aptima HPV assay can be used with " +
            "cervical cytology to adjunctively screen to assess the presence or absence of high-risk HPV types. This information, together with the physician’s " +
            "assessment of cytology history, other risk factors, and professional guidelines, may be used to guide patient management. * Broom-type device (e.g., " +
            "Wallach Pipette) or endocervical brush/spatula. This assay is not intended for use as a screening device for women under age 30 with normal cervical " +
            "cytology. The Aptima HPV assay is not intended to substitute for regular cervical cytology screening. Detection of HPV using the Aptima HPV assay does not " +
            "differentiate HPV types and cannot evaluate persistence of any one type. The use of this assay has not been evaluated for the management of HPV vaccinated " +
            "women, women with prior ablative or excisional therapy, hysterectomy, who are pregnant, or who have other risk factors (e.g., HIV +, immunocompromised, " +
            "history of sexually transmitted infection).The Aptima HPV assay is designed to enhance existing methods for the detection of cervical disease and should be " +
            "used in conjunction with clinical information derived from other diagnostic and screening tests, physical examinations, and full medical history in " +
            "accordance with appropriate patient management procedures.";

        public static string References = "1. Darragh TM, Colgan TJ, Cox JT et al. The Lower Anogenital Squamous Terminology (LAST) Standardization Project for " +
            "HPV-Associated Lesions: Background and Consensus Recommendations from the College of American Pathologists and the American Society for Colposcopy and " +
            "Cervical Pathology. Arch Pathol Lab Med 2012 Oct; 136(10): 1266-97." + Environment.NewLine +
            "2.Doorbar, J. 2006. Molecular biology of human papillomavirus infection and cervical cancer.Clin Sci(Lond). 110(5):525-41.";

        public static string ASRComment = "This test was performed using a US FDA approved RNA probe kit.  The procedure and performance were verified by Yellowstone Pathology Institute (YPI).";

		public static string InsufficientComment = "The quantity of genomic DNA present in the sample is insufficient to perform the analysis, even after an attempt to " +
			"increase DNA content by using more specimen volume.  There is no charge for this specimen.  Consider repeat testing, if clinically indicated.";
		
		public static string IndeterminateComment = "Results are indeterminate due to technical issues with this specific specimen, which may be related to specimen DNA " +
			"quality or interfering substances.  Consider repeat testing, if clinically indicated.";
        
        protected string m_ResultCode;
        protected string m_Result;
        protected string m_Comment;

		public HPVTWIResult()
		{
			
		}

        public string ResultCode
        {
            get { return this.m_ResultCode; }
        }        

        public string Result
        {
            get { return this.m_Result; }
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

            panelSetOrder.ResultCode = this.m_ResultCode;
            panelSetOrder.Result = this.m_Result;
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
