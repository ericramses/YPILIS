using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class HistologySlidePaperZPLLabel
    {
        private string m_SlideOrderId;
        private string m_ReportNo;
        private string m_LastName;        
        private string m_TestName;
        private string m_SlideLabel;
        private string m_Location;

        public HistologySlidePaperZPLLabel(string slideOrderId, string reportNo, string lastName, string testName, string slideLabel, string location)
        {
            this.m_SlideOrderId = slideOrderId;
            this.m_ReportNo = reportNo;
            this.m_LastName = lastName;            
            this.m_TestName = testName;
            this.m_SlideLabel = slideLabel;
            this.m_Location = location;
        }

        public void AppendCommands(StringBuilder result, int xOffset)
        {                        
            string truncatedtestName = null;
            if (this.m_TestName.Length > 16)
            {
                truncatedtestName = this.m_TestName.Substring(0, 16);
            }
            else
            {
                truncatedtestName = this.m_TestName;
            }

            string truncatedLastName = null;
            if (this.m_LastName.Length > 16)
            {
                truncatedLastName = this.m_LastName.Substring(0, 16);
            }
            else
            {
                truncatedLastName = this.m_LastName;
            }            
            
            result.Append("^PW440");
            result.Append("^FWR");
            result.Append("^FO0,0^AO,15,10^FD" + this.m_Location + "^FS");            
            result.Append("^FO120,0^AO,30,15^FD" + truncatedLastName + "^FS");
            result.Append("^FO80,0^AO,30,15^FD" + truncatedtestName + "^FS");
            result.Append("^FO50,0^AO,30,15^FD" + this.m_SlideLabel + "^FS");
            result.Append("^FO0,190^BXN,04,200^FD" + "HSLD" + this.m_SlideOrderId + "^FS");            
            result.Append("^FO150,0^AO,30,15^FD" + this.m_ReportNo + "^FS");            
        }
    }
}
