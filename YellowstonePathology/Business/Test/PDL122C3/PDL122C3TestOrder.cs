using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.PDL122C3
{
    [PersistentClass("tblPDL122C3TestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class PDL122C3TestOrder : PanelSetOrder
    {
        private string m_Result;
        private string m_StainPercent;
        private string m_Method;
        private string m_Comment;
        private string m_Interpretation;

        public PDL122C3TestOrder()
        {
        	
        }
        
        public PDL122C3TestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
            this.m_Method = "PD-L1 22C3 FDA (KEYTRUDA): " +
                "PD - L1 staining was performed utilizing the DAKO FDA - approved PD - L1, 22C3 pharmDx protocol using the Dako Automated Link 48 platform.Following incubation " +
                "with the primary monoclonal antibody to PD - L1 or the Negative Control Reagent(NCR), specimens were incubated with a Linker antibody specific to the host species " +
                "of the primary antibody, and then were incubated with a ready-to - use visualization reagent consisting of secondary antibody molecules and horseradish peroxidase " +
                "molecules coupled to a dextran polymer backbone.The enzymatic conversion of the subsequently added chromogen results in precipitation of a visible reaction product at " +
                "the site of antigen.The color of the chromogenic reaction was modified by a chromogen enhancement reagent.The specimen then was counterstained and coverslipped.PD - L1 " +
                "protein expression is determined by using Tumor Proportion Score(TPS), which is the percentage of viable tumor cells showing partial or complete membrane staining. The specimen " +
                "should be considered PD-L1 positive for KEYTRUDA (pembrolizumab) eligibility if TPS >= 50% of the viable tumor cells exhibit membrane staining at any intensity.";
            this.m_Interpretation = "PD-L1 is one of the receptors for PD-1. PD-L1 is inducibly expressed on both hematopoietic and " +
                "non-hematopoietic cells following cellspecific stimulation and plays a role in maintenance of peripheral tolerance.  PD - L1 " +
                "expression has been linked to poorer prognosis and shorter survival in some tumor types.  On - going clinical trials are " +
                "evaluating the efficacy of inhibition of PD - L1 in various tumors.";
            this.m_ReportReferences = "1. Ohaegbulam KC, Assal A, Lazar-Molnar E, et al. Human cancer immunotherapy with antibodies to the PD-1 and " +
                "PD-L1 pathway. Trends Mol Med. 2015; 21(1):24 - 33." + Environment.NewLine +
                "2. D'Incecco A, Andreozzi M, et al.PD - 1 and PD - L1 expression in molecularly selected non - small - cell lung cancer " +
                "patients.Br J Cancer. 2015; 112(1):95 - 102." + Environment.NewLine +
                "3. Massi D, Brusa D, Merelli B, et al. PD - L1 marks a subset of melanomas with a shorter overall survival and distinct genetic " +
                "and morphological characteristics. Ann Oncol. 2014; 25(12):2433 - 42." + Environment.NewLine +
                "4. Chen BJ, Chapuy B, Ouyang J, et al. PD - L1 expression is characteristic of a subset of aggressive B-cell lymphomas and " +
                "virusassociated malignancies.Clin Cancer Res. 2013; 19(13):3462 - 73." + Environment.NewLine +
                "5. Zhang Y, Kang S, Shen J, et al. Prognostic significance of programmed cell death 1(PD - 1) or PD-1 ligand 1(PD - L1) Expression " +
                "in epithelial - originated cancer: a meta-analysis.Medicine(Baltimore). 2015; 94(6):e515.";
            this.m_Comment = "This test utilizes PD-L1 antibody clone 22C3.  In general, higher levels of PD-L1 expression are associated with " +
                "better response to PD-1 antagonists.";
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    this.NotifyPropertyChanged("Result");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string StainPercent
        {
            get { return this.m_StainPercent; }
            set
            {
                if (this.m_StainPercent != value)
                {
                    this.m_StainPercent = value;
                    this.NotifyPropertyChanged("StainPercent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string Method
        {
            get { return this.m_Method; }
            set
            {
                if (this.m_Method != value)
                {
                    this.m_Method = value;
                    this.NotifyPropertyChanged("Method");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string Comment
        {
            get { return this.m_Comment; }
            set
            {
                if (this.m_Comment != value)
                {
                    this.m_Comment = value;
                    this.NotifyPropertyChanged("Comment");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string Interpretation
        {
            get { return this.m_Interpretation; }
            set
            {
                if (this.m_Interpretation != value)
                {
                    this.m_Interpretation = value;
                    this.NotifyPropertyChanged("Interpretation");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Result: " + this.m_Result);
            result.AppendLine();

            result.AppendLine("Stain Percent: " + this.m_StainPercent);
            result.AppendLine();

            return result.ToString();
        }

		public override YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
		{
			YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToAccept();
			if (result.Success == true)
			{
				if(string.IsNullOrEmpty(this.StainPercent) == true)
				{
					result.Success = false;
					result.Message = "The results cannot be accepted because the stain percent is not set.";
				}				
			}
			return result;
		}
    }
}
