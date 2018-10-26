using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationRecount
{
    public class HER2AmplificationRecountWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public HER2AmplificationRecountWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            HER2AmplificationRecountTestOrder her2AmplificationRecountTestOrder = (HER2AmplificationRecountTestOrder)this.m_PanelSetOrder;

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HER2AmplificationRecount.xml";
            base.OpenTemplate();

            base.SetDemographicsV2();

            this.ReplaceText("cells_counted", her2AmplificationRecountTestOrder.CellsCounted.ToString());
            this.ReplaceText("cep17_count", her2AmplificationRecountTestOrder.CEP17Counted);
            this.ReplaceText("her2_count", her2AmplificationRecountTestOrder.HER2Counted);

            string finalDate = YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate) + " - " + YellowstonePathology.Business.BaseData.GetMillitaryTimeString(this.m_PanelSetOrder.FinalTime);
            this.SetXmlNodeData("final_date", finalDate);

            this.SetReportDistribution();
            this.SetCaseHistory();

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
