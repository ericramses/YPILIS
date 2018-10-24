using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationResultCollection : ObservableCollection<HER2AmplificationResult>
    {
        public HER2AmplificationResultCollection(PanelSetOrderCollection panelSetOrderCollection)
        {
            this.Add(new HER2AmplificationResultGroup1(panelSetOrderCollection));
            this.Add(new HER2AmplificationResultGroup2(panelSetOrderCollection));
        }

        public HER2AmplificationResultMatch FindMatch()
        {
            HER2AmplificationResultMatch result = new HER2AmplificationResultMatch();

            foreach (HER2AmplificationResult her2AmplificationResult in this)
            {
                her2AmplificationResult.IsAMatch(result);
                if (result.IsAMatch == true)
                {
                    break;
                }
            }
            return result;
        }
    }
}
