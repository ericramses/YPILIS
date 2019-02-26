using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    [PersistentClass("tblHER2AmplificationSummaryTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class HER2AmplificationSummaryTestOrder : HER2AmplificationByISH.HER2AmplificationByISHTestOrder
    {
        private int m_CellsRecount;
        private int m_TotalChr17SignalsRecount;
        private int m_TotalHer2SignalsRecount;
        private string m_IHCScore;

        public HER2AmplificationSummaryTestOrder()
        { }

        public HER2AmplificationSummaryTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        { }

        [PersistentProperty()]
        public int CellsRecount
        {
            get { return this.m_CellsRecount; }
            set
            {
                if (this.m_CellsRecount != value)
                {
                    this.m_CellsRecount = value;
                    this.NotifyPropertyChanged("CellsRecount");
                }
            }
        }

        [PersistentProperty()]
        public int TotalChr17SignalsRecount
        {
            get { return this.m_TotalChr17SignalsRecount; }
            set
            {
                if (this.m_TotalChr17SignalsRecount != value)
                {
                    this.m_TotalChr17SignalsRecount = value;
                    this.NotifyPropertyChanged("TotalChr17SignalsRecount");
                }
            }
        }

        [PersistentProperty()]
        public int TotalHer2SignalsRecount
        {
            get { return this.m_TotalHer2SignalsRecount; }
            set
            {
                if (this.m_TotalHer2SignalsRecount != value)
                {
                    this.m_TotalHer2SignalsRecount = value;
                    this.NotifyPropertyChanged("TotalHer2SignalsRecount");
                }
            }
        }

        [PersistentProperty()]
        public string IHCScore
        {
            get { return this.m_IHCScore; }
            set
            {
                if (this.m_IHCScore != value)
                {
                    this.m_IHCScore = value;
                    this.NotifyPropertyChanged("IHCScore");
                }
            }
        }

        public void SetValues(AccessionOrder accessionOrder)
        {
            HER2AmplificationByISH.HER2AmplificationByISHTest ishTest = new HER2AmplificationByISH.HER2AmplificationByISHTest();
            Her2AmplificationByIHC.Her2AmplificationByIHCTest ihcTest = new Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            HER2AmplificationRecount.HER2AmplificationRecountTest recountTest = new HER2AmplificationRecount.HER2AmplificationRecountTest();

            HER2AmplificationByISH.HER2AmplificationByISHTestOrder ishTestOrder = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ishTest.PanelSetId, this.m_OrderedOnId, true);
            this.m_CellsCounted = ishTestOrder.CellsCounted;
            this.m_TotalChr17SignalsCounted = ishTestOrder.TotalChr17SignalsCounted;
            this.m_TotalHer2SignalsCounted = ishTestOrder.TotalHer2SignalsCounted;
            this.m_Her2byIHCOrder = ishTestOrder.Her2byIHCOrder;
            this.m_NumberOfObservers = ishTestOrder.NumberOfObservers;
            this.m_IncludeImmunoRecommendedComment = ishTestOrder.IncludeImmunoRecommendedComment;
            this.m_IncludeResultComment = ishTestOrder.IncludeResultComment;
            this.m_IncludePolysomyComment = ishTestOrder.IncludePolysomyComment;
            this.m_PolysomyPercent = ishTestOrder.PolysomyPercent;
            this.m_Chr17SignalRangeLow = ishTestOrder.Chr17SignalRangeLow;
            this.m_Chr17SignalRangeHigh = ishTestOrder.Chr17SignalRangeHigh;
            this.m_Her2SignalRangeLow = ishTestOrder.Her2SignalRangeLow;
            this.m_Her2SignalRangeHigh = ishTestOrder.Her2SignalRangeHigh;
            this.m_CommentLabel = ishTestOrder.CommentLabel;
            this.m_SampleAdequacy = ishTestOrder.SampleAdequacy;
            this.m_ProbeSignalIntensity = ishTestOrder.ProbeSignalIntensity;
            this.m_TechComment = ishTestOrder.TechComment;
            this.m_ResultComment = ishTestOrder.ResultComment;
            this.m_InterpretiveComment = ishTestOrder.InterpretiveComment;
            this.m_ResultDescription = ishTestOrder.ResultDescription;
            this.m_SourceBlock = ishTestOrder.SourceBlock;
            this.m_GeneticHeterogeneity = ishTestOrder.GeneticHeterogeneity;
            this.m_Her2Chr17ClusterRatio = ishTestOrder.Her2Chr17ClusterRatio;
            this.m_ReportReference = ishTestOrder.ReportReference;
            this.m_Indicator = ishTestOrder.Indicator;
            this.m_Method = ishTestOrder.Method;
            this.m_NotInterpretable = ishTestOrder.NotInterpretable;
            this.m_ASRComment = ishTestOrder.ASRComment;
            this.m_FixationComment = ishTestOrder.FixationComment;
            this.m_HER2ByIHCRequired = ishTestOrder.HER2ByIHCRequired;
            this.m_RecountRequired = ishTestOrder.RecountRequired;

            Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC ihcTestOrder = (Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ihcTest.PanelSetId, this.m_OrderedOnId, true);
            this.m_IHCScore = ihcTestOrder.Score;

            if (accessionOrder.PanelSetOrderCollection.Exists(recountTest.PanelSetId, this.m_OrderedOnId, true) == true)
            {
                HER2AmplificationRecount.HER2AmplificationRecountTestOrder recountTestOrder = (HER2AmplificationRecount.HER2AmplificationRecountTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(recountTest.PanelSetId, this.m_OrderedOnId, true);
                this.m_CellsRecount = recountTestOrder.CellsCounted;
                this.m_TotalChr17SignalsRecount = recountTestOrder.Chr17SignalsCounted;
                this.m_TotalHer2SignalsRecount = recountTestOrder.Her2SignalsCounted;
                this.m_CellsCounted = recountTestOrder.CellsCounted;
                this.m_TotalChr17SignalsCounted = recountTestOrder.Chr17SignalsCounted;
                this.m_TotalHer2SignalsCounted = recountTestOrder.Her2SignalsCounted;
            }

            HER2AmplificationByISH.HER2AmplificationResultCollection resultCollection = new HER2AmplificationByISH.HER2AmplificationResultCollection(accessionOrder.PanelSetOrderCollection, this.m_ReportNo);
            HER2AmplificationByISH.HER2AmplificationResult result = resultCollection.FindMatch();
            Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_OrderedOn, this.m_OrderedOnId);
            result.SetResults(specimenOrder);
            this.m_CellsCounted = ishTestOrder.CellsCounted;
            this.m_TotalChr17SignalsCounted = ishTestOrder.TotalChr17SignalsCounted;
            this.m_TotalHer2SignalsCounted = ishTestOrder.TotalHer2SignalsCounted;
        }
    }
}
