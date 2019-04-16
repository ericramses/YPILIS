using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.View
{
    public class PanelSetOrderAmendmentViewCollection : ObservableCollection<PanelSetOrderAmendmentView>
    {
        public PanelSetOrderAmendmentViewCollection()
        { }

        public PanelSetOrderAmendmentViewCollection(Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.Refresh(accessionOrder, reportNo);
        }

        public void Refresh(Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.Clear();
            foreach (Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
            {
                if(panelSetOrder.ReportNo == reportNo) this.Add(new View.PanelSetOrderAmendmentView(accessionOrder, panelSetOrder));
            }
        }
    }
}
