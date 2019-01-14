using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSERule: INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        public const string LSEColonReferences = "Practice EoGAi, Group PW. Recommendations from the EGAPP Working Group; genetic testing strategies in newly diagnosed " +
            "individuals with colorectal cancer aimed at reducing morbidity and mortality from Lynch Syndrome in relatives. Genet Med. 2009 January; 11(1): 35-41.";

        public const string LSEGYNReferences = "Meyer L, Broaddus R, Lu K. Endometrial cancer and Lynch syndrome: clinical and pathologic considerations. Cancer Control. 2009;16(1):14–22";
        public const string LSEGENReferences = "Le DT, Durham, JN, Smith KN et al.  Mismatch-repair deficiency predicts response of solid tumors to PD-1 blockade.  Science. 2017 July 28; 357(6349): 409-413.";

        public static string IHCMethod = YellowstonePathology.Business.Test.LynchSyndrome.LSEIHCResult.Method;
		public static string IHCBRAFMethod = "IHC: " + YellowstonePathology.Business.Test.LynchSyndrome.LSEIHCResult.Method + " BRAF: " + YellowstonePathology.Business.Test.BRAFV600EK.BRAFResult.Method;
		public static string IHCBRAFMLHMethod = "IHC: " + YellowstonePathology.Business.Test.LynchSyndrome.LSEIHCResult.Method + " BRAF: " + YellowstonePathology.Business.Test.BRAFV600EK.BRAFResult.Method + " MLH: " + YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisResult.Method;

        public static string GeneralIndication = "Assess tumor for mismatch repair deficiency to determine eligibility for PD-1 blockade therapy; screening for Lynch Syndrome.";
        public static string IHCAllIntactResult = "Intact nuclear expression of MLH1, MSH2, MSH6, and PMS2 mismatch repair proteins.";

        public static string AdditionalTestingNone = "No Additional Testing Required";
        public static string AdditionalTestingReflexBRAFMeth = "Reflex to BRAf and METH";
        public static string AdditionalTestingMeth = "Perform Methylation Analysis";

        //protected bool m_IHCMatched;
        protected string m_Indication;
        protected string m_ResultName;

        protected LSEResultEnum m_PMS2Result;
		protected LSEResultEnum m_MSH6Result;
		protected LSEResultEnum m_MSH2Result;
		protected LSEResultEnum m_MLH1Result;

        protected string m_BRAFResult;
        protected string m_MethResult;
        //protected string m_AdditionalTesting;

		protected string m_Interpretation;
		protected string m_Result;
        protected string m_Method;
        protected string m_References;

		public LSERule()
		{
		}

        /*public bool IHCMatched
        {
            get { return this.m_IHCMatched; }
            set
            {
                if(this.m_IHCMatched != value)
                {
                    this.m_IHCMatched = value;
                    this.NotifyPropertyChanged("IHCMatched");
                }                
            }
        }*/

        public string ResultName
        {
            get { return this.m_ResultName; }
            set { this.m_ResultName = value; }
        }

        public string Indication
        {
            get { return this.m_Indication; }
            set { this.m_Indication = value; }
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

        public string BRAFResult
        {
            get { return this.m_BRAFResult; }
            set { this.m_BRAFResult = value; }
        }

        public string MethResult
        {
            get { return this.m_MethResult; }
            set { this.m_MethResult = value; }
        }

        /*public string AdditionalTesting
        {
            get { return this.m_AdditionalTesting; }
            set { this.m_AdditionalTesting = value; }
        }*/

		public string Interpretation
		{
			get { return this.m_Interpretation; }
		}

		public string Result
		{
			get { return this.m_Result; }
		}

        public string Method
        {
            get { return this.m_Method; }
        }

        public string References
        {
            get { return this.m_References; }
        }

        public virtual void SetResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromEvaluation)
        {
            throw new Exception("needs work");
            /*
            panelSetOrderLynchSyndromEvaluation.Interpretation = this.m_Interpretation;
            panelSetOrderLynchSyndromEvaluation.Result = this.m_Result;
            panelSetOrderLynchSyndromEvaluation.ReflexToBRAFMeth = this.ReflexToBRAFMeth;
            panelSetOrderLynchSyndromEvaluation.Method = this.m_Method;
            panelSetOrderLynchSyndromEvaluation.ReportReferences = this.m_References;			
            */
        }

        public virtual void SetResultsV2(PanelSetOrderLynchSyndromeEvaluation psoLSE)
        {

            /*
            if(this.AreAnyLoss() == false)
            {
                this.m_Interpretation = IHCAllIntactResult;
            }
            else
            {
                this.m_Interpretation = this.BuildLossInterpretation();
            }
            */

            /*
            if(this.m_BrafResult != LSEResultEnum.NotPerformed)
            {
                this.m_Interpretation = this.m_Interpretation + Environment.NewLine + "BRAF mutation V600E ";
                if (this.m_BrafResult == LSEResultEnum.Detected) this.m_Interpretation += "DETECTED.";
                if (this.m_BrafResult == LSEResultEnum.NotDetected) this.m_Interpretation += "NOT DETECTED.";
            }

            if (this.m_MethResult != LSEResultEnum.NotPerformed)
            {
                this.m_Interpretation = this.m_Interpretation + Environment.NewLine + "MLH1 promoter methylation ";
                if (this.m_MethResult == LSEResultEnum.Detected) this.m_Interpretation += "DETECTED.";
                if (this.m_MethResult == LSEResultEnum.NotDetected) this.m_Interpretation += "NOT DETECTED.";
            }
            */
        }

        public virtual bool IsIHCMatch(IHCResult ihcResult)
        {
            bool result = false;
            if (this.MLH1Result == ihcResult.MLH1Result.LSEResult &&
                this.MSH2Result == ihcResult.MSH2Result.LSEResult &&
                this.MSH6Result == ihcResult.MSH6Result.LSEResult &&
                this.PMS2Result == ihcResult.PMS2Result.LSEResult)
            {
                result = true;
            }
            return result;
        }

        public string BuildLossResult()
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
            
            result = result + joinedResults + " mismatch repair proteins. ";
            return result;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}