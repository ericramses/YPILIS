using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResult : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected HER2AmplificationByISHTestOrder m_HER2AmplificationByISHTestOrder;
        protected Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC m_PanelSetOrderHer2AmplificationByIHC;
        protected string m_Interpretation;
        protected HER2AmplificationResultEnum m_Result;

        protected string m_InterpretiveComment;
        protected string m_ResultComment;
        protected string m_ResultDescription;
        protected string m_ReportReference;
        protected string m_FixationOutOfBoundsComment = "The specimen fixation time does not meet ASCO CAP guidelines (6 to 72 hours), which " +
            "may cause false negative results.  Repeat testing on an alternate specimen that meets fixation time guidelines is recommended, " +
            "if available.";
        protected string m_FixationColdSchemaComment = "The time from specimen procurement to fixation in formalin (cold ischemia time) exceeds " +
            "one hour, which may cause false negative results.  Repeat testing on an alternate specimen that meets ASCO CAP guidelines for cold " +
            "ischemia time is recommended, if available.";

        public HER2AmplificationResult(PanelSetOrderCollection panelSetOrderCollection)
        {
            Her2AmplificationByIHC.Her2AmplificationByIHCTest her2AmplificationByIHCTest = new Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            HER2AmplificationByISH.HER2AmplificationByISHTest her2AmplificationByISHTest = new HER2AmplificationByISH.HER2AmplificationByISHTest();

            if(panelSetOrderCollection.Exists(her2AmplificationByISHTest.PanelSetId) == true)
            {
                this.m_HER2AmplificationByISHTestOrder = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)panelSetOrderCollection.GetPanelSetOrder(her2AmplificationByISHTest.PanelSetId);
            }

            if (panelSetOrderCollection.Exists(her2AmplificationByIHCTest.PanelSetId) == true)
            {
                this.m_PanelSetOrderHer2AmplificationByIHC = (Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)panelSetOrderCollection.GetPanelSetOrder(her2AmplificationByIHCTest.PanelSetId);
            }
        }

        public string Interpretation
        {
            get { return m_Interpretation; }
            set
            {
                if (this.m_Interpretation != value)
                {
                    this.m_Interpretation = value;
                    NotifyPropertyChanged("Interpretation");
                }
            }
        }

        public HER2AmplificationResultEnum Result
        {
            get { return m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    NotifyPropertyChanged("Result");
                }
            }
        }

        public virtual bool IsAMatch()
        {
            return false;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void HandleIHC()
        {
            if (this.m_PanelSetOrderHer2AmplificationByIHC != null && this.m_PanelSetOrderHer2AmplificationByIHC.Accepted == true)
            {
                if (this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("0") || this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("1+"))
                {
                    this.m_Result = HER2AmplificationResultEnum.Negative;
                }
                else if (this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("2+"))
                {
                    this.m_HER2AmplificationByISHTestOrder.RecountRequired = true;
                }
                else if (this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("3+"))
                {
                    this.m_Result = HER2AmplificationResultEnum.Positive;
                }
            }
        }
        public bool IsRecountNeeded()
        {
            bool result = false;
            if (this.m_HER2AmplificationByISHTestOrder.RecountRequired == true && this.m_HER2AmplificationByISHTestOrder.NumberOfObservers < 3) result = true;
            return result;
        }

        public virtual void SetResults(HER2AmplificationByISHTestOrder testOrder, Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            if (testOrder.GeneticHeterogeneity == HER2AmplificationByISHGeneticHeterogeneityCollection.GeneticHeterogeneityPresentInCells)
            {
                this.m_InterpretiveComment += Environment.NewLine + Environment.NewLine +
                    "However, this tumor exhibits genetic heterogeneity in HER2 gene amplification in scattered individual cells.  The " +
                    "clinical significance and potential clinical benefit of trastuzumab is uncertain when " +
                    testOrder.Indicator.ToLower() +
                    "carcinoma demonstrates genetic heterogeneity." + Environment.NewLine + Environment.NewLine;
                this.m_ResultComment = "This tumor exhibits genetic heterogeneity in HER2 gene amplification in scattered individual cells.  The clinical " +
                    "significance and potential clinical benefit of trastuzumab is uncertain when " +
                    testOrder.Indicator.ToLower() +
                    " carcinoma demonstrates genetic heterogeneity";
            }
            else if (testOrder.GeneticHeterogeneity == HER2AmplificationByISHGeneticHeterogeneityCollection.GeneticHeterogeneityPresentInClusters)
            {
                this.m_InterpretiveComment += Environment.NewLine + Environment.NewLine +
                    "However, this tumor exhibits genetic heterogeneity in HER2 gene amplification in small cell clusters. The HER2/Chr17 " +
                    "ratio in the clusters is " +
                    testOrder.Her2Chr17ClusterRatio +
                    ".  The clinical significance and potential clinical benefit of trastuzumab is uncertain when " +
                    testOrder.Indicator.ToLower() +
                    " carcinoma demonstrates genetic heterogeneity." + Environment.NewLine + Environment.NewLine;
                this.m_ResultComment = "This tumor exhibits genetic heterogeneity in HER2 gene amplification in small cell clusters.  The clinical significance " +
                    "and potential clinical benefit of trastuzumab is uncertain when " +
                    testOrder.Indicator.ToLower() +
                    " carcinoma demonstrates genetic heterogeneity.";
            }

            testOrder.Result = this.m_Result.ToString();
            //testOrder.ResultCode = this.m_ResultCode;
            testOrder.ResultComment = this.m_ResultComment;
            testOrder.InterpretiveComment = this.m_InterpretiveComment.TrimEnd();
            testOrder.ResultDescription = this.m_ResultDescription;
            testOrder.CommentLabel = null;
            testOrder.ReportReference = this.m_ReportReference;
            testOrder.NoCharge = false;

            if (specimenOrder.FixationDuration > 72 || specimenOrder.FixationDuration < 6)
            {
                specimenOrder.FixationComment = m_FixationOutOfBoundsComment;
            }
        }
    }
}
