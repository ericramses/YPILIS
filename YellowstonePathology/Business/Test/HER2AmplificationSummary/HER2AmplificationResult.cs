using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationResult
    {
        protected string m_HER2Result;
        protected Double? m_HER2CEP17Ratio;
        protected Double? m_AverageHER2CopyNo;
        protected bool m_HER2ByIHCRequired;
        protected string m_HER2ByIHCScore;

        public HER2AmplificationResult(PanelSetOrderCollection panelSetOrderCollection)
        {
            Her2AmplificationByIHC.Her2AmplificationByIHCTest her2AmplificationByIHCTest = new Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            HER2AmplificationByISH.HER2AmplificationByISHTest her2AmplificationByISHTest = new HER2AmplificationByISH.HER2AmplificationByISHTest();

            if (panelSetOrderCollection.Exists(her2AmplificationByIHCTest.PanelSetId) == true)
            {
                Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC panelSetOrderHer2AmplificationByIHC = (Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)panelSetOrderCollection.GetPanelSetOrder(her2AmplificationByIHCTest.PanelSetId);
                this.m_HER2ByIHCScore = panelSetOrderHer2AmplificationByIHC.Score;
            }

            if(panelSetOrderCollection.Exists(her2AmplificationByISHTest.PanelSetId) == true)
            {
                HER2AmplificationByISH.HER2AmplificationByISHTestOrder her2AmplificationByISHTestOrder = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)panelSetOrderCollection.GetPanelSetOrder(her2AmplificationByISHTest.PanelSetId);
                this.m_HER2CEP17Ratio = her2AmplificationByISHTestOrder.AverageHer2Chr17SignalAsDouble;
                this.m_AverageHER2CopyNo = her2AmplificationByISHTestOrder.AverageHer2NeuSignal;
                this.m_HER2ByIHCRequired = her2AmplificationByISHTestOrder.HER2ByIHCRequired;
            }
        }

        public string HER2Result
        {
            get { return m_HER2Result; }
        }

        public Double? HER2CEP17Ratio
        {
            get { return m_HER2CEP17Ratio; }
        }

        public Double? AverageHER2CopyNo
        {
            get { return m_AverageHER2CopyNo; }
        }

        public bool HER2ByIHCRequired
        {
            get { return m_HER2ByIHCRequired; }
        }

        public string HER2ByIHCScore
        {
            get { return m_HER2ByIHCScore; }
        }

        public virtual bool IsAMatch()
        {
            bool result = false;
            return result;
        }
    }
}
