using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.HER2AnalysisSummary
{
    public class HER2AnalysisSummaryResultCollection : ObservableCollection<HER2AnalysisSummaryResult>
    {
        public HER2AnalysisSummaryResultCollection(PanelSetOrderCollection panelSetOrderCollection, string reportNo)
        {
            this.Add(new HER2AnalysisSummaryResultGroup2Breast(panelSetOrderCollection, reportNo));
            this.Add(new HER2AnalysisSummaryResultGroup3Breast(panelSetOrderCollection, reportNo));
            this.Add(new HER2AnalysisSummaryResultGroup4Breast(panelSetOrderCollection, reportNo));
        }

        public HER2AnalysisSummaryResult FindMatch()
        {
            HER2AnalysisSummaryResult result = null;

            foreach (HER2AnalysisSummaryResult her2AnalysisSummaryResult in this)
            {
                if (her2AnalysisSummaryResult.IsAMatch() == true)
                {
                    result = her2AnalysisSummaryResult;
                    break;
                }
            }
            return result;
        }
    }
}
