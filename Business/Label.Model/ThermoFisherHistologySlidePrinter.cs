using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class ThermoFisherHistologySlidePrinter
    {
        private List<HistologySlideDirectPrintLabel> m_Queue;

        public ThermoFisherHistologySlidePrinter()
        {
            this.m_Queue = new List<HistologySlideDirectPrintLabel>();
        }

        public List<HistologySlideDirectPrintLabel> Queue
        {
            get { return this.m_Queue; }
        }

        public void Print()
        {
            if (string.IsNullOrEmpty(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.SlideMatePrinterPath) == false)
            {
                StringBuilder lines = new StringBuilder();
                foreach (HistologySlideDirectPrintLabel histologySlideDirectPrintLabel in this.m_Queue)
                {
                    lines.Append(histologySlideDirectPrintLabel.GetLine());
                }
                
                string fileName = System.IO.Path.Combine(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.SlideMatePrinterPath, Guid.NewGuid() + ".txt");
                using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(fileName))
                {
                    streamWriter.Write(lines.ToString());
                }
            }
        }
    }
}
