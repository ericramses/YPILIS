using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.HematopathologySummary
{
    [PersistentClass("tblHematopathologySummaryTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class HematopathologySummaryTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_Result;
        private string m_Interpretation;

        public HematopathologySummaryTestOrder()
        { }

        public HematopathologySummaryTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
        }

        public YellowstonePathology.Business.Rules.MethodResult IsOkToSetResults()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new YellowstonePathology.Business.Rules.MethodResult();
            if (this.m_Accepted == true)
            {
                result.Success = false;
                result.Message = "Results may not be set because the results already have been accepted.";
            }
            return result;
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
            result.Append("Result: ");
            result.Append(this.m_Result);
            result.Append("Interpretation: ");
            result.AppendLine(this.m_Interpretation);
            return result.ToString();
        }
    }
}
