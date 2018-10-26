using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationResult
    {
        protected PanelSetOrderCollection m_PanelSetOrderCollection;
        protected Double? m_HER2CEP17Ratio;
        protected Double? m_AverageHER2CopyNo;
        protected bool m_HER2ByIHCRequired;
        protected bool m_HER2ByIHCIsOrdered;
        protected bool m_HER2ByIHCIsAccepted;
        protected string m_HER2ByIHCScore;
        protected string m_Interpretation;
        protected HER2AmplificationResultEnum m_Result;

        private bool m_RequiresBlindedObserver;

        public HER2AmplificationResult(PanelSetOrderCollection panelSetOrderCollection)
        {
            this.m_PanelSetOrderCollection = panelSetOrderCollection;
            Her2AmplificationByIHC.Her2AmplificationByIHCTest her2AmplificationByIHCTest = new Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            HER2AmplificationByISH.HER2AmplificationByISHTest her2AmplificationByISHTest = new HER2AmplificationByISH.HER2AmplificationByISHTest();

            if(this.m_PanelSetOrderCollection.Exists(her2AmplificationByISHTest.PanelSetId) == true)
            {
                HER2AmplificationByISH.HER2AmplificationByISHTestOrder her2AmplificationByISHTestOrder = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_PanelSetOrderCollection.GetPanelSetOrder(her2AmplificationByISHTest.PanelSetId);
                if (her2AmplificationByISHTestOrder.Accepted == true)
                {
                    this.m_HER2CEP17Ratio = her2AmplificationByISHTestOrder.AverageHer2Chr17SignalAsDouble;
                    this.m_AverageHER2CopyNo = her2AmplificationByISHTestOrder.AverageHer2NeuSignal;
                    this.m_HER2ByIHCRequired = her2AmplificationByISHTestOrder.HER2ByIHCRequired;
                }
            }

            if (this.m_PanelSetOrderCollection.Exists(her2AmplificationByIHCTest.PanelSetId) == true)
            {
                this.m_HER2ByIHCIsOrdered = true;
                Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC panelSetOrderHer2AmplificationByIHC = (Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)this.m_PanelSetOrderCollection.GetPanelSetOrder(her2AmplificationByIHCTest.PanelSetId);
                if (panelSetOrderHer2AmplificationByIHC.Accepted == true)
                {
                    this.m_HER2ByIHCScore = panelSetOrderHer2AmplificationByIHC.Score;
                    this.m_HER2ByIHCIsAccepted = true;
                }
            }
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

        public bool HER2ByIHCIsOrdered
        {
            get { return m_HER2ByIHCIsOrdered; }
        }

        public bool HER2ByIHCIsAccepted
        {
            get { return m_HER2ByIHCIsAccepted; }
        }

        public string HER2ByIHCScore
        {
            get { return m_HER2ByIHCScore; }
        }

        public string Interpretation
        {
            get { return m_Interpretation; }
        }

        public HER2AmplificationResultEnum Result
        {
            get { return m_Result; }
        }

        public bool RequiresBlindedObserver
        {
            get { return m_RequiresBlindedObserver; }
        }

        public virtual bool IsAMatch()
        {
            return false;
        }

        public void HandleIHC()
        {
            if (this.m_HER2ByIHCIsOrdered == true && this.m_HER2ByIHCIsAccepted == true)
            {
                if (this.m_HER2ByIHCScore.Contains("0") || this.m_HER2ByIHCScore.Contains("1+"))
                {
                    this.m_Result = HER2AmplificationResultEnum.Negative;
                }
                else if (this.m_HER2ByIHCScore.Contains("2+"))
                {
                    this.m_RequiresBlindedObserver = true;
                }
                else if (this.m_HER2ByIHCScore.Contains("3+"))
                {
                    this.m_Result = HER2AmplificationResultEnum.Positive;
                }
            }
        }
    }
}
