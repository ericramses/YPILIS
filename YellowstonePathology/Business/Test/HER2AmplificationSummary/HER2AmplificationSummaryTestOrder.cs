using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    [PersistentClass("tblHER2AmplificationSummaryTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class HER2AmplificationSummaryTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_Interpretation;

        public HER2AmplificationSummaryTestOrder()
        { }

        public HER2AmplificationSummaryTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        { }

        [PersistentProperty()]
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
            string result = "HER2 Amplification Summary: " + "Under construction";
            return result;
        }

        public YellowstonePathology.Business.Rules.MethodResult IsOkToSetResults()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            if (this.m_Accepted == true)
            {
                result.Success = false;
                result.Message = "The results may not be set because they have already been accepted.";
            }
            return result;
        }
    }
}
