using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationResultGroup2 : HER2AmplificationResult
    {
        private string m_Result;

        public HER2AmplificationResultGroup2(PanelSetOrderCollection panelSetOrderCollection) : base(panelSetOrderCollection)
        {
            this.m_Result = "Negative";
        }
        public string Result
        {
            get { return m_Result; }
        }

        public override bool IsAMatch()
        {
            bool result = false;

            return result;
        }
    }
}
