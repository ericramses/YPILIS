using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEResult
	{
        public const string LSEColonReferences = "Practice EoGAi, Group PW. Recommendations from the EGAPP Working Group; genetic testing strategies in newly diagnosed " +
            "individuals with colorectal cancer aimed at reducing morbidity and mortality from Lynch Syndrome in relatives. Genet Med. 2009 January; 11(1): 35-41.";

        public const string LSEGYNReferences = "Meyer L, Broaddus R, Lu K. Endometrial cancer and Lynch syndrome: clinical and pathologic considerations. Cancer Control. 2009;16(1):14–22";
        public const string LSEGENReferences = "Le DT, Durham, JN, Smith KN et al.  Mismatch-repair deficiency predicts response of solid tumors to PD-1 blockade.  Science. 2017 July 28; 357(6349): 409-413.";

        public static string IHCMethod = YellowstonePathology.Business.Test.LynchSyndrome.LSEIHCResult.Method;
		public static string IHCBRAFMethod = "IHC: " + YellowstonePathology.Business.Test.LynchSyndrome.LSEIHCResult.Method + " BRAF: " + YellowstonePathology.Business.Test.BRAFV600EK.BRAFResult.Method;
		public static string IHCBRAFMLHMethod = "IHC: " + YellowstonePathology.Business.Test.LynchSyndrome.LSEIHCResult.Method + " BRAF: " + YellowstonePathology.Business.Test.BRAFV600EK.BRAFResult.Method + " MLH: " + YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisResult.Method;

        public static string GeneralIndication = "Assess tumor for mismatch repair deficiency to determine eligibility for PD-1 blockade therapy; screening for Lynch Syndrome.";

        protected LSEResultEnum m_PMS2Result;
		protected LSEResultEnum m_MSH6Result;
		protected LSEResultEnum m_MSH2Result;
		protected LSEResultEnum m_MLH1Result;        
        protected LSEResultEnum m_BrafResult;
        protected LSEResultEnum m_MethResult;

        protected bool m_BRAFIsIndicated;

		protected string m_Interpretation;
		protected string m_Comment;
        protected string m_Method;
        protected string m_References;

		public LSEResult()
		{
            
		}		

		public LSEResultEnum PMS2Result
		{
			get { return this.m_PMS2Result; }
			set { this.m_PMS2Result = value; }
		}

		public LSEResultEnum MSH6Result
		{
			get { return this.m_MSH6Result; }
			set { this.m_MSH6Result = value; }
		}

		public LSEResultEnum MSH2Result
		{
			get { return this.m_MSH2Result; }
			set { this.m_MSH2Result = value; }
		}

		public LSEResultEnum MLH1Result
		{
			get { return this.m_MLH1Result; }
			set { this.m_MLH1Result = value; }
		}

        public LSEResultEnum BrafResult
        {
            get { return this.m_BrafResult; }
            set { this.m_BrafResult = value; }
        }

        public LSEResultEnum MethResult
        {
            get { return this.m_MethResult; }
            set { this.m_MethResult = value; }
        }

        public bool BRAFIsIndicated
        {
            get { return this.m_BRAFIsIndicated; }
            set { this.m_BRAFIsIndicated = value; }
        }

		public string Interpretation
		{
			get { return this.m_Interpretation; }
		}

		public string Comment
		{
			get { return this.m_Comment; }
		}

        public string Method
        {
            get { return this.m_Method; }
        }

        public string References
        {
            get { return this.m_References; }
        }

        public bool AreAllIntact()
        {
            bool result = true;
            if (this.m_PMS2Result != LSEResultEnum.Intact) result = false;
            if (this.m_MSH6Result != LSEResultEnum.Intact) result = false;
            if (this.m_MSH2Result != LSEResultEnum.Intact) result = false;
            if (this.m_MLH1Result != LSEResultEnum.Intact) result = false;
            return result;
        }

        public bool AreAnyLoss()
        {
            bool result = false;
            if (this.m_PMS2Result != LSEResultEnum.Loss) result = true;
            if (this.m_MSH6Result != LSEResultEnum.Loss) result = true;
            if (this.m_MSH2Result != LSEResultEnum.Loss) result = true;
            if (this.m_MLH1Result != LSEResultEnum.Loss) result = true;
            return result;
        }

        public virtual void SetResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromEvaluation)
        {            
            panelSetOrderLynchSyndromEvaluation.Interpretation = this.m_Interpretation;
            panelSetOrderLynchSyndromEvaluation.Comment = this.m_Comment;
            panelSetOrderLynchSyndromEvaluation.BRAFIsIndicated = this.m_BRAFIsIndicated;
            panelSetOrderLynchSyndromEvaluation.Method = this.m_Method;
            panelSetOrderLynchSyndromEvaluation.ReportReferences = this.m_References;			
        }

        public string BuildLossInterpretation()
        {
            string result = "Loss of nuclear expression of ";            
            List<string> results = new List<string>();
            if (this.m_MLH1Result == LSEResultEnum.Loss) results.Add("MLH1");
            if (this.m_MSH2Result == LSEResultEnum.Loss) results.Add("MSH2");
            if (this.m_MSH6Result == LSEResultEnum.Loss) results.Add("MSH6");
            if (this.m_PMS2Result == LSEResultEnum.Loss) results.Add("PMS2");

            var joinedResults = string.Join(", ", results);
            if(results.Count > 1)
            {
                int posOfLastComma = joinedResults.LastIndexOf(",");
                joinedResults = joinedResults.Remove(posOfLastComma, 1);
                joinedResults = joinedResults.Insert(posOfLastComma, " and");
            }
            
            result = result + joinedResults + " mismatch repair proteins";
            return result;
        }

		public static LSEResult GetResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromEvaluation)
		{
			LSEResult result = new LSEResult();
			YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest panelSetLynchSyndromeIHCPanel = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest();
			YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLynchSyndromeIHCPanel.PanelSetId, panelSetOrderLynchSyndromEvaluation.OrderedOnId, true);
			if (panelSetOrderLynchSyndromeIHC != null) panelSetOrderLynchSyndromeIHC.SetSummaryResult(result);

            result.BRAFIsIndicated = panelSetOrderLynchSyndromEvaluation.BRAFIsIndicated;
            if (panelSetOrderLynchSyndromEvaluation.BRAFIsIndicated == true)
			{
                YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafMutationAnalysisTest = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();

                YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest rasRAFPanelTest = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest();
                if (accessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromEvaluation.OrderedOnId, false) == true) 
                {
                    YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromEvaluation.OrderedOnId, false);
                    panelSetOrderBraf.SetSummaryResult(result);
                }
                else if (accessionOrder.PanelSetOrderCollection.Exists(brafMutationAnalysisTest.PanelSetId, panelSetOrderLynchSyndromEvaluation.OrderedOnId, false) == true)
                {
                    YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafMutationAnalysisTest.PanelSetId, panelSetOrderLynchSyndromEvaluation.OrderedOnId, false);
                    brafMutationAnalysisTestOrder.SetSummaryResult(result);
                }
                else if(accessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromEvaluation.OrderedOnId, true) == false)
                {
                    YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder panelSetOrderRASRAF = (YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromEvaluation.OrderedOnId, false);
                    panelSetOrderRASRAF.SetBrafSummaryResult(result);
                }
            }

			YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();			
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromEvaluation.OrderedOnId, true) == true)
            {
				YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromEvaluation.OrderedOnId, true);
                panelSetOrderMLH1MethylationAnalysis.SetSummaryResult(result);
            }

			return result;
		}
	}
}
